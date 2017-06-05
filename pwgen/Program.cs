using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace pwgen
{
    class Program
    {
        static void Main(string[] args)
        {
            bool interactive = false;
            while (args.Length < 1)
            {
                Console.WriteLine("Arguments: <number of characters in pw> [additional characters]");
                Console.WriteLine("Waiting for input...");
                interactive = true;
                args = Console.ReadLine()?.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                if (args == null)
                    return;
            }

            int count = 0;
            try
            {
                count = Convert.ToInt32(args[0]);
            }
            catch(FormatException e) { }
            catch(OverflowException e) { }

            if (count <= 0)
            {
                Console.WriteLine("pw must have at least 1 character");
            }
            else
            {
                string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string additionalChars = "";
                for (int i = 1; i < args.Length; ++i)
                {
                    additionalChars += args[i];
                }
                additionalChars = string.Join("", additionalChars.Distinct().Except(chars));
                if (additionalChars.Length > 0)
                    Console.WriteLine("These additional chars were accepted: " + additionalChars);
                chars += additionalChars;
                StringBuilder stringBuilder = new StringBuilder(count);
                Random random = new Random();
                for (int i = 0; i < count; ++i)
                {
                    stringBuilder.Append(chars[random.Next(0, chars.Length)]);
                }
                Console.WriteLine(stringBuilder.ToString());
            }

            if (interactive)
                Console.ReadLine();
        }
    }
}