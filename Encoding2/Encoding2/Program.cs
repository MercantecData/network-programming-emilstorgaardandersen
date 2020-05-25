using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int port = 5002;
            IPAddress ip = IPAddress.Any;
            IPEndPoint endpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(endpoint);
            listener.Start();

            Console.WriteLine("Awaiting Clients...");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[255];

            int numberOfBytes = stream.Read(buffer, 0, 255);

            string converted = Encoding.UTF8.GetString(buffer, 0,
            numberOfBytes);

            Console.WriteLine(converted);
        }
    }
}
