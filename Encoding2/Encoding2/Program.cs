using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CallResponse
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hvilken port??");
            int port = Convert.ToInt32(Console.ReadLine());
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du skulle skrive 1 eller 2!!!!!!!!!");
                    Console.ResetColor();
                }
            }
        }
        static void serverFunc(int port)
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

        static void clientFunc(int port)
       {
            Console.WriteLine("Skriv serverens ip adresse");
            string serverIP = Console.ReadLine();

            // Connects to server
            TcpClient client = connect(serverIP, port);

            Console.WriteLine("Skriv din besked");
            string text = Console.ReadLine();

            // Send message to server
            sendMessage(text, client);

            // Start listener
            TcpListener listener = StartListener(port);

            // Get message from server
            getMessageFromStream(listener);
       }

        static void menu()
        {
            Console.WriteLine("Vil du være server eller client");
            Console.WriteLine("Skriv 1 for at være server");
            Console.WriteLine("Skriv 2 for at være client");
            Console.WriteLine("Skriv 3 for at lukke programmet");
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