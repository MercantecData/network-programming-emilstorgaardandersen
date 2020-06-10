using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Async
{
    public class Server
    {
        public List<TcpClient> clients = new List<TcpClient>();
        public Server(int port)
        {
            Random random = new Random();

            Console.WriteLine("Skriv to tal, som clienten skal gætte et random tal imellem");

            Console.Write("Tal1: ");
            int firstNum = Convert.ToInt32(Console.ReadLine());

            Console.Write("Tal2: ");
            int secondNum = Convert.ToInt32(Console.ReadLine());

            int tal = random.Next(firstNum, secondNum);

            TcpListener listener = StartListener(port);

            System.Console.WriteLine("The number is: " + tal);
            Console.WriteLine("Awaiting Clients...");

            AcceptClients(listener, tal, clients);

            string text = "Guess a number between " + firstNum + " and " + secondNum;
            SendMessage(text, clients);

            while (true)
            {
                Console.Write("Write your message here: ");
                string text1 = Console.ReadLine();
                SendMessage(text1, clients);
            }
            //Console.ReadKey();
            listener.Stop();
        }

        public async void ReceiveMessage(NetworkStream stream, int tal, List<TcpClient> clients)
        {
            byte[] buffer = new byte[256];

            int value = 0;
            int forsøg = 0;
            int liv = 10;

            while (true)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nClient guesses: " + receivedMessage);
                Console.ResetColor();

                if (liv != 0)
                {
                    // Tjek at brugeren skriver et tal
                    if (!int.TryParse(receivedMessage, out value))
                    {
                        SendMessage("Du skal skrive et tal\n", clients);
                        continue;
                    }

                    if (Convert.ToInt32(receivedMessage) == tal)
                    {
                        forsøg++;
                        System.Console.WriteLine("liv: " + liv);
                        string text = "Du gættede rigtigt. Du brugte " + forsøg + " forsøg\n";
                        SendMessage(text, clients);
                        Console.WriteLine("Clienten gættede rigtgt");
                    }
                    else if (Convert.ToInt32(receivedMessage) > tal)
                    {
                        forsøg++;
                        liv--;
                        System.Console.WriteLine("liv: " + liv);
                        string text = "Du gættede for højt. Du har " + liv + " liv tilbage\n";
                        SendMessage(text, clients);
                    }
                    else if (Convert.ToInt32(receivedMessage) < tal)
                    {
                        forsøg++;
                        liv--;
                        System.Console.WriteLine("liv: " + liv);
                        string text = "Du gættede for lavt. Du har " + liv + " liv tilbage\n";
                        SendMessage(text, clients);
                    }
                }
                else
                {
                    SendMessage("Du har ikke flere liv tilbage (DIN TABER)", clients);
                }
            }
        }

        static TcpListener StartListener(int port)
        {
            IPAddress ip1 = IPAddress.Any;
            IPEndPoint endpoint1 = new IPEndPoint(ip1, port);
            TcpListener listener = new TcpListener(endpoint1);
            listener.Start();
            return listener;
        }

        static void SendMessage(string text, List<TcpClient> clients)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            foreach (TcpClient client in clients)
            {
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        public async void AcceptClients(TcpListener listener, int tal, List<TcpClient> clients)
        {
            bool isRunning = true;

            while (isRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                ReceiveMessage(stream, tal, clients);
            }
        }
    }
}
