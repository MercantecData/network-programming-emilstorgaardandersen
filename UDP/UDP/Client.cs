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
            UdpClient client = new UdpClient();

            string text = "Hello UDP!";
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(serverIP), 1234);

            client.Send(bytes, bytes.Length, endpoint);
        }
    }
}
