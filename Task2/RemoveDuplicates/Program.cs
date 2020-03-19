using System;

namespace RemoveDuplicates
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Incorrect number of arguments!");
                Console.WriteLine("Usage: remove_duplicates.exe <input string>");
                Environment.Exit(1);
            }

            var checkedCharacters = "";
            foreach (var ch in args[0])
            {
                if (checkedCharacters.Contains(ch)) continue;

                Console.Write(ch);
                checkedCharacters += ch;
            }
        }
    }
}