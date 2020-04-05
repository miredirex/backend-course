using System;
using CheckIdentifier;
using NUnit.Framework;

namespace CheckIdentifierTests
{
    public class SemanticRuleTests
    {
        [TestCase("abc")]
        [TestCase("123")]
        [TestCase("abc123")]
        [TestCase("123abc")]
        [TestCase("helloworld")]
        [TestCase("q1w2e3r4")]
        public void Identifier_Matches_Rule(string sample)
        {
            Assert.True(SemanticRule.MatchesRule(sample));
        }

        [TestCase("abc_")]
        [TestCase("_")]
        [TestCase("///")]
        [TestCase("abc123.4")]
        [TestCase("0+1")]
        public void Identifier_DoesntMatch_Rule(string sample)
        {
            Assert.False(SemanticRule.MatchesRule(sample));
        }

        [Test]
        public void NullIdentifier_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                SemanticRule.MatchesRule(null);
            });
        }
    }
}