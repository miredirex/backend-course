using System;
using System.Linq;

namespace PasswordStrength
{
    public static class PasswordAnalyzer
    {
        
        public struct PasswordMetadata
        {
            public int NumbersCount;
            public int UppercaseLettersCount;
            public int LowercaseLettersCount;
            public int RepeatingCharsCount;

            public bool IsOnlyConsistsOfLetters;
            public bool IsOnlyConsistsOfDigits;
        }
        
        public static int Analyze(string password)
        {
            var len = password.Length;
            var strength = 0;

            var meta = ExtractPasswordMetadata(password);

            strength = CalculateStrengthByLength(len)
                     + CalculateStrengthByNumberCount(meta)
                     + CalculateStrengthByRepeatingChars(meta)
                     + CalculateStrengthByUppercaseLetters(meta, len)
                     + CalculateStrengthByLowercaseLetters(meta, len)
                     + CalculateStrengthByCharConsistency(meta, len);

            return strength;
        }

        private static int CalculateStrengthByLength(int length)
        {
            return 4 * length;
        }

        private static int CalculateStrengthByNumberCount(PasswordMetadata meta)
        {
            return 4 * meta.NumbersCount;
        }

        private static int CalculateStrengthByRepeatingChars(PasswordMetadata meta)
        {
            return -meta.RepeatingCharsCount;
        }

        private static int CalculateStrengthByUppercaseLetters(PasswordMetadata meta, int length)
        {
            if (meta.UppercaseLettersCount > 0)
            {
                return (length - meta.UppercaseLettersCount) * 2;
            }

            return 0;
        }
        
        private static int CalculateStrengthByLowercaseLetters(PasswordMetadata meta, int length)
        {
            if (meta.LowercaseLettersCount > 0)
            {
                return (length - meta.LowercaseLettersCount) * 2;
            }

            return 0;
        }

        private static int CalculateStrengthByCharConsistency(PasswordMetadata meta, int length)
        {
            var value = 0;
            
            if (meta.IsOnlyConsistsOfLetters) value -= length;
            if (meta.IsOnlyConsistsOfDigits) value -= length;
            
            return value;
        }

        public static PasswordMetadata ExtractPasswordMetadata(string password)
        {
            var meta = new PasswordMetadata
            {
                IsOnlyConsistsOfDigits = true,
                IsOnlyConsistsOfLetters = true
            };

            string repeatingChars = "";

            foreach (char c in password)
            {
                if (char.IsLower(c)) meta.LowercaseLettersCount++;
                if (char.IsUpper(c)) meta.UppercaseLettersCount++;

                if (c.IsLatinLetter())
                {
                    meta.IsOnlyConsistsOfDigits = false;
                }
                else if (char.IsDigit(c))
                {
                    meta.NumbersCount++;
                    meta.IsOnlyConsistsOfLetters = false;
                }
                else // If not a latin letter or digit
                {
                    throw new ArgumentException($"Password contains invalid character: {c}");
                }

                if (repeatingChars.Contains(c))
                {
                    meta.RepeatingCharsCount++;
                    
                    // If the current character is met the second time, add +1 to count once
                    var occurrences = repeatingChars.Count(chr => (c == chr));
                    if (occurrences == 1)
                    {
                        meta.RepeatingCharsCount++;
                    }
                }

                repeatingChars += c;
            }

            return meta;
        }

        private static bool IsLatinLetter(this char ch)
        {
            return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
        }
    }
}