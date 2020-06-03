using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDP
{
    public class Client
    {
        public Client(string serverIP)
        {
            Sender(serverIP);
        }

        public static async void Sender(string serverIP)
        {
            UdpClient client = new UdpClient();

            Console.Write("Write your message here: ");
            string text = Console.ReadLine();
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(serverIP), 1234);

            await client.SendAsync(bytes, bytes.Length, endpoint);
        }
    }
}
