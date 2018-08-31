namespace Arduino
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxPort = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxLogPath = new System.Windows.Forms.TextBox();
            this.tbxHistory = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudD = new System.Windows.Forms.NumericUpDown();
            this.nudI = new System.Windows.Forms.NumericUpDown();
            this.nudP = new System.Windows.Forms.NumericUpDown();
            this.lblPulseDelay = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMaxRotorPeriod = new System.Windows.Forms.Label();
            this.lblMinRotorPeriod = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.nudDesireSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentSpeed = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDesireSpeed)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 255);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(508, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.LightBlue;
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.cbxPort);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.tbxLogPath);
            this.tabPage3.Controls.Add(this.tbxHistory);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(498, 202);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Port";
            // 
            // cbxPort
            // 
            this.cbxPort.FormattingEnabled = true;
            this.cbxPort.Location = new System.Drawing.Point(72, 18);
            this.cbxPort.Name = "cbxPort";
            this.cbxPort.Size = new System.Drawing.Size(121, 21);
            this.cbxPort.TabIndex = 14;
            this.cbxPort.SelectedIndexChanged += new System.EventHandler(this.cbxPort_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Image = global::Arduino.Properties.Resources.FolderBrowserDialogControl_678_24;
            this.button1.Location = new System.Drawing.Point(233, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 24);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Logs";
            // 
            // tbxLogPath
            // 
            this.tbxLogPath.Location = new System.Drawing.Point(72, 52);
            this.tbxLogPath.Name = "tbxLogPath";
            this.tbxLogPath.Size = new System.Drawing.Size(155, 20);
            this.tbxLogPath.TabIndex = 11;
            // 
            // tbxHistory
            // 
            this.tbxHistory.Location = new System.Drawing.Point(291, 3);
            this.tbxHistory.Multiline = true;
            this.tbxHistory.Name = "tbxHistory";
            this.tbxHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxHistory.Size = new System.Drawing.Size(192, 175);
            this.tbxHistory.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightBlue;
            this.tabPage1.Controls.Add(this.btnReset);
            this.tabPage1.Controls.Add(this.lblState);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.lblPulseDelay);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.lblMaxRotorPeriod);
            this.tabPage1.Controls.Add(this.lblMinRotorPeriod);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Controls.Add(this.btnSetSpeed);
            this.tabPage1.Controls.Add(this.nudDesireSpeed);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lblCurrentSpeed);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(498, 202);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Control";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(326, 39);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 30;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(414, 44);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(36, 13);
            this.lblState.TabIndex = 29;
            this.lblState.Text = "(state)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudD);
            this.groupBox1.Controls.Add(this.nudI);
            this.groupBox1.Controls.Add(this.nudP);
            this.groupBox1.Location = new System.Drawing.Point(324, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 115);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotor Control";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "I";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "D";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "P";
            // 
            // nudD
            // 
            this.nudD.DecimalPlaces = 2;
            this.nudD.Location = new System.Drawing.Point(54, 82);
            this.nudD.Name = "nudD";
            this.nudD.Size = new System.Drawing.Size(72, 20);
            this.nudD.TabIndex = 31;
            this.nudD.ValueChanged += new System.EventHandler(this.nudD_ValueChanged);
            // 
            // nudI
            // 
            this.nudI.DecimalPlaces = 2;
            this.nudI.Location = new System.Drawing.Point(53, 51);
            this.nudI.Name = "nudI";
            this.nudI.Size = new System.Drawing.Size(73, 20);
            this.nudI.TabIndex = 30;
            this.nudI.ValueChanged += new System.EventHandler(this.nudI_ValueChanged);
            // 
            // nudP
            // 
            this.nudP.DecimalPlaces = 2;
            this.nudP.Location = new System.Drawing.Point(53, 21);
            this.nudP.Name = "nudP";
            this.nudP.Size = new System.Drawing.Size(73, 20);
            this.nudP.TabIndex = 29;
            this.nudP.ValueChanged += new System.EventHandler(this.nudP_ValueChanged);
            // 
            // lblPulseDelay
            // 
            this.lblPulseDelay.AutoSize = true;
            this.lblPulseDelay.BackColor = System.Drawing.Color.White;
            this.lblPulseDelay.Location = new System.Drawing.Point(160, 150);
            this.lblPulseDelay.MinimumSize = new System.Drawing.Size(30, 18);
            this.lblPulseDelay.Name = "lblPulseDelay";
            this.lblPulseDelay.Size = new System.Drawing.Size(30, 18);
            this.lblPulseDelay.TabIndex = 27;
            this.lblPulseDelay.Text = "    ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Pulse delay (us)";
            // 
            // lblMaxRotorPeriod
            // 
            this.lblMaxRotorPeriod.AutoSize = true;
            this.lblMaxRotorPeriod.BackColor = System.Drawing.Color.White;
            this.lblMaxRotorPeriod.Location = new System.Drawing.Point(198, 110);
            this.lblMaxRotorPeriod.MinimumSize = new System.Drawing.Size(30, 18);
            this.lblMaxRotorPeriod.Name = "lblMaxRotorPeriod";
            this.lblMaxRotorPeriod.Size = new System.Drawing.Size(30, 18);
            this.lblMaxRotorPeriod.TabIndex = 25;
            this.lblMaxRotorPeriod.Text = "     ";
            // 
            // lblMinRotorPeriod
            // 
            this.lblMinRotorPeriod.AutoSize = true;
            this.lblMinRotorPeriod.BackColor = System.Drawing.Color.White;
            this.lblMinRotorPeriod.Location = new System.Drawing.Point(160, 110);
            this.lblMinRotorPeriod.MinimumSize = new System.Drawing.Size(30, 18);
            this.lblMinRotorPeriod.Name = "lblMinRotorPeriod";
            this.lblMinRotorPeriod.Size = new System.Drawing.Size(30, 18);
            this.lblMinRotorPeriod.TabIndex = 24;
            this.lblMinRotorPeriod.Text = "      ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Min/Max Rotor Period (us)";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(326, 10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.Location = new System.Drawing.Point(244, 13);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(47, 23);
            this.btnSetSpeed.TabIndex = 8;
            this.btnSetSpeed.Text = "Set";
            this.btnSetSpeed.UseVisualStyleBackColor = true;
            this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
            // 
            // nudDesireSpeed
            // 
            this.nudDesireSpeed.Location = new System.Drawing.Point(159, 15);
            this.nudDesireSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDesireSpeed.Name = "nudDesireSpeed";
            this.nudDesireSpeed.Size = new System.Drawing.Size(71, 20);
            this.nudDesireSpeed.TabIndex = 1;
            this.nudDesireSpeed.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Target Frequency (Hz)";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(407, 10);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current Frequency (Hz)";
            // 
            // lblCurrentSpeed
            // 
            this.lblCurrentSpeed.AutoSize = true;
            this.lblCurrentSpeed.BackColor = System.Drawing.Color.White;
            this.lblCurrentSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentSpeed.Location = new System.Drawing.Point(161, 50);
            this.lblCurrentSpeed.MaximumSize = new System.Drawing.Size(130, 0);
            this.lblCurrentSpeed.MinimumSize = new System.Drawing.Size(130, 0);
            this.lblCurrentSpeed.Name = "lblCurrentSpeed";
            this.lblCurrentSpeed.Size = new System.Drawing.Size(130, 37);
            this.lblCurrentSpeed.TabIndex = 5;
            this.lblCurrentSpeed.Text = "0";
            this.lblCurrentSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(506, 228);
            this.tabControl1.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(508, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.graphToolStripMenuItem.Text = "Graph";
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.graphToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 277);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDesireSpeed)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbxHistory;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblPulseDelay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMaxRotorPeriod;
        private System.Windows.Forms.Label lblMinRotorPeriod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSetSpeed;
        private System.Windows.Forms.NumericUpDown nudDesireSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrentSpeed;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudD;
        private System.Windows.Forms.NumericUpDown nudI;
        private System.Windows.Forms.NumericUpDown nudP;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxLogPath;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxPort;
    }
}

