using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomNetworking;
using System.Net.Sockets;

namespace HugeSuccess
{
    class SuccessServer
    {
        private readonly StringSocket socket;
        private readonly IEnumerable<Command> commands;
        private readonly Action readNext;

        internal SuccessServer(StringSocket socket, IEnumerable<Command> commands, Action readNext)
        {
            this.socket = socket;
            this.commands = commands;
            this.readNext = readNext;
        }

        internal void Start() {
            foreach (var c in commands)
            {
                ScheduleSend(c.Message, c.Time);
            }
        }

        private async void ScheduleSend(string message, TimeSpan time) {
            readNext();
            await Task.Delay((int) time.TotalMilliseconds);
            socket.BeginSend(message, (e, p) => { }, null);
        }
    }
}
