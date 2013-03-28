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
            sp = new SerialPort();
            initialize = new Thread(initPort);
            bytes = new Byte[20];

            _continue = false;
            state = 0;
            packet = new Byte[20];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mForm1 = new Form1();
            initialize.Start();
            Application.Run(mForm1);
            state = 3;
            _continue = false;
            state = 3;
            initialize.Join();
<<<<<<< HEAD
            if (readThread != null && readThread.IsAlive) readThread.Join();
            if (writeThread != null && writeThread.IsAlive) writeThread.Join();
=======
>>>>>>> Working
        }
        public static void initPort()
        {
            while (state != 3)
            {
                if (_continue && state == 1)
                {
                    sp.BaudRate = 57600;
                    sp.StopBits = StopBits.One;
                    sp.DataBits = 8;
                    sp.Parity = Parity.None;
                    sp.ReadTimeout = 500;
                    sp.WriteTimeout = 500;
                    sp.PortName = mForm1.mQuad.comPort;
                    Trace.WriteLine(sp.PortName);
                    // TODO: Exception if COM port access is denied
                    try
                    {
                        sp.Open();
                    }
                    catch (Exception)
                    {
                        Trace.WriteLine("Could not open COM Port.");
                        _continue = false;
                    }
                    readThread = new Thread(Read);
                    writeThread = new Thread(Write);
                    readThread.Start();
                    writeThread.Start();
                    state = 0;
                }
                else if (!_continue && state == 2)
                {
                    readThread.Join();
                    writeThread.Join();
                    sp.Close();
                    Trace.WriteLine("COM Port closed.");
                    state = 0;
                }
<<<<<<< HEAD
                else if (state == 0)
                {
                    // Just wait
                }
                else
                {
                    break;
                }
=======
                else if (state == -3) break;

>>>>>>> Working
                // Do nothing
            }
        }
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
                                if (j > 0) printData(j);
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

        public static void Write()
        {
            while (_continue)
            {
                try
                {
                    wBytes = makePacket(mForm1.mQuad.packetType);
                    if (mForm1.mQuad.packetType != 0)
                    {
                        for (int i = 0; i < wBytes.Length; i++)
                        {
                            Trace.Write(wBytes[i]);
                            Trace.Write(" ");
                        }
                        Trace.WriteLine(" ");
                    }
                    sp.Write(wBytes, 0, wBytes.Length);
                    mForm1.mQuad.packetType = 0;
                    System.Threading.Thread.Sleep(300);
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

        /* Option: 
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
                    /*** Using floats
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

        public static byte[] GetBytes(uint value)
        {
            return new byte[4] { 
                    (byte)(value & 0xFF), 
                    (byte)((value >> 8) & 0xFF), 
                    (byte)((value >> 16) & 0xFF), 
                    (byte)((value >> 24) & 0xFF) };
        }

        public static unsafe byte[] GetBytes(float value)
        {
            uint val = *((uint*)&value);
            return GetBytes(val);
        }

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
                    // printf("%d\t", (int) ((char) packet[k]));
                    if (packet[k] < 253)
                    {
                        Trace.Write(2 * temp);

                        // printf("%4d\t", 2 * temp);
                        // printf("%d\t", (int) (2 * (char) packet[k]));
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
                        // printf("%4d\t", 2 * (int)((char)(packet[k] + packet[k + 1])));
                        //printf("%d\t", (2 * ((int) packet[k] + (int) packet[k+1])));
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
