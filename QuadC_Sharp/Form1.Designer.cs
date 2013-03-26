namespace QuadC_Sharp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.setBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pitchVal = new System.Windows.Forms.Label();
            this.rollVal = new System.Windows.Forms.Label();
            this.yawVal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pitchSet = new System.Windows.Forms.TextBox();
            this.rollSet = new System.Windows.Forms.TextBox();
            this.yawSet = new System.Windows.Forms.TextBox();
            this.pitchKd = new System.Windows.Forms.TextBox();
            this.pitchKi = new System.Windows.Forms.TextBox();
            this.pitchKp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.rollKp = new System.Windows.Forms.TextBox();
            this.rollKi = new System.Windows.Forms.TextBox();
            this.rollKd = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.yawKp = new System.Windows.Forms.TextBox();
            this.yawKi = new System.Windows.Forms.TextBox();
            this.yawKd = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.altSet = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.altVal = new System.Windows.Forms.Label();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.throttleSet = new System.Windows.Forms.NumericUpDown();
            this.comPortBox = new System.Windows.Forms.ComboBox();
            this.comStart = new System.Windows.Forms.Button();
            this.stopCOMBtn = new System.Windows.Forms.Button();
            this.anglesSet = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.stopBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.throttleSet)).BeginInit();
            this.SuspendLayout();
            // 
            // setBtn
            // 
            this.setBtn.Location = new System.Drawing.Point(297, 274);
            this.setBtn.Name = "setBtn";
            this.setBtn.Size = new System.Drawing.Size(75, 23);
            this.setBtn.TabIndex = 0;
            this.setBtn.Text = "Set PID";
            this.setBtn.UseVisualStyleBackColor = true;
            this.setBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pitch: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Roll: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Yaw: ";
            // 
            // pitchVal
            // 
            this.pitchVal.AutoSize = true;
            this.pitchVal.Location = new System.Drawing.Point(58, 29);
            this.pitchVal.Name = "pitchVal";
            this.pitchVal.Size = new System.Drawing.Size(35, 13);
            this.pitchVal.TabIndex = 4;
            this.pitchVal.Text = "label4";
            // 
            // rollVal
            // 
            this.rollVal.AutoSize = true;
            this.rollVal.Location = new System.Drawing.Point(140, 29);
            this.rollVal.Name = "rollVal";
            this.rollVal.Size = new System.Drawing.Size(35, 13);
            this.rollVal.TabIndex = 4;
            this.rollVal.Text = "label4";
            // 
            // yawVal
            // 
            this.yawVal.AutoSize = true;
            this.yawVal.Location = new System.Drawing.Point(225, 29);
            this.yawVal.Name = "yawVal";
            this.yawVal.Size = new System.Drawing.Size(35, 13);
            this.yawVal.TabIndex = 4;
            this.yawVal.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pitch: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Roll: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Yaw: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Current Values";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Desired Values";
            // 
            // pitchSet
            // 
            this.pitchSet.Location = new System.Drawing.Point(53, 76);
            this.pitchSet.Name = "pitchSet";
            this.pitchSet.Size = new System.Drawing.Size(34, 20);
            this.pitchSet.TabIndex = 6;
            // 
            // rollSet
            // 
            this.rollSet.Location = new System.Drawing.Point(137, 76);
            this.rollSet.Name = "rollSet";
            this.rollSet.Size = new System.Drawing.Size(34, 20);
            this.rollSet.TabIndex = 6;
            // 
            // yawSet
            // 
            this.yawSet.Location = new System.Drawing.Point(222, 76);
            this.yawSet.Name = "yawSet";
            this.yawSet.Size = new System.Drawing.Size(34, 20);
            this.yawSet.TabIndex = 6;
            // 
            // pitchKd
            // 
            this.pitchKd.Location = new System.Drawing.Point(222, 143);
            this.pitchKd.Name = "pitchKd";
            this.pitchKd.Size = new System.Drawing.Size(34, 20);
            this.pitchKd.TabIndex = 11;
            // 
            // pitchKi
            // 
            this.pitchKi.Location = new System.Drawing.Point(137, 143);
            this.pitchKi.Name = "pitchKi";
            this.pitchKi.Size = new System.Drawing.Size(34, 20);
            this.pitchKi.TabIndex = 12;
            // 
            // pitchKp
            // 
            this.pitchKp.Location = new System.Drawing.Point(53, 143);
            this.pitchKp.Name = "pitchKp";
            this.pitchKp.Size = new System.Drawing.Size(34, 20);
            this.pitchKp.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Pitch PID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(185, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Kd:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(101, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Ki: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Kp: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Kp: ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(101, 210);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Ki: ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(185, 210);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Kd:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 189);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "Roll PID";
            // 
            // rollKp
            // 
            this.rollKp.Location = new System.Drawing.Point(53, 206);
            this.rollKp.Name = "rollKp";
            this.rollKp.Size = new System.Drawing.Size(34, 20);
            this.rollKp.TabIndex = 13;
            // 
            // rollKi
            // 
            this.rollKi.Location = new System.Drawing.Point(137, 206);
            this.rollKi.Name = "rollKi";
            this.rollKi.Size = new System.Drawing.Size(34, 20);
            this.rollKi.TabIndex = 12;
            // 
            // rollKd
            // 
            this.rollKd.Location = new System.Drawing.Point(222, 206);
            this.rollKd.Name = "rollKd";
            this.rollKd.Size = new System.Drawing.Size(34, 20);
            this.rollKd.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 279);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Kp: ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(101, 279);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Ki: ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(185, 279);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 13);
            this.label19.TabIndex = 9;
            this.label19.Text = "Kd:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 254);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 13);
            this.label20.TabIndex = 10;
            this.label20.Text = "Yaw PID";
            // 
            // yawKp
            // 
            this.yawKp.Location = new System.Drawing.Point(53, 275);
            this.yawKp.Name = "yawKp";
            this.yawKp.Size = new System.Drawing.Size(34, 20);
            this.yawKp.TabIndex = 13;
            // 
            // yawKi
            // 
            this.yawKi.Location = new System.Drawing.Point(137, 275);
            this.yawKi.Name = "yawKi";
            this.yawKi.Size = new System.Drawing.Size(34, 20);
            this.yawKi.TabIndex = 12;
            // 
            // yawKd
            // 
            this.yawKd.Location = new System.Drawing.Point(222, 275);
            this.yawKd.Name = "yawKd";
            this.yawKd.Size = new System.Drawing.Size(34, 20);
            this.yawKd.TabIndex = 11;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(294, 55);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(43, 13);
            this.label21.TabIndex = 14;
            this.label21.Text = "Throttle";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(294, 119);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(42, 13);
            this.label22.TabIndex = 14;
            this.label22.Text = "Altitude";
            // 
            // altSet
            // 
            this.altSet.Location = new System.Drawing.Point(297, 141);
            this.altSet.Name = "altSet";
            this.altSet.Size = new System.Drawing.Size(75, 20);
            this.altSet.TabIndex = 15;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(276, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(45, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Altitude:";
            // 
            // altVal
            // 
            this.altVal.AutoSize = true;
            this.altVal.Location = new System.Drawing.Point(327, 29);
            this.altVal.Name = "altVal";
            this.altVal.Size = new System.Drawing.Size(35, 13);
            this.altVal.TabIndex = 4;
            this.altVal.Text = "label4";
            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(393, 274);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(75, 23);
            this.refreshBtn.TabIndex = 16;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // throttleSet
            // 
            this.throttleSet.Location = new System.Drawing.Point(297, 76);
            this.throttleSet.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.throttleSet.Name = "throttleSet";
            this.throttleSet.Size = new System.Drawing.Size(75, 20);
            this.throttleSet.TabIndex = 17;
            this.throttleSet.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // comPortBox
            // 
            this.comPortBox.FormattingEnabled = true;
            this.comPortBox.Location = new System.Drawing.Point(297, 206);
            this.comPortBox.Name = "comPortBox";
            this.comPortBox.Size = new System.Drawing.Size(75, 21);
            this.comPortBox.TabIndex = 18;
            this.comPortBox.SelectedIndexChanged += new System.EventHandler(this.comPortBox_SelectedIndexChanged);
            // 
            // comStart
            // 
            this.comStart.Location = new System.Drawing.Point(393, 205);
            this.comStart.Name = "comStart";
            this.comStart.Size = new System.Drawing.Size(75, 23);
            this.comStart.TabIndex = 19;
            this.comStart.Text = "Start COM";
            this.comStart.UseVisualStyleBackColor = true;
            this.comStart.Click += new System.EventHandler(this.comStart_Click);
            // 
            // stopCOMBtn
            // 
            this.stopCOMBtn.Location = new System.Drawing.Point(393, 227);
            this.stopCOMBtn.Name = "stopCOMBtn";
            this.stopCOMBtn.Size = new System.Drawing.Size(75, 23);
            this.stopCOMBtn.TabIndex = 20;
            this.stopCOMBtn.Text = "Stop COM";
            this.stopCOMBtn.UseVisualStyleBackColor = true;
            this.stopCOMBtn.Click += new System.EventHandler(this.stopCOMBtn_Click);
            // 
            // anglesSet
            // 
            this.anglesSet.Location = new System.Drawing.Point(393, 75);
            this.anglesSet.Name = "anglesSet";
            this.anglesSet.Size = new System.Drawing.Size(75, 23);
            this.anglesSet.TabIndex = 21;
            this.anglesSet.Text = "Set Desired";
            this.anglesSet.UseVisualStyleBackColor = true;
            this.anglesSet.Click += new System.EventHandler(this.anglesSet_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(297, 189);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "COM Port";
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(393, 141);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 23;
            this.stopBtn.Text = "STOP!!!";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 322);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.anglesSet);
            this.Controls.Add(this.stopCOMBtn);
            this.Controls.Add(this.comStart);
            this.Controls.Add(this.comPortBox);
            this.Controls.Add(this.throttleSet);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.altSet);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.yawKd);
            this.Controls.Add(this.rollKd);
            this.Controls.Add(this.pitchKd);
            this.Controls.Add(this.yawKi);
            this.Controls.Add(this.rollKi);
            this.Controls.Add(this.pitchKi);
            this.Controls.Add(this.yawKp);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.rollKp);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.pitchKp);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.yawSet);
            this.Controls.Add(this.rollSet);
            this.Controls.Add(this.pitchSet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.altVal);
            this.Controls.Add(this.yawVal);
            this.Controls.Add(this.rollVal);
            this.Controls.Add(this.pitchVal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.setBtn);
            this.Name = "Form1";
            this.Text = "Quadrotor Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.throttleSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button setBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pitchVal;
        private System.Windows.Forms.Label rollVal;
        private System.Windows.Forms.Label yawVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox pitchSet;
        private System.Windows.Forms.TextBox rollSet;
        private System.Windows.Forms.TextBox yawSet;
        private System.Windows.Forms.TextBox pitchKd;
        private System.Windows.Forms.TextBox pitchKi;
        private System.Windows.Forms.TextBox pitchKp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox rollKp;
        private System.Windows.Forms.TextBox rollKi;
        private System.Windows.Forms.TextBox rollKd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox yawKp;
        private System.Windows.Forms.TextBox yawKi;
        private System.Windows.Forms.TextBox yawKd;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox altSet;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label altVal;
        private System.Windows.Forms.Button refreshBtn;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.NumericUpDown throttleSet;
        private System.Windows.Forms.ComboBox comPortBox;
        private System.Windows.Forms.Button comStart;
        private System.Windows.Forms.Button stopCOMBtn;
        private System.Windows.Forms.Button anglesSet;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button stopBtn;
    }
}

