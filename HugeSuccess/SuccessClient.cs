using CustomNetworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HugeSuccess
{
    class SuccessClient
    {
        private readonly StringSocket socket;
        private readonly Box box;
        private readonly Action startSong;
        private readonly object consoleLock = new Object();

        internal SuccessClient(StringSocket socket, Box box, Action startSong)
        {
            this.socket = socket;
            this.box = box;
            this.startSong = startSong;
        }

        internal void ReadNext() {
            socket.BeginReceive((s, e, p) => Process(s), null);
        }

        private void Process(string message)
        {
            lock (consoleLock)
            {
                var valid = Enumerable.Repeat(message, 1)
                                      .Where(s => s.Length > 0);

                foreach (var s in valid)
                {
                    if (s[0] == '$')
                    {
                        ProcessCommand(s);
                    }
                    else
                    {
                        foreach (var c in s)
                        {
                            Console.Write(c);
                            Thread.Sleep(20);
                        }

                        Console.SetCursorPosition(box.x + 1, Console.CursorTop + 1);
                    }
                }
            }
        }

        private void ProcessCommand(string message)
        {
            switch (message)
            {
                case "$CLEAR":
                    Drawing.Clear(box);
                    return;
                case "$LINE":
                    Console.SetCursorPosition(box.x + 1, Console.CursorTop + 1);
                    return;
                case "$START":
                    startSong();
                    return;
            }
        }
    }
}
