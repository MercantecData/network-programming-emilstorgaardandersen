using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Async
{
    public class Server
    {
        public List<TcpClient> clients = new List<TcpClient>();
        public Server(int port)
        {
            TcpListener listener = StartListener(port);

            Console.WriteLine("Awaiting Clients...");

            AcceptClients(listener);

            while (true)
            {
                Console.Write("Write your message here: ");
                string text = Console.ReadLine();
                SendMessage(text, clients);
            }

            //Console.ReadKey();
            listener.Stop();
        }

        public async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];

            while (true)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nClient writes: " + receivedMessage);
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

        static void SendMessage(string text, List<TcpClient> clients)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            foreach (TcpClient client in clients)
            {
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        public async void AcceptClients(TcpListener listener)
        {
            bool isRunning = true;

            while (isRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                ReceiveMessage(stream);
            }
        }
    }
}
