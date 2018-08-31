using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.IO.Ports;

namespace Arduino
{
    public partial class Form1 : Form
    {
        //TestSerialPort serialPort1 = new TestSerialPort();
        SerialPort serialPort1 = new SerialPort();
   
        string TERMINAL = "\n";
        STATE state = STATE.Ready;
        Task task = null;
        ParameterState log = new ParameterState();

        public Form1()
        {
            InitializeComponent();

            serialPort1.PortName = SearchPorts();

            serialPort1.BaudRate = 9600;
            serialPort1.Open();
            serialPort1.DiscardInBuffer();  //clear anything

            //testing
            //serialPort1.parentfm = this;

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

            int matrix = ((byte)e << 2 | (byte)state);
            //matrix 0...15            

            //enum STATE { Ready = 0000, Running    = 0001, Locked  = 0010, Unlocked    = 0011}       //2 bit
            //enum EVENT { Start = 0000, Stop       = 0100, Lock    = 1000, Unlock      = 1100}       //2 bit

            switch (matrix)
            {
                case 0:     //Start + Ready
                    ChangeState(EVENT.Start);
                    break;
                case 1:     //Start + Running
                    ChangeState(EVENT.Lock);
                    break;
                case 2:     //Start + Locked
                    ChangeState(EVENT.Unlock);
                    break;
                case 3:     //Start + UnLocked
                    ChangeState(EVENT.Lock);
                    break;
                case 5:     //Stop + Running
                case 6:     //Stop + Locked
                case 7:     //Stop + Unlocked
                    ChangeState(EVENT.Stop);
                    break;
                case 9:     //Lock + Running
                case 11:    //Lock + Unlocked
                    ChangeState(EVENT.Lock);
                    break;
                case 14:    //Unlock + Locked
                    ChangeState(EVENT.Unlock);
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
                    log.Start();
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
                    state = STATE.Unlocked;
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
                case DATATYPES.GETPULSEDELAY:
                    log.pulse_delay = int.Parse(data[1]);
                    AsyncText(lblPulseDelay, data[1]);
                    break;
                case DATATYPES.GETTARGETFREQ:
                    log.target_speed = float.Parse(data[1]);
                    AsyncNUD(nudDesireSpeed, Decimal.Parse(data[1]));
                    break;
                case DATATYPES.GETROTORFREQ:
                    log.rotor_speed = float.Parse(data[1]);
                    AsyncText(lblCurrentSpeed, data[1]);
                    break;
                case DATATYPES.GETMINMAXPERIODS:
                    log.min_period = long.Parse(data[1]);
                    log.max_period = long.Parse(data[2]);
                    AsyncText(lblMinRotorPeriod, data[1]);
                    AsyncText(lblMaxRotorPeriod, data[2]);
                    break;
                case DATATYPES.GETPID:
                    log.p = float.Parse(data[1]);
                    log.i = float.Parse(data[2]);
                    log.d = float.Parse(data[3]);
                    AsyncNUD(nudP, Decimal.Parse(data[1]) );
                    AsyncNUD(nudI, Decimal.Parse(data[2]) );
                    AsyncNUD(nudD, Decimal.Parse(data[3]) );
                    break;
                case DATATYPES.ACK:
                    CMD ackcmd = Enums.CMDDecode[data[1]];
                    ProcessACK( ackcmd );
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
            SendCommand(CMD.GETROTORFREQ);
            if ((timercycle = (++timercycle) % 4) == 0)
            {
                SendCommand(CMD.GETMINMAXPERIODS);
                SendCommand(CMD.GETPULSEDELAY);
                SendCommand(CMD.GETPID);
                SendCommand(CMD.GETTARGETFREQ);
                log.Write();
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

