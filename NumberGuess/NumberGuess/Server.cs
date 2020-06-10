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
        // Creates a list
        public List<TcpClient> clients = new List<TcpClient>();
        public Server(int port)
        {
            Random random = new Random();

            Console.WriteLine("Skriv to tal, som clienten skal gætte et random tal imellem");

            // Waits for the first number
            Console.Write("Tal1: ");
            int firstNum = Convert.ToInt32(Console.ReadLine());

            // Waits for the second number
            Console.Write("Tal2: ");
            int secondNum = Convert.ToInt32(Console.ReadLine());

            // Creates a random number between the first number and the second number
            int number = random.Next(firstNum, secondNum);

            // Starts listener
            TcpListener listener = StartListener(port);

            // Letting the server know wich number the client is trying to guess
            Console.WriteLine("The number is: " + number);

            Console.WriteLine("Awaiting Clients...");

            AcceptClients(listener, number, clients);

            string text = "Guess a number between " + firstNum + " and " + secondNum;
            SendMessage(text, clients);

            while (true)
            {
                // Sends message to client
                Console.Write("Write your message here: ");
                string text1 = Console.ReadLine();
                SendMessage(text1, clients);
            }
            //Console.ReadKey();
            //listener.Stop();
        }

        public async void ReceiveMessage(NetworkStream stream, int number, List<TcpClient> clients)
        {
            // Creates a buffer
            byte[] buffer = new byte[256];

            int value = 0;
            int tries = 0;
            int life = 10;

            // Keeps reading message from client
            while (true)
            {
                // Gets number of bytes
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                // Converts bytes to string
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                // Prints clients message/guess
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nClient guesses: " + receivedMessage);
                Console.ResetColor();

                // If client hasnt lost all his lves
                if (life != 0)
                {
                    // Check if user send a number
                    if (!int.TryParse(receivedMessage, out value))
                    {
                        SendMessage("You have to right a number\n", clients);
                        continue;
                    }

                    // If client guessed the number
                    if (Convert.ToInt32(receivedMessage) == number)
                    {
                        tries++;
                        Console.WriteLine("liv: " + life);
                        string text = "Congratulations!!! You guessed the number. You used " + tries + " tries\n";
                        SendMessage(text, clients);
                        Console.WriteLine("Client gues right");
                    }
                    else if (Convert.ToInt32(receivedMessage) > number) // If client guessed too high
                    {
                        tries++;
                        life--;
                        Console.WriteLine("liv: " + life);
                        string text = "You guessed too high. You have " + life + " life left\n";
                        SendMessage(text, clients);
                    }
                    else if (Convert.ToInt32(receivedMessage) < number) // If client guessed too low
                    {
                        tries++;
                        life--;
                        Console.WriteLine("liv: " + life);
                        string text = "You guessed too low. You have " + life + " life lefr\n";
                        SendMessage(text, clients);
                    }
                }
                else // Else client ran out of lifes
                {
                    SendMessage("You have used all your lifes", clients);
                }
            }
        }

        static TcpListener StartListener(int port)
        {
            // Creates endpoint
            IPAddress ip1 = IPAddress.Any;
            IPEndPoint endpoint1 = new IPEndPoint(ip1, port);
            TcpListener listener = new TcpListener(endpoint1);

            // Starts listener
            listener.Start();
            return listener;
        }

        static void SendMessage(string text, List<TcpClient> clients)
        {
            // Converts text to bytes
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            foreach (TcpClient client in clients)
            {
                // Sends message to all clients
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        public async void AcceptClients(TcpListener listener, int tal, List<TcpClient> clients)
        {
            // Keeps adding clients
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                ReceiveMessage(stream, tal, clients);
            }
        }
    }
}
