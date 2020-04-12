using System.Linq;

namespace CheckIdentifier
{
    public static class SemanticRule
    {
        public struct MatchResult
        {
            public bool HasPassed { get; }
            public string FailMessage { get; }

            public MatchResult(bool hasPassed, string failMessage)
            {
                HasPassed = hasPassed;
                FailMessage = failMessage;
            }
        }

        public static MatchResult MatchesRule(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new MatchResult(false, "Provided string is empty");
            }

            for (var i = 0; i < str.Length; i++)
            {
                if (!char.IsLetterOrDigit(str[i]))
                {
                    return new MatchResult(false,
                        $"{str} does not match SR3 rule. \"{str[i]}\" is not a digit or letter.");
                }

                if (i == 0 && !char.IsLetter(str.First()))
                {
                    return new MatchResult(false, $"Identifier must start with a letter: {str[i]} is not a letter.");
                }
            }

            return new MatchResult(true, string.Empty);
        }
    }
}