﻿using System;

namespace Encoding2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hvilken port??");
            // Gets port from console
            int port = Convert.ToInt32(Console.ReadLine());
            bool running = true;

            // Prints the menu
            Menu();

            while (running)
            {
                // Gets input from console
                string input = Console.ReadLine();

                // Clear console
                Console.Clear();

                // Prints the menu
                Menu();

                if (input == "1")
                {
                    new Server(port);
                }
                else if (input == "2")
                {
                    while (true)
                    {
                        new Client(port);
                    }
                }
                else if (input == "3")
                {
                    running = false;
                }
                else
                {
                    // If none of the numbers from the menu, has been choosed
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