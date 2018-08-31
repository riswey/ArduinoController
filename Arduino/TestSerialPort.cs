using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Driven by the client
 * 
 * When they write to me, I plan a response and set the buffer
 * 
 */

namespace Arduino
{
    public class TestSerialPort
    {
        struct State
        {
            public int running; //0=stopped,1=running,2=lockable
            public float p;
            public float i;
            public float d;
            public bool locked;
            public float target_f;
            public long min;
            public long max;
        };

        int _PulseDelay = 0;
        public int PulseDelay {
            get {
                if (!state.locked) _PulseDelay = r.Next(0, 100);
                return _PulseDelay;
            }
        }

        float _RotorFreq = 0;
        public int RotorFreq
        {
            get
            {
                _RotorFreq += (state.target_f - _RotorFreq) / 10;
                return (int)_RotorFreq;
            }
        }

        State state = new State {
            running = 0,
            p = 0,
            i = 0,
            d = 0,
            locked = false,
            target_f = 0,
            min = 1,
            max = 5
        };

        Random r = new Random();

        public Form1 parentfm;

        private string buffer = "";

        public int BaudRate { get; set; }
        public string PortName { get; set; }
        public bool IsOpen { get { return true; } }
        public void Open() { }
        public void DiscardInBuffer() { }
        public void Close() { }

        private void Send(string buf)
        {
            buffer = buf + "\n";
            parentfm.serialPort1_DataReceived(this, null);
        }

        public void Write(string packet)
        {
            //What CMD did I just get asked to do
            //sim Dave's API
            string[] data = packet.Split(' ');
            CMD cmd = Enums.CMDDecode[data[0]];

            switch(cmd){
                case CMD.START:
                    state.p = 0;
                    state.i = 0;
                    state.d = 0;
                    state.locked = false;
                    state.running = 1;
                    Task task = Task.Delay(5000).ContinueWith(t => state.running = 2);
                    Send("ACK " + Enums.CMDEncode[CMD.START]);
                    break;
                case CMD.STOP:
                    state.running = 0;
                    Send("ACK " + Enums.CMDEncode[cmd]);
                    break;
                case CMD.SETLOCK:
                    //Can't lock until state = 2
                    if (state.running == 2)
                    {
                        state.locked = true;
                        Send("ACK " + Enums.CMDEncode[cmd]);
                    }
                    break;
                case CMD.SETUNLOCK:
                    if (state.locked == true)
                    {
                        state.locked = false;
                        Send("ACK " + Enums.CMDEncode[cmd]);
                    }
                    break;
                case CMD.SETPULSEDELAY:
                    _PulseDelay = int.Parse(data[1]);
                    Send("ACK " + Enums.CMDEncode[cmd]);
                    break;
                case CMD.SETPID:
                    state.p = float.Parse(data[1]);
                    state.i = float.Parse(data[2]);
                    state.d = float.Parse(data[3]);
                    Send("ACK " + Enums.CMDEncode[cmd]);
                    break;
                case CMD.SETFREQ:
                    state.target_f = int.Parse(data[1]);
                    Send("ACK " + Enums.CMDEncode[cmd]);
                    break;

                // NO ACK
                case CMD.GETTARGETFREQ:
                    Send("FW " + state.target_f);
                    break;
                case CMD.GETROTORFREQ:
                    Send("CF " + RotorFreq);
                    break;
                case CMD.GETPULSEDELAY:
                    Send("PW " + PulseDelay);
                    break;
                case CMD.GETPID:
                    Send(String.Format("PID {0} {1} {2}", state.p, state.i, state.d));
                    break;
                case CMD.GETMINMAXPERIODS:
                    Send(String.Format("MM {0} {1}",state.min, state.max));
                    break;
                default:
                    parentfm.Msg("Unknown packet: " + cmd);
                    break;

            }
            //SendLine("CF " + r.Next(0,100).ToString() );

        }

        public string ReadLine()
        {
            return buffer;
        }

        public string[] GetPortNames()
        {
            return new string[] { "COM6" };
        }
    }
}
