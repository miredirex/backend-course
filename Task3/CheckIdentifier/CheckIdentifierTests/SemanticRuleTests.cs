using CheckIdentifier;
using NUnit.Framework;

namespace CheckIdentifierTests
{
    public class SemanticRuleTests
    {
        [TestCase("abc")]
        [TestCase("abc123")]
        [TestCase("helloworld")]
        [TestCase("q1w2e3r4")]
        public void Identifier_Matches_Rule(string sample)
        {
            Assert.True(SemanticRule.MatchesRule(sample).HasPassed);
        }

        [TestCase("abc_")]
        [TestCase("_")]
        [TestCase("///")]
        [TestCase("123")]
        [TestCase("123abc")]
        [TestCase("abc123.4")]
        [TestCase("0+1")]
        public void Identifier_DoesntMatch_Rule(string sample)
        {
            Assert.False(SemanticRule.MatchesRule(sample).HasPassed);
        }

        [Test]
        public void NullIdentifier_ThrowsException()
        {
            Assert.False(SemanticRule.MatchesRule(null).HasPassed);
        }
    }
}