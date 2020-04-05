﻿using System;

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
            var isIdentifierMatches = SemanticRule.MatchesRule(identifier, isLogError: true);
            
            Console.WriteLine(isIdentifierMatches ? "Yes" : "No");
        }
    }
}