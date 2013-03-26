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
        }
        public void getValsFromClass()
        {
            pitchVal.Text = mQuad.pitch_c.ToString();
            rollVal.Text = mQuad.roll_c.ToString();
            yawVal.Text = mQuad.yaw_c.ToString();
            altVal.Text = mQuad.alt_c.ToString();
            /*
            pitchSet.Text = mQuad.pitch_d.ToString();
            rollSet.Text = mQuad.roll_d.ToString();
            yawSet.Text = mQuad.yaw_d.ToString(); ;
            throttleSet.Value = mQuad.throttle;
            altSet.Text = mQuad.alt_d.ToString();
            pitchKd.Text = mQuad.pitchKd.ToString();
            pitchKi.Text = mQuad.pitchKi.ToString();
            pitchKp.Text = mQuad.pitchKp.ToString();
            rollKd.Text = mQuad.rollKd.ToString();
            rollKi.Text = mQuad.rollKi.ToString();
            rollKp.Text = mQuad.rollKp.ToString();
            yawKd.Text = mQuad.yawKd.ToString();
            yawKi.Text = mQuad.yawKi.ToString();
            yawKp.Text = mQuad.yawKp.ToString();
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* Probably the most important function */
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            getValsFromClass();
            updatePortList();
        }

        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!mQuad.changed)
            {
                System.Threading.Thread.Sleep(10);
            }
  
        }
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mQuad.throttle = (int) throttleSet.Value;
            mQuad.packetType = 1;
        }

        private void comStart_Click(object sender, EventArgs e)
        {
            Language typeItem = (Language)comPortBox.SelectedItem;
            string value = typeItem.Name.ToString();
            mQuad.comPort = value;
            Program._continue = true;
            Program.state = 1;
        }

        private void stopCOMBtn_Click(object sender, EventArgs e)
        {
            Program._continue = false;
            Program.state = 2;
        }

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

        }

        private void comPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            mQuad.throttle = 0;
            mQuad.packetType = 1;
        }
   
    }
}
