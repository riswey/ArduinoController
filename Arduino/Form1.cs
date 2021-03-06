﻿using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.IO.Ports;

namespace Arduino
{
    public partial class Form1 : Form
    {
        //TestSerialPort serialPort1;
        SerialPort serialPort1 = new SerialPort();
   
        string TERMINAL = "\n";
        STATE state = STATE.Ready;
        Task task = null;
        ParameterState parameters = new ParameterState();

        public Form1()
        {
            InitializeComponent();

            //serialPort1 = new TestSerialPort(this);
            serialPort1 = new SerialPort();

            serialPort1.PortName = SearchPorts();

            serialPort1.BaudRate = 9600;
            serialPort1.Open();
            serialPort1.DiscardInBuffer();  //clear anything

        }

        private string SearchPorts()
        {
            string[] ports = SerialPort.GetPortNames();

            cbxPort.Items.Clear();

            foreach (string port in ports)
            {
                cbxPort.Items.Add(port);
            }

            if (cbxPort.Items.Count > 0)
            {
            } else {
                cbxPort.Items.Add("(None)");
            }

            cbxPort.SelectedIndex = cbxPort.Items.Count - 1;

            return cbxPort.SelectedItem.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer1 != null) timer1.Dispose();
            //if (task != null) task.Dispose();
            if (serialPort1 != null) serialPort1.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get current device state
            SendCommand(CMD.GETPID);
            AsyncText(lblState, state.ToString());
        }

        private void ProcessEvent(EVENT e) {
            switch (e)
            {
                case EVENT.Start:
                    switch (state)
                    {
                        case STATE.Ready:
                            ChangeState(EVENT.Start);
                            break;
                        case STATE.Lockable:
                            ChangeState(EVENT.Lock);
                            break;
                        case STATE.Locked:
                            ChangeState(EVENT.Unlock);
                            break;
                    }
                    break;

                case EVENT.Lock:
                    switch (state)
                    {
                        case STATE.Lockable:
                            ChangeState(EVENT.Lock);
                            break;
                    }
                    break;
                case EVENT.Unlock:
                    switch (state)
                    {
                        case STATE.Locked:
                            ChangeState(EVENT.Unlock);
                            break;
                    }
                    break;
                case EVENT.Stop:
                    switch (state)
                    {
                        case STATE.Running:
                        case STATE.Lockable:
                        case STATE.Locked:
                            ChangeState(EVENT.Stop);
                            break;
                    }
                    break;
            }

        }

        private void ChangeState(EVENT e)
        {
            switch (e)
            {
                case EVENT.Start:
                    SendCommand(CMD.SETFREQ);
                    SendCommand(CMD.GETTARGETFREQ);
                    SendCommand(CMD.START);
                    break;
                case EVENT.Stop:
                    SendCommand(CMD.STOP);
                    break;
                case EVENT.Lock:
                    SendCommand(CMD.SETLOCK);
                    break;
                case EVENT.Unlock:
                    SendCommand(CMD.SETUNLOCK);
                    break;
            }
        }

        private void ProcessACK(CMD cmd)
        {
            switch (cmd)
            {
                case CMD.START:
                    timer1.Start();
                    parameters.Start();
                    parameters.StartRMTimer();
                    state = STATE.Running;
                    AsyncColor(btnStart, Color.Orange);
                    //Task task = Task.Delay(5000).ContinueWith(t => ProcessEvent(EVENT.Lock));
                    break;
                case CMD.STOP:
                    /*
                    if (this.task != null)
                    {
                        this.task.Dispose();
                        this.task = null;
                    }
                    */
                    timer1.Stop();
                    state = STATE.Ready;
                    AsyncDisable(this.btnSetSpeed, false);
                    AsyncColor(btnStart, default(Color));
                    AsyncText(btnStart, "Start");
                    break;
                case CMD.SETLOCK:
                    state = STATE.Locked;
                    AsyncText(btnStart, "Unlock");
                    AsyncColor(btnStart, Color.Red);
                    AsyncDisable(this.btnSetSpeed);
                    break;
                case CMD.SETUNLOCK:
                    state = STATE.Lockable;
                    AsyncText(btnStart, "Lock");
                    AsyncColor(btnStart, Color.Orange);
                    AsyncDisable(this.btnSetSpeed, false);
                    break;
                case CMD.SETPULSEDELAY:
                    AsyncText(toolStripStatusLabel1, "Pulse Delay set.");
                    break;
                case CMD.SETPID:
                    AsyncText(toolStripStatusLabel1, "PID set.");
                    break;
                case CMD.SETFREQ:
                    parameters.StartRMTimer();
                    AsyncText(toolStripStatusLabel1, "Target Rotor Frequency set.");
                    break;
            }

            AsyncText(lblState, state.ToString());

            //should we have a queue to time out unsuccessful async tasks
        }


        void SendCommand(CMD cmd)
        {
            string data = "";
            switch ((CMD)cmd)
            {
                case CMD.SETFREQ:
                    data = nudDesireSpeed.Value.ToString();
                    break;
                case CMD.SETPULSEDELAY:
                    //data = nudDesireSpeed.Value.ToString();
                    break;
                case CMD.SETPID:
                    data = nudP.Value.ToString();
                    data += " " + nudI.Value.ToString();
                    data += " " + nudD.Value.ToString();
                    break;
            }

            string packet = Enums.CMDEncode[cmd] + " " + data + TERMINAL;

            AsyncText(tbxHistory, packet + "\t" + cmd.ToString() + "\r\n", -1);

            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Write(packet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        //TODO: Made public for testing
        public void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string packet = serialPort1.ReadLine();
                string trimmed = packet.TrimEnd(Environment.NewLine.ToCharArray());
                HandlePacket(trimmed);
            }
        }

