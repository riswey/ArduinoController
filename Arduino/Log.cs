using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino
{
    class ParameterState
    {
        public float p { get; set; }
        public float i { get; set; }
        public float d { get; set; }
        public int pulse_delay { get; set; }
        public long min_period { get; set; }
        public long max_period { get; set; }
        public float target_speed { get; set; }
        public float rotor_speed { get; set; }

        public string path { get; set; }
        private long start_t { get; set; }


        public void Start(string path = "")
        {
            this.path = path;
            doWrite("-------------------------------------------\r\nt\tTarget\tActual\tP\tI\tD\tDelay\tMin\tMax");
            start_t = DateTimeOffset.Now.UtcTicks;
        }

        private void doWrite(string str)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + "Log.dat", true))
            {
                file.WriteLine(str);
            }

        }

        public void Write()
        {
            long millisecs = (long)Math.Round((DateTimeOffset.Now.UtcTicks - start_t)/10000.0d,0);

            string str = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                millisecs,
                target_speed,
                rotor_speed, 
                p, 
                i, 
                d, 
                pulse_delay,
                min_period,
                max_period
            );

            doWrite(str);
        }

    }
}
