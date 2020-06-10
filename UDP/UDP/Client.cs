using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDP
{
    public class Client
    {
        // Constructor
        public Client(int port)
        {
            Sender(port);
        }

        public static async void Sender(int port )
        {
            // Gets servers ip adress
            Console.WriteLine("Skriv serverens ip??");
            string serverIP = Console.ReadLine();

            // Creates client
            UdpClient client = new UdpClient();

            Console.Write("Write your message here: ");

            // Gets message from client
            string text = Console.ReadLine();

            // Converts message to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            // Creates endpoint
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(serverIP), port);

            await client.SendAsync(bytes, bytes.Length, endpoint);
        }
    }
}