        void HandlePacket(string packet)
        {
            AsyncText(tbxHistory, packet + "\r\n", -1);

            string[] data = packet.Split(' ');
            DATATYPES cmd = Enums.DATATYPEDecode[data[0]];

            switch (cmd)
            {
                case DATATYPES.ACK:
                    CMD ackcmd = Enums.CMDDecode[data[1]];
                    ProcessACK(ackcmd);
                    break;
                case DATATYPES.GETPULSEDELAY:
                    parameters.pulse_delay = int.Parse(data[1]);
                    AsyncText(lblPulseDelay, data[1]);
                    break;
                case DATATYPES.GETTARGETFREQ:
                    parameters.target_speed = float.Parse(data[1]);
                    AsyncNUD(nudDesireSpeed, Decimal.Parse(data[1]));
                    break;
                case DATATYPES.GETROTORFREQ:
                    parameters.rotor_speed = float.Parse(data[1]);
                    AsyncText(lblCurrentSpeed, data[1]);
                    break;
                case DATATYPES.GETMINMAXPERIODS:
                    parameters.min_period = long.Parse(data[1]);
                    parameters.max_period = long.Parse(data[2]);
                    if (parameters.IsMMInRange())
                    {
                        AsyncText(lblMinRotorPeriod, data[1]);
                        AsyncText(lblMaxRotorPeriod, data[2]);
                    } else
                    {
                        AsyncText(lblMinRotorPeriod, "-");
                        AsyncText(lblMaxRotorPeriod, "-");
                    }
                    break;
                case DATATYPES.GETPID:
                    parameters.p = float.Parse(data[1]);
                    parameters.i = float.Parse(data[2]);
                    parameters.d = float.Parse(data[3]);
                    AsyncNUD(nudP, Decimal.Parse(data[1]) );
                    AsyncNUD(nudI, Decimal.Parse(data[2]) );
                    AsyncNUD(nudD, Decimal.Parse(data[3]) );
                    break;
                case DATATYPES.GETLOCKABLE:
                    bool lockable = bool.Parse(data[1]);
                    if (lockable)
                    {
                        /* This is a weird one.
                         * Making it unlockable is exacly the same process as Unlock
                         * Yet normally Unlock is prompted by a START press
                         * -> Serial communication by the Event handler
                         * But here the Serial Comms is result of an RL command
                         * So if true -> just need to Simulate the ACK of an SU (set unlock)
                         */
                        ProcessACK(CMD.SETUNLOCK);
                    }
                    break;
                default:
                    AsyncText(toolStripStatusLabel1, "Unknown Packet: " + packet );
                    break;
            }
        }

        private void serialPort1_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ProcessEvent(EVENT.Start);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ProcessEvent(EVENT.Stop);
        }

        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            SendCommand(CMD.SETFREQ);
        }

        int timercycle = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (state == STATE.Running)
            {
                //If started but not entered lock cycle yet -> poll to see if lockable
                SendCommand(CMD.GETLOCKABLE);
            }

            SendCommand(CMD.GETROTORFREQ);

            //Log if 4th tick AND Max/Min are meaningful
            if ((timercycle = (++timercycle) % 4) == 0 )
            {
                if (parameters.IsRMDisabled())
                {
                    //RM (min/max) disable period expired
                    SendCommand(CMD.GETMINMAXPERIODS);
                    SendCommand(CMD.GETPULSEDELAY);
                    SendCommand(CMD.GETPID);
                    SendCommand(CMD.GETTARGETFREQ);
                    parameters.Write();
                }
            }

        }

        private void nudP_ValueChanged(object sender, EventArgs e)
        {
            SendCommand(CMD.SETPID);
        }

        private void nudI_ValueChanged(object sender, EventArgs e)
        {
            SendCommand(CMD.SETPID);
        }

        private void nudD_ValueChanged(object sender, EventArgs e)
        {
            SendCommand(CMD.SETPID);
        }

        private void AsyncText(Control obj, string text, int append = 0)
        {
            if (append == 0)
            {
                obj.Invoke(new Action<string>(n => obj.Text = n), new object[] { text });
            }
            else if(append == 1)
            {
                obj.Invoke(new Action<string>(n => obj.Text += n), new object[] { text });
            }
            else
            {
                obj.Invoke(new Action<string>(n => obj.Text = n + obj.Text), new object[] { text });
            }
        }

        private void AsyncText(ToolStripLabel obj, string text)
        {
            obj.GetCurrentParent().Invoke(new Action<string>(n => obj.Text = n), new object[] { text });
        }

        private void AsyncNUD(NumericUpDown obj, decimal value)
        {
            obj.Invoke(new Action<decimal>(n => obj.Value = value), new object[] { value });
        }

        private void AsyncColor(Control obj, Color color)
        {
            obj.Invoke(new Action(() => obj.BackColor = color));
        }

        private void AsyncDisable(Control obj, bool disable = true)
        {
            obj.Invoke(new Action(() => obj.Enabled = !disable));
        }

        public void Msg(string msg)
        {
            MessageBox.Show(msg);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (tbxLogPath.Text.Trim() != "") {
                    fbd.SelectedPath = tbxLogPath.Text;
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbxLogPath.Text = fbd.SelectedPath;
                }

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ProcessACK(CMD.STOP);
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet");
        }

        private void cbxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = cbxPort.SelectedItem.ToString();
        }

    }
}

