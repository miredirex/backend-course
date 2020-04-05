﻿using System;

namespace CheckIdentifier
{
    public static class SemanticRule
    {
        public static bool MatchesRule(string str, bool isLogError = false)
        {
            if (str == null)
            {
                throw new NullReferenceException("Parameter 'str' can't be null");
            }
            
            foreach (char c in str) if (!char.IsLetterOrDigit(c))
            {
                if (isLogError)
                {
                    Console.WriteLine($"{str} does not match SR3 rule. \"{c}\" is not a digit or letter.");
                }

                return false;
            }

            return true;
        }
    }
}