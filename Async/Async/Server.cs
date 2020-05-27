using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Async
{
    public class Server
    {
        public Server(int port)
        {
            TcpListener listener = StartListener(port);

            Console.WriteLine("Awaiting Clients...");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);

            while (true)
            {
                Console.Write("Write your message here: ");
                string text = Console.ReadLine();
                SendMessage(text, stream);
            }

            //Console.ReadKey();
            listener.Stop();
        }

        public async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];

            while (true)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n" + receivedMessage);
                Console.ResetColor();
            }
        }

        static TcpListener StartListener(int port)
        {
            IPAddress ip1 = IPAddress.Any;
            IPEndPoint endpoint1 = new IPEndPoint(ip1, port);
            TcpListener listener = new TcpListener(endpoint1);
            listener.Start();
            return listener;
        }

        static void SendMessage(string text, NetworkStream stream)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
