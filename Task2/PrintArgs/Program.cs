using System;

namespace PrintArgs
{
    static class Program
    {
        private const string ArgumentsSeparator = " ";

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No parameters were specified!");
                Environment.Exit(1);
            }

            Console.WriteLine($"Number of Arguments: {args.Length}");
            Console.WriteLine($"Arguments: {string.Join(ArgumentsSeparator, args)}");
        }
    }
}