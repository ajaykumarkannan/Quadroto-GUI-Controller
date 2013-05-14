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

// This file has all the functions which handle the GUI interactions
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace QuadC_Sharp
{
    public partial class Form1 : Form
    {
        public mQuadrotor mQuad;
        public int deltaVal = 60;
        //Build a list
        public Form1()
        {
            mQuad = new mQuadrotor();
            InitializeComponent();
            guiInitialize();
            this.backgroundWorker1.RunWorkerAsync();
            getValsFromClass();
            updatePortList();
        }

        // Updates the list of serial ports in the list. 
        private void updatePortList()
        {
            var dataSource = new List<Language>();
            mQuad.portList = SerialPort.GetPortNames();
            for (int i = 0; i < mQuad.portList.Length; i++)
            {
                dataSource.Add(new Language() { Name = mQuad.portList[i], Value = "i" });
            }

            //Setup data binding
            this.comPortBox.DataSource = dataSource;
            this.comPortBox.DisplayMember = "Name";
            this.comPortBox.ValueMember = "Value";

            // make it readonly
            this.comPortBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Initializes all the values of the GUI
        public void guiInitialize()
        {
            pitchVal.Text = mQuad.pitch_c.ToString();
            rollVal.Text = mQuad.roll_c.ToString();
            yawVal.Text = mQuad.yaw_c.ToString();
            pitchSet.Text = mQuad.pitch_d.ToString();
            rollSet.Text = mQuad.roll_d.ToString();
            yawSet.Text = mQuad.yaw_d.ToString(); ;
            throttleSet.Value = mQuad.throttle;
            altSet.Text = mQuad.alt_d.ToString();
            altVal.Text = mQuad.alt_c.ToString();
            pitchKd.Text = mQuad.pitchKd.ToString();
            pitchKi.Text = mQuad.pitchKi.ToString();
            pitchKp.Text = mQuad.pitchKp.ToString();
            rollKd.Text = mQuad.rollKd.ToString();
            rollKi.Text = mQuad.rollKi.ToString();
            rollKp.Text = mQuad.rollKp.ToString();
            yawKd.Text = mQuad.yawKd.ToString();
            yawKi.Text = mQuad.yawKi.ToString();
            yawKp.Text = mQuad.yawKp.ToString();
            deltaValBox.Text = deltaValBox.ToString();
        }

        // Updates pitch, roll and yaw values
        public void getValsFromClass()
        {
            pitchVal.Text = mQuad.pitch_c.ToString();
            rollVal.Text = mQuad.roll_c.ToString();
            yawVal.Text = mQuad.yaw_c.ToString();
            altVal.Text = mQuad.alt_c.ToString();
        }

        // Set PID button handler
        private void button1_Click(object sender, EventArgs e)
        {
            mQuad.pitchKp = float.Parse(pitchKp.Text);
            mQuad.pitchKi = float.Parse(pitchKi.Text);
            mQuad.pitchKd = float.Parse(pitchKd.Text);
            mQuad.rollKp = float.Parse(rollKp.Text);
            mQuad.rollKi = float.Parse(rollKi.Text);
            mQuad.rollKd = float.Parse(rollKd.Text);
            mQuad.yawKp = float.Parse(yawKp.Text);
            mQuad.yawKi = float.Parse(yawKi.Text);
            mQuad.yawKd = float.Parse(yawKd.Text);
            mQuad.packetType = 2;
        }

        // Refresh button handler
        private void button1_Click_1(object sender, EventArgs e)
        {
            getValsFromClass();
            updatePortList();
        }

        // Keeps updating the GUI values
        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!mQuad.changed)
            {
                System.Threading.Thread.Sleep(10);
            }
  
        }
        // Keeps updating the GUI values
        private void backgroundWorker1_RunWorkerCompleted(
            object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (mQuad.changed)
            {
                getValsFromClass();
                mQuad.changed = false;
            }
            this.backgroundWorker1.RunWorkerAsync();
        }

        // For changes in throttle
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mQuad.throttle = (int) throttleSet.Value;
            mQuad.packetType = 1;
        }

        // Start the comm and used to interface with backend
        private void comStart_Click(object sender, EventArgs e)
        {
            Language typeItem = (Language)comPortBox.SelectedItem;
            string value = typeItem.Name.ToString();
            mQuad.comPort = value;
            Program._continue = true;
            Program.state = 1;
        }

        // Stop COM port
        private void stopCOMBtn_Click(object sender, EventArgs e)
        {
            Program._continue = false;
            Program.state = 2;
        }

        // Handle the set angles. Note that the packetType has been changed from ping to type 3. This will be reset
        // by the backend
        private void anglesSet_Click(object sender, EventArgs e)
        {
            mQuad.yaw_d = float.Parse(yawSet.Text);
            mQuad.roll_d = float.Parse(rollSet.Text);
            mQuad.pitch_d = float.Parse(pitchSet.Text);
            mQuad.alt_d = float.Parse(altSet.Text);
            mQuad.packetType = 3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Junk
        }

        private void comPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Junk
        }

        // Immediate STOP using the STOP button
        private void stopBtn_Click(object sender, EventArgs e)
        {
            mQuad.throttle = 0;
            mQuad.packetType = 1;
        }

        // The next two functions are for large changes in the throttle
        private void button1_Click_2(object sender, EventArgs e)
        {
            if (deltaVal + (int)throttleSet.Value <= 700)
            {
                mQuad.throttle = deltaVal + (int)throttleSet.Value;
                throttleSet.Value += deltaVal;
                mQuad.packetType = 1;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (-deltaVal + (int)throttleSet.Value >= 0)
            {
                mQuad.throttle = -deltaVal + (int)throttleSet.Value;
                throttleSet.Value -= deltaVal;
                mQuad.packetType = 1;
            }
        }

        // What to do when the delta value has been changed for throttle. Mainly backend
        private void deltaValBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                deltaVal = int.Parse(deltaValBox.Text);
            }
            catch (System.ArgumentException sys)
            {
                Console.WriteLine("Wrong input format to deltaVal.");
                deltaValBox.Text = deltaVal.ToString();
            }
            catch (System.FormatException sys)
            {
                Console.WriteLine("Wrong input format to deltaVal.");
                deltaValBox.Text = deltaVal.ToString();
            }
        }
   
    }
}
