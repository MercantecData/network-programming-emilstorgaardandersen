using System;
using System.Text;

namespace Encoding1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string message = "Hej med dig";

            byte[] bytes = Encoding.ASCII.GetBytes(message);

            foreach (var b in bytes)
            {
                Console.WriteLine(b);
            }
        }
    }
}
