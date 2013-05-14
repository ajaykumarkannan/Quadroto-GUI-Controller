/***************************************************************************
This file is part of Quad Controller.
Quad Controller is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Quad Controller is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Quad Controller.  If not, see <http://www.gnu.org/licenses/>.
***************************************************************************/

/* This file is basically the backend of the entire control software. It handles
 * the serial communication with the Xbee on the computer side. It have multiple 
 * threads to handle reading and writing to the serial port. There are a couple 
 * of functions which are used to parse the commands to be sent to the quadrotor
 * and a few functions to parse the data from the quadrotor.
 */
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace QuadC_Sharp
{

    static class Program
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        public static bool _continue;
        public static int state;
        static Byte[] packet, bytes, wBytes;
        static SerialPort sp;
        static Form1 mForm1;
        public static Thread initialize, readThread, writeThread;
        public static TextWriterTraceListener twtl;
        public static ConsoleTraceListener ctl;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // The following portion is used to write the output to a file at the location specified by twtl line
            Trace.Listeners.Clear();
            twtl = new TextWriterTraceListener("C:\\BTP\\Text_" + DateTime.Now.Month + "." + DateTime.Now.Day + "_" + DateTime.Now.Hour + "." + DateTime.Now.Minute + ".txt");
            //Path.Combine(Path.GetTempPath(), AppDomain.CurrentDomain.FriendlyName));
            twtl.Name = "TextLogger";
            twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

            ctl = new ConsoleTraceListener(false);
            ctl.TraceOutputOptions = TraceOptions.DateTime;

            Trace.Listeners.Add(twtl);
            Trace.Listeners.Add(ctl);
            Trace.AutoFlush = true;

            // AllocConsole();
            // Instantiates the serial port object
            sp = new SerialPort();

            // Initialize thread uses the initPort function
            initialize = new Thread(initPort);
            bytes = new Byte[20];

            // Two state flags are used _continue and state. They will be dealt with in the later bit of the code
            _continue = false;
            state = 0;
            packet = new Byte[20];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mForm1 = new Form1();

            // Starts the initialize thread which opens the serial port and then starts the reader and writer threads
            initialize.Name = "Initialize";
            initialize.Start();
            Application.Run(mForm1);
            state = 3;
            _continue = false;
            state = 3;

            // Join is used when the program is finished to close everything
            initialize.Join();
        }

        // The Initialize threads calls upon this function to open the port. The port parameters can be
        // adjusted here e.g. baud rate, stopbits, etc. 
        public static void initPort()
        {
            /* State Description
             * state == 1 - Open port
             * state == 2 - Close port and close threads
             * state == -3 - Normal read and write. This thread does nothing
             */ 
            while (state != 3)
            {
                if (_continue && state == 1 && !sp.IsOpen)
                {
                    sp.BaudRate = 57600;
                    // sp.BaudRate = 115200;
                    sp.StopBits = StopBits.One;
                    sp.DataBits = 8;
                    sp.Parity = Parity.None;
                    sp.ReadTimeout = 500;
                    sp.WriteTimeout = 500;
                    sp.PortName = mForm1.mQuad.comPort;
                    Console.WriteLine(sp.PortName);
                    // To check if port CAN indeed be opened
                    try
                    {
                        sp.Open();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Could not open COM Port.");
                        _continue = false;
                    }
                    // Starts the read and write threads
                    readThread = new Thread(Read);
                    readThread.Name = "ReadThread";
                    writeThread = new Thread(Write);
                    writeThread.Name = "WriteThread";
                    readThread.Start();
                    writeThread.Start();
                    state = 0;
                }
                else if (!_continue && state == 2)
                {
                    readThread.Join();
                    writeThread.Join();
                    sp.Close();
                    Console.WriteLine("COM Port closed.");
                    state = 0;
                }
                else if (state == -3) break;
                // Do nothing
            }
        }

        // This function is responsible for reading from the serial port. It looks for the start and end packet
        // and once it gets both, it parses the data received via a call to printData
        public static void Read()
        {
            while (_continue)
            {
                try
                {

                    int bytesRead = sp.Read(bytes, 0, bytes.Length);
                    Int16 j = 0;
                    for (int i = 0; i < bytesRead; i++)
                    {
                        if (j >= 20) j = 0;
                        // Trace.WriteLine(bytes[i]);
                        switch (bytes[i])
                        {
                            case 255:
                                j = 0;
                                break;
                            case 254:
                                if (j > 0)
                                {
                                    Thread.BeginCriticalRegion();
                                    printData(j);
                                    Thread.EndCriticalRegion(); 
                                }
                                j = -1;
                                break;
                            default:
                                if (j >= 0) packet[j++] = bytes[i];
                                break;
                        }
                    }
                }
                catch (TimeoutException) { }
                catch (Exception)
                {
                    Trace.WriteLine("Some error in COM Port, most probably ejected.");
                    state = 2;
                    _continue = false;
                }
            }
        }

        /* This function is responsible for writing the commands to the serial port as well as pinging the quadrotor.
         * The packet is made in the makePacket function and depending on the type of packet specified in the 
         * mForm1.mQuad.packetType flag, it writes to the serial port. The packet is stored in wBytes.
         */
        public static void Write()
        {
            while (_continue)
            {
                try
                {
                    wBytes = makePacket(mForm1.mQuad.packetType);
                    if (mForm1.mQuad.packetType != 0)
                    {
                        Thread.BeginCriticalRegion();
                        for (int i = 0; i < wBytes.Length; i++)
                        {
                            Trace.Write(wBytes[i]);
                            Trace.Write(" ");
                        }
                        Trace.WriteLine(" ");
                        Thread.EndCriticalRegion();
                    }
                    sp.Write(wBytes, 0, wBytes.Length);
                    mForm1.mQuad.packetType = 0;
                    System.Threading.Thread.Sleep(300);
                }
                catch (TimeoutException) { }
                catch (Exception)
                {
                    Console.WriteLine("Some error in COM Port, most probably ejected.");
                    state = 2;
                    _continue = false;
                }
            }
        }

        /* This function prepares the command/ping packet. Since we are using a byte based address, if an item is more than
         * one byte, it must be scaled appropriately or must be split into multiple bytes and parsed on the quad side. 
         * See the code on both sides for better understanding.
         * Option: 
         * 0 - Ping
         * 1 - Throttle
         * 2 - Pitch, Roll, Yaw PID
         * 3 - Set the desired angles
         */
        public static Byte[] makePacket(int option)
        {
            Byte[] output;
            int start;
            switch (option)
            {
                case 0:
                    output = new Byte[1 + 5];
                    output[1] = 0x01;
                    output[3] = 0x40;
                    break;
                case 1:
                    output = new Byte[2 + 5];
                    output[1] = 0x02;
                    output[3] = (Byte)(mForm1.mQuad.throttle & 0xFF);
                    output[4] = (Byte)((mForm1.mQuad.throttle >> 8) & 0xFF);
                    break;
                case 2:
                    // Using bytes
                    output = new Byte[9 + 5];
                    output[1] = 0x03;
                    // Pitch PID
                    start = 3;
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.pitchKp) & 0xFF);
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.pitchKi) & 0xFF);
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.pitchKd) & 0xFF);
                    // Roll PID
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.rollKp) & 0xFF);
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.rollKi) & 0xFF);
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.rollKd) & 0xFF);
                    // Yaw PID
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.yawKp) & 0xFF);
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.yawKi) & 0xFF);
                    output[start++] = (Byte)((int)(100 * mForm1.mQuad.yawKd) & 0xFF);
                    /*** The commented part is using floats directly
                    output = new Byte[4 * 9 + 5];
                    output[1] = 0x03;
                    // Pitch PID
                    Trace.Write(mForm1.mQuad.pitchKp);
                    temp = GetBytes(mForm1.mQuad.pitchKp);
                    start = 3;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.Write(mForm1.mQuad.pitchKi);
                    temp = GetBytes(mForm1.mQuad.pitchKi);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.WriteLine(mForm1.mQuad.pitchKd);
                    temp = GetBytes(mForm1.mQuad.pitchKd);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    // Roll PID
                    Trace.Write(mForm1.mQuad.rollKp);
                    temp = GetBytes(mForm1.mQuad.rollKp);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.Write(mForm1.mQuad.rollKi);
                    temp = GetBytes(mForm1.mQuad.rollKi);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.WriteLine(mForm1.mQuad.rollKd);
                    temp = GetBytes(mForm1.mQuad.rollKd);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    // Yaw PID
                    Trace.Write(mForm1.mQuad.yawKp);
                    temp = GetBytes(mForm1.mQuad.yawKp);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.Write(mForm1.mQuad.yawKi);
                    temp = GetBytes(mForm1.mQuad.yawKi);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.WriteLine(mForm1.mQuad.yawKd);
                    temp = GetBytes(mForm1.mQuad.yawKd);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                     **/
                    break;
                case 3:
                    // Desired angles and altitude
                    output = new Byte[4 + 5];
                    output[1] = 0x04;
                    start = 3;
                    output[start++] = (Byte)((int)(mForm1.mQuad.pitch_d) & 0xFF);
                    output[start++] = (Byte)((int)(mForm1.mQuad.roll_d) & 0xFF);
                    output[start++] = (Byte)((int)(mForm1.mQuad.yaw_d) & 0xFF);
                    output[start++] = (Byte)((int)(mForm1.mQuad.alt_d) & 0xFF); 
                    /*** Using floats
                    output = new Byte[3 * 4 + 5];
                    output[1] = 0x04;
                    Trace.Write(mForm1.mQuad.pitch_d);
                    temp = GetBytes(mForm1.mQuad.pitch_d);
                    start = 3;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.Write(mForm1.mQuad.roll_d);
                    temp = GetBytes(mForm1.mQuad.roll_d);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    Trace.Write(" ");
                    Trace.WriteLine(mForm1.mQuad.yaw_d);
                    temp = GetBytes(mForm1.mQuad.yaw_d);
                    start += 4;
                    for (int i = start; i < start + 4; i++) output[i] = temp[i - start];
                    */
                    break;
                default:
                    output = new Byte[1 + 5];
                    output[1] = 0x01;
                    output[3] = 0x40;
                    break;
            }
            output = checkSize(output);
            return output;
        }

        // This function gets the byte array from a uint 
        public static byte[] GetBytes(uint value)
        {
            return new byte[4] { 
                    (byte)(value & 0xFF), 
                    (byte)((value >> 8) & 0xFF), 
                    (byte)((value >> 16) & 0xFF), 
                    (byte)((value >> 24) & 0xFF) };
        }

        // This function gets the byte array from a float. This program won't compile unless the UNSAFE flag is 
        // set in the compiler options. If you are using this project file, it should already be set. If you don't 
        // need this function, remove it and the unsafe flag in the compiler option.
        public static unsafe byte[] GetBytes(float value)
        {
            uint val = *((uint*)&value);
            return GetBytes(val);
        }

        // This is an intermediate function used to check and fill in the missing parts of the packet (such as the
        // xor checksum and the length). 
        static private Byte[] checkSize(Byte[] input)
        {
            Byte[] op;
            int size = input.Length;
            // Not checking first, last or xor bytes
            for (int i = 1; i < input.Length - 2; i++)
            {
                if (input[i] > 252) size++;
            }
            op = new Byte[size];
            op[0] = 0xFF;
            op[size - 1] = 0xFE;
            op[1] = input[1];
            op[2] = (Byte)size;
            int j = 3;
            for (int i = 3; i < input.Length - 2; i++)
            {
                if (input[i] > 252)
                {
                    op[j] = 253;
                    op[j + 1] = (Byte)(input[i] - (Byte)253);
                    j += 2;
                }
                else
                {
                    op[j++] = input[i];
                }
            }
            Byte xorSum = 0;
            for (int i = 1; i < op.Length - 2; i++) xorSum ^= op[i];
            op[op.Length - 2] = xorSum;
            return op;
        }

        // This function parses the data from the quad and outputs to the file mentioned above
        public static void printData(Int16 msize)
        {
            int k, k1 = 0, temp = 0;
            // First byte is packet type and second byte is length
            // Second last byte is XOR check
            for (int i = 0; i < msize; i++) temp ^= packet[i];
            if (temp == 0)
            {
                for (k = 2; k < msize; k++)
                {
                    temp = (SByte)(packet[k]);
                    if (k1 == 2 || k1 == 4 || k1 == 5) temp = packet[k];
                    else temp = (SByte)(packet[k]);
                    if (packet[k] < 253)
                    {
                        Trace.Write(2 * temp);
                    }
                    else
                    {
                        if (k1 == 2 || k1 == 4 || k1 == 5)
                        {
                            Trace.Write(2 * (packet[k] + packet[k + 1]));
                        }
                        else
                        {
                            Trace.Write((SByte)(packet[k] + packet[k + 1]));
                        }
                        k++;
                    }
                    switch (k1)
                    {
                        case 0:
                            mForm1.mQuad.roll_c = 2 * temp;
                            break;
                        case 1:
                            mForm1.mQuad.pitch_c = 2 * temp;
                            break;
                        case 2:
                            mForm1.mQuad.yaw_c = 2 * (packet[k] + packet[k + 1]);
                            break;
                        default:
                            break;
                    }
                    k1++;
                    Trace.Write("\t");
                }
                if (!mForm1.mQuad.changed) mForm1.mQuad.changed = true;
                Trace.WriteLine(" ");
            }
        }
    }
    public class mQuadrotor
    {
        /*** Local Variables ***/
        // Current variables
        public float pitch_c, roll_c, yaw_c, alt_c;
        public bool changed = false;
        // Desired values
        public float pitch_d, roll_d, yaw_d, alt_d;
        // PID Variables
        public int throttle;
        public int packetType;
        public string[] portList;
        public string comPort;
        public float pitchKi, pitchKd, pitchKp, rollKi, rollKp, rollKd, yawKi, yawKp, yawKd;
        public mQuadrotor()
        {
            comPort = null;
            packetType = 0;
            pitch_c = 0;
            roll_c = 0;
            yaw_c = 0;
            alt_c = 0;
            pitch_d = 0;
            roll_d = 0;
            yaw_d = 0;
            alt_d = 0;
            throttle = 0;
            pitchKi = 0;
            pitchKd = 0;
            pitchKp = 0;
            rollKi = 0;
            rollKp = 0;
            rollKd = 0;
            yawKi = 0;
            yawKp = 0;
            yawKd = 0;
        }
    }

    public class Language
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
