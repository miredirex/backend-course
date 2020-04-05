using NUnit.Framework;
using RemoveExtraBlanks;

namespace RemoveExtraBlanksTests
{
    public class ProgramTest
    {
        [TestCase("a  b  c  d  e  ",    "a b c d e",  Description = "Should remove repeating spaces")]
        [TestCase(" abc          123 ", "abc 123",    Description = "Should remove repeating spaces")]
        [TestCase("   abc\t\t\t123",    "abc\t123",   Description = "Should remove repeating tabs")]
        [TestCase("abc   \t\t\t   123", "abc \t 123", Description = "Should remove repeating tabs and spaces")]
        [TestCase("               ",    "",           Description = "Should be trimmed")]
        [TestCase("abc\nqwe",           "abc\nqwe",   Description = "Should not modify the source string")]
        public void RemoveExcessSeparators_ShouldDelete_ExtraSpacesAndTabsInString(string str, string expected)
        {
            Assert.AreEqual(expected, Program.RemoveExcessSeparators(str));
        }
    }
}