using System;
using System.Text;

namespace Encoding1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string message = "Hej med dig, håber det går godt";

            // Converts message to byte array
            byte[] bytes = Encoding.UTF8.GetBytes(message);

            // Loops through each byte in the byte array
            foreach (var b in bytes)
            {
                // Prints the byte
                Console.WriteLine(b);
            }

            // Converts bytes to string
            string converted = Encoding.UTF8.GetString(bytes);

            // Prints the message
            Console.WriteLine(converted);
        }
    }
}
