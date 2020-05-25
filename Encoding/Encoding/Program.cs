using System;
using System.Text;

namespace Encoding1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string message = "Hej med dig, håber det går godt";

            byte[] bytes = Encoding.UTF8.GetBytes(message);

            foreach (var b in bytes)
            {
                Console.WriteLine(b);
            }

            string converted = Encoding.UTF8.GetString(bytes);

            Console.WriteLine(converted);
        }
    }
}
