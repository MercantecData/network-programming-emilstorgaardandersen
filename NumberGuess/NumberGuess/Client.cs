using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Async
{
    public class Client
    {
        public Client(int port, string serverIP)
        {
            TcpClient client = new TcpClient();

            IPAddress ip = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);

            Console.WriteLine("Guess a number between 0-100");

            while (true)
            {
                Console.Write("Write your guess here: ");
                string text = Console.ReadLine();
                SendMessage(text, stream);
            }

            //client.Close();
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

        static void SendMessage(string text, NetworkStream stream)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
