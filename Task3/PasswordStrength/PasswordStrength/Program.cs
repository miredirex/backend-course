using System;

namespace PasswordStrength
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No password to analyze.\nUsage: PasswordStrength.exe <password>");
            }

            var password = args[0];
            
            try
            {
                var passwordStrength = PasswordAnalyzer.Analyze(password);
                Console.WriteLine(passwordStrength);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}