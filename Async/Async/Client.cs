using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Async
{
    public class Client
    {
        public Client(int port)
        {
            // Gets ip adress
            Console.WriteLine("Skriv serverens ip??");
            string serverIP = Console.ReadLine();

            // Creates client
            TcpClient client = new TcpClient();

            // Creates endpoint
            IPAddress ip = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // Connects
            client.Connect(endPoint);

            // Creates stream
            NetworkStream stream = client.GetStream();

            ReceiveMessage(stream);

            while (true)
            {
                // Gets message from client and sends it to the server
                Console.Write("Write your message here: ");
                string text = Console.ReadLine();
                SendMessage(text, stream);
            }
        }

        public async void ReceiveMessage(NetworkStream stream)
        {
            // Creates a buffer
            byte[] buffer = new byte[256];

            // Keeps reading messages from server
            while (true)
            {
                // Gets number of bytes
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);

                // Converts bytes to string
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                // Prints servers message
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nServer writes: " + receivedMessage);
                Console.ResetColor();
            }
        }

        static void SendMessage(string text, NetworkStream stream)
        {
            // Sends message
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}