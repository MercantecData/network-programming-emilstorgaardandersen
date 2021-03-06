﻿using System;

namespace Async
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hvilken port??");

            // Gets port from console
            int port = Convert.ToInt32(Console.ReadLine());

            bool running = true;

            // Shows menu
            Menu();

            while (running)
            {
                // Waits for input from user
                string input = Console.ReadLine();

                // Clear console
                Console.Clear();

                // Shows menu
                Menu();

                if (input == "1")
                {
                    new Server(port);
                }
                else if (input == "2")
                {
                    new Client(port);
                }
                else if (input == "3")
                {
                    running = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du skulle skrive 1, 2 eller 3!!!!!!!!!");
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
