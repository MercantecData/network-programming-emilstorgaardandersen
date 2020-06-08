using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Encoding2
{
    public class Client
    {
        public Client(int port)
        {
            Console.WriteLine("Skriv serverens ip adresse");
            string serverIP = Console.ReadLine();

            while (true)
            {
                // Connects to server
                TcpClient client = Connect(serverIP, port);

                Console.WriteLine("Skriv din besked");
                string text = Console.ReadLine();

                // Send message to server
                SendMessage(text, client);

                // Start listener
                TcpListener listener = StartListener(port);

                // Get message from server
                GetMessageFromStream(listener);
            }
        }

        static TcpClient Connect(string serverIP, int port)
        {
            // Creates a client
            TcpClient client = new TcpClient();

            // Creates an endpoint
            IPAddress ip = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // Connects to client
            client.Connect(endPoint);
            return client;
        }

        static void SendMessage(string text, TcpClient client)
        {
            // Creates stream
            NetworkStream stream = client.GetStream();

            // Creates buffer
            byte[] buffer = Encoding.UTF8.GetBytes(text); // Usin UTF8

            // Writes to stream
            stream.Write(buffer, 0, buffer.Length);
        }

        static TcpListener StartListener(int port)
        {
            // Creates a listener, thats listens on every ip address, on the followering port
            IPAddress ip = IPAddress.Any;
            IPEndPoint endpoint = new IPEndPoint(ip, port);
            TcpListener listener = new TcpListener(endpoint);

            // Starts the listener
            listener.Start();
            return listener;
        }

        static void GetMessageFromStream(TcpListener listener)
        {
            // Creates client
            TcpClient client = listener.AcceptTcpClient();

            // Creates stream
            NetworkStream stream = client.GetStream();

            // Creates buffer
            byte[] buffer = new byte[255];

            // Gets number of bytes
            int numberOfBytes = stream.Read(buffer, 0, 255);

            // Converts message to string
            string converted = Encoding.UTF8.GetString(buffer, 0, numberOfBytes);

            // Prints the converted message, in green text
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(converted);
            Console.ResetColor();

            listener.Stop();
        }
    }
}
