using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CustomNetworking;

namespace HugeSuccess
{
    class HugeSuccess
    {
        static void Main(string[] args)
        {
            // Load Resources.
            var assembly = Assembly.GetExecutingAssembly();
            var song = assembly.GetManifestResourceStream("HugeSuccess.stillalive.wav");
            var lyricsStream = assembly.GetManifestResourceStream("HugeSuccess.lyrics.txt");
            var reader = new StreamReader(lyricsStream);

            var lyrics = reader.ReadToEnd()
                               .Split('\n')
                               .Where(s => s.Length > 0)
                               .Select(s => s + '\n');

            var commands = lyrics.Select(l => l.Split(';'))
                                 .Select(l => new Command(TimeSpan.Parse(l[0]), l[1]));

            SoundPlayer sp = new SoundPlayer(song);
            Action startSong = sp.Play;

            // Establish a Server
            var tcpListener = new TcpListener(IPAddress.Any, 0x1337);
            tcpListener.Start();

            var tcpClient = new TcpClient("localhost", 0x1337);

            var serverSocket = new StringSocket(tcpListener.AcceptSocket(), new UTF8Encoding());
            var clientSocket = new StringSocket(tcpClient.Client, new UTF8Encoding());

            var client = new SuccessClient(clientSocket, new Box(1, 1, 60, 40), startSong);
            var server = new SuccessServer(serverSocket, commands, client.ReadNext);

            Console.SetWindowSize(62, 42);
            Console.ForegroundColor = ConsoleColor.Yellow;

            server.Start();

            // Prevent user from closing with keypress or writing to the console.
            Console.SetIn(new StringReader(""));
            while (true)
            {
                Thread.Sleep(1000);
                Console.ReadLine();
            }
        }
    }
}
