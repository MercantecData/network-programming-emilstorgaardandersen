using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Encoding2
{
    public class Server
    {
        public Server(int port)
        {
            while (true)
            {
                // Starts listener
                TcpListener listener = StartListener(port);

                Console.WriteLine("Awaiting Clients...");

                // Gets message from client
                getMessageFromStream(listener);

                // Connects to client
                TcpClient client = connect("172.16.114.206", port);

                string text = "Det virkede!!!!!!!!!!";

                //Send response message to client
                sendMessage(text, client);
            }
        }

        static TcpClient connect(string serverIP, int port)
        {
            TcpClient client = new TcpClient();
            IPAddress ip = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            client.Connect(endPoint);
            return client;
        }

        static void sendMessage(string text, TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
        }

        static TcpListener StartListener(int port)
        {
            IPAddress ip1 = IPAddress.Any;
            IPEndPoint endpoint1 = new IPEndPoint(ip1, port);
            TcpListener listener = new TcpListener(endpoint1);
            listener.Start();
            return listener;
        }

        static void getMessageFromStream(TcpListener listener)
        {
            TcpClient client1 = listener.AcceptTcpClient();

            NetworkStream stream1 = client1.GetStream();
            byte[] buffer1 = new byte[255];

            int numberOfBytes = stream1.Read(buffer1, 0, 255);

            string converted = Encoding.UTF8.GetString(buffer1, 0,
            numberOfBytes);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(converted);
            Console.ResetColor();

            listener.Stop();
        }
    }
}
