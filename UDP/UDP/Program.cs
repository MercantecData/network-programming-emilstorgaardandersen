using System;

namespace UDP
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hvilken port??");
            // int port = Convert.ToInt32(Console.ReadLine());
            bool running = true;

            Menu();

            while (running)
            {
                string input = Console.ReadLine();
                Console.Clear();
                Menu();

                if (input == "1")
                {
                    new Server();
                }
                else if (input == "2")
                {
                    Console.WriteLine("Skriv serverens ip??");
                    string serverIP = Console.ReadLine();
                    new Client(serverIP);
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

        static void Menu()
        {
            Console.WriteLine("Vil du være server eller client");
            Console.WriteLine("Skriv 1 for at være server");
            Console.WriteLine("Skriv 2 for at være client");
            Console.WriteLine("Skriv 3 for at lukke programmet");
        }
    }
}
