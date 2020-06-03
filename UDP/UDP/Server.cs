using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDP
{
    public class Server
    {
        public Server()
        {
            Receiver();
            Console.ReadLine();
        }

        public static async void Receiver()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 1234);
            UdpClient client = new UdpClient(endpoint);

            UdpReceiveResult result = await client.ReceiveAsync();

            byte[] buffer = result.Buffer;

            string text = Encoding.UTF8.GetString(buffer);

            Console.WriteLine("Received: " + text);
        }
    }
}
