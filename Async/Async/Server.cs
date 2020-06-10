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
        // Creates a list
        public List<TcpClient> clients = new List<TcpClient>();
        public Server(int port)
        {
            // Starts listener
            TcpListener listener = StartListener(port);

            Console.WriteLine("Awaiting Clients...");

            AcceptClients(listener);

            while (true)
            {
                // Sends message to client
                Console.Write("Write your message here: ");
                string text = Console.ReadLine();
                SendMessage(text, clients);
            }

            //Console.ReadKey();
            //listener.Stop();
        }

        public async void ReceiveMessage(NetworkStream stream)
        {
            // Creates a buffer
            byte[] buffer = new byte[256];

            // Keeps reading message from client
            while (true)
            {
                // Gets number of bytes
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                // Converts bytes to string
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                // Prints clients message
                Console.Write("\nClient writes: " + receivedMessage);
            }
        }

        static TcpListener StartListener(int port)
        {
            // Creates endpoint
            IPAddress ip = IPAddress.Any;
            IPEndPoint endpoint = new IPEndPoint(ip, port);
            TcpListener listener = new TcpListener(endpoint);

            // Starts listener
            listener.Start();
            return listener;
        }

        static void SendMessage(string text, List<TcpClient> clients)
        {
            // Send message
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            foreach (TcpClient client in clients)
            client.GetStream().Write(buffer, 0, buffer.Length);
        }

        public async void AcceptClients(TcpListener listener)
        {
            // Keeps adding clients
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                ReceiveMessage(stream);
            }
        }
    }
}
