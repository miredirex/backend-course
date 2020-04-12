using System;
using System.Linq;

namespace PasswordStrength
{
    public static class PasswordAnalyzer
    {
        public static int Analyze(string password)
        {
            var len = password.Length;
            var strength = 0;

            var meta = ExtractPasswordMetadata(password);

            strength = strength
                       + 4 * len
                       + 4 * meta.NumbersCount
                       - meta.RepeatingCharsCount;

            if (meta.UppercaseLettersCount > 0)
            {
                strength += (len - meta.UppercaseLettersCount) * 2;
            }

            if (meta.LowercaseLettersCount > 0)
            {
                strength += (len - meta.LowercaseLettersCount) * 2;
            }

            if (meta.IsOnlyConsistsOfLetters) strength -= len;
            if (meta.IsOnlyConsistsOfDigits) strength -= len;

            return strength;
        }
        
        public struct PasswordMetadata
        {
            public int NumbersCount { get; set; }
            public int UppercaseLettersCount { get; set; }
            public int LowercaseLettersCount { get; set; }
            public int RepeatingCharsCount { get; set; }

            public bool IsOnlyConsistsOfLetters { get; set; }
            public bool IsOnlyConsistsOfDigits { get; set; }
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