using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace Arduino
{

    enum CMD: byte
    {
        TEST = 32,
        START,
        STOP,
        SETSPEED,
        REQSPEED,
        SETP,
        SETI,
        SETD,
        REQCSV_PID      //return a comma separated list of values
    };

    enum STATE {Ready = 0, Running, Locked, Unlocked}       //2 bit
    enum EVENT {Start = 0, Stop, Lock, Unlock}              //2 bit

    public partial class Form1 : Form
    {
        string SIGNATURE = "\x0E\x0E";
        string TERMINAL = "\n";
        STATE state = STATE.Ready;
        Task task = null;

        public Form1()
        {
            InitializeComponent();
            serialPort1.Open();
            serialPort1.DiscardInBuffer();  //clear anything
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer1 != null) timer1.Dispose();
            if (task != null) task.Dispose();
            if (serialPort1 != null) serialPort1.Close();
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
                    timer1.Start();
                    state = STATE.Running;
                    SendCommand(CMD.SETSPEED);
                    SendCommand(CMD.START);
                    AsyncColor(btnStart, Color.Orange); 
                    Task task = Task.Delay(5000).ContinueWith(t => ProcessEvent(EVENT.Lock));
                    break;
                case EVENT.Stop:
                    if (this.task != null)
                    {
                        this.task.Dispose();
                        this.task = null;
                    }
                    timer1.Stop();
                    state = STATE.Ready;
                    SendCommand(CMD.STOP);
                    AsyncDisable(this.btnSetSpeed, false);
                    AsyncColor(btnStart, default(Color) );
                    AsyncText(btnStart, "Start");
                    break;
                case EVENT.Lock:
                    state = STATE.Locked;
                    AsyncText(btnStart, "Unlock");
                    AsyncColor(btnStart, Color.Red );
                    AsyncDisable(this.btnSetSpeed);
                    break;
                case EVENT.Unlock:
                    state = STATE.Unlocked;
                    AsyncText(btnStart, "Lock");
                    AsyncColor(btnStart, Color.Orange);

                    AsyncDisable(this.btnSetSpeed, false);
                    break;
            }
            //New state
            AsyncText(toolStripStatusLabel1, state.ToString());
        }

        private void AsyncText(Control obj, string text, bool append = false)
        {
            if (append)
            {
                obj.Invoke(new Action<string>(n => obj.Text += n), new object[] { text });
            }
            else
            {
                obj.Invoke(new Action<string>(n => obj.Text = n), new object[] { text });
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
            obj.Invoke(new Action( () => obj.BackColor = color) );
        }

        private void AsyncDisable(Control obj, bool disable = true)
        {
            obj.Invoke(new Action(() => obj.Enabled = !disable));
        }

        void SendCommand(CMD cmd)
        {
            string data = "";
            switch ((CMD)cmd)
            {
                case CMD.SETSPEED:
                    data = nudDesireSpeed.Value.ToString();
                    break;
                case CMD.SETP:
                    data = nudP.Value.ToString();
                    break;
                case CMD.SETI:
                    data = nudI.Value.ToString();
                    break;
                case CMD.SETD:
                    data = nudD.Value.ToString();
                    break;
            }

            string packet = SerialEventsController.EncodePacket((byte)cmd, data);

            if (serialPort1.IsOpen)
            {
                try
                {
                    AsyncText(lblPacketOut, cmd + " " + data);

                    serialPort1.Write(packet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    string packet = serialPort1.ReadLine();
                    HandlePacket(packet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Serial Port Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        void HandlePacket(string packet)
        {
            string trimmed = packet.TrimEnd(Environment.NewLine.ToCharArray());
            if (SerialEventsController.DecodePacket(trimmed, out byte cmd, out string data) != 0) return;

            AsyncText(lblPacketIn, (CMD)cmd + " " + data);

            switch ((CMD)cmd)
            {
                case CMD.REQSPEED:
                    AsyncText(lblCurrentSpeed, data );
                    break;
                case CMD.REQCSV_PID:
                    string[] pid = data.Split(',');
                    if (pid.Length != 3) return;
                    AsyncNUD(nudP, Decimal.Parse(pid[0]) );
                    AsyncNUD(nudI, Decimal.Parse(pid[1]) );
                    AsyncNUD(nudD, Decimal.Parse(pid[2]) );
                    break;
                case CMD.TEST:
                    MessageBox.Show(data);
                    break;
                default:
                    AsyncText(toolStripStatusLabel1, "Unknown CMD: " + cmd.ToString() );
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
            SendCommand(CMD.SETSPEED);
            //now request it
            SendCommand(CMD.REQSPEED);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendCommand(CMD.REQSPEED);
        }

        private void nudP_ValueChanged(object sender, EventArgs e)
        {
            SendCommand(CMD.SETP);
        }

        private void nudI_ValueChanged(object sender, EventArgs e)
        {
            SendCommand(CMD.SETI);
        }

        private void nudD_ValueChanged(object sender, EventArgs e)
        {
            SendCommand(CMD.SETD);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get current device state
            SendCommand(CMD.REQSPEED);
            SendCommand(CMD.REQCSV_PID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendCommand(CMD.TEST);
        }
    }
}

