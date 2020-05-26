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
            bool running = true;

            menu();

            while(running) {
                string input = Console.ReadLine();
                Console.Clear();
                menu();
                
                if (input == "1")
                {
                    serverFunc(port);
                }
                else if (input == "2")
                {
                    clientFunc(port);
                }
                else if (input == "3")
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Du skulle skrive 1 eller 2");
                }
            }
        }
        static void serverFunc(int port)
        {
            // receives message from client
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

            listener.Stop();

            // Returns message to client
            TcpClient client1 = new TcpClient();

            IPAddress ip1 = IPAddress.Parse("172.16.114.206");
            IPEndPoint endPoint1 = new IPEndPoint(ip1, port);
            client1.Connect(endPoint1);

            NetworkStream stream1 = client1.GetStream();

            string text = "Det virkede!!!!!!!!!!";

            byte[] buffer1 = Encoding.UTF8.GetBytes(text);

            stream1.Write(buffer1, 0, buffer1.Length);
        }

        static void clientFunc(int port)
        {
            // Sends message to server
            TcpClient client = new TcpClient();

            Console.WriteLine("Skriv serverens ip adresse");
            string serverIP = Console.ReadLine();

            IPAddress ip = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();

            Console.WriteLine("Skriv din besked");
            string text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            stream.Write(buffer, 0, buffer.Length);


            // Receives message from server
            IPAddress ip1 = IPAddress.Any;
            IPEndPoint endpoint1 = new IPEndPoint(ip1, port);

            TcpListener listener = new TcpListener(endpoint1);
            listener.Start();

            TcpClient client1 = listener.AcceptTcpClient();

            NetworkStream stream1 = client1.GetStream();

            byte[] buffer1 = new byte[255];

            int numberOfBytes = stream1.Read(buffer1, 0, 255);

            string converted = Encoding.UTF8.GetString(buffer1, 0,
            numberOfBytes);

            Console.WriteLine(converted);

            listener.Stop();
        }

        static void menu()
        {
            Console.WriteLine("Vil du være server eller client");
            Console.WriteLine("Skriv 1 for at være server");
            Console.WriteLine("Skriv 2 for at være client");
            Console.WriteLine("Skriv 3 for at lukke programmet");
        }
    }
}