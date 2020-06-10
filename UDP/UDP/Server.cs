using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDP
{
    public class Server
    {
        // Constructor
        public Server(int port)
        {
            Receiver(port);

            // Keeps application running
            Console.ReadLine();
        }

        public static async void Receiver(int port)
        {
            Console.WriteLine("Awaiting Clients...");

            // Creates endpoint 
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);

            // Creates client
            UdpClient client = new UdpClient(endpoint);

            UdpReceiveResult result = await client.ReceiveAsync();

            // Gets byte array
            byte[] buffer = result.Buffer;

            // Converts bytes to string
            string text = Encoding.UTF8.GetString(buffer);

            Console.WriteLine("Received: " + text);
        }
    }
}
