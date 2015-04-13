using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeSuccess
{
    class Command
    {
        private readonly TimeSpan time;
        public TimeSpan Time { get { return time; } }

        private readonly string message;
        public string Message { get { return message; } }

        internal Command(TimeSpan time, string message)
        {
            this.time = time;
            this.message = message;
        }
    }
}
