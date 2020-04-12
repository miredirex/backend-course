using System;

namespace CheckIdentifier
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Invalid amount of arguments. Required: 1, got: {args.Length}");
                return;
            }

            var identifier = args[0];
            SemanticRule.MatchResult identifierMatch = SemanticRule.MatchesRule(identifier);

            Console.WriteLine(identifierMatch.HasPassed ? "Yes" : "No");
            if (!identifierMatch.HasPassed)
            {
                Console.WriteLine(identifierMatch.FailMessage);
            }
        }
    }
}