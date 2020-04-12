using System;
using NUnit.Framework;
using PasswordStrength;

namespace PasswordStrengthTests
{
    public class PasswordAnalyzerTest
    {
        [Test]
        public void EmptyPassword_ShouldReturn_ZeroStrength()
        {
            Assert.AreEqual(0, PasswordAnalyzer.Analyze(""));
        }
        
        /**
         * Rules:
         * 1) total chars    * 4
         * 2) digits         * 4
         * 3) uppercase      (len - it) * 2
         * 4) lowercase      (len - it) * 2
         * 5) subtract length if only letters
         * 6) subtract length if only digits
         * 7) subtract repetitions
         */
        
        [TestCase("a",   (4 * 1) + (0 * 4) + (0) + ((1 - 1) * 2) - 1 - 0 - 0)]
        [TestCase("abc", (4 * 3) + (0 * 4) + (0) + ((3 - 3) * 2) - 3 - 0 - 0)]
        [TestCase("AAA", (4 * 3) + (0 * 4) + ((3 - 3) * 2) + (0) - 3 - 0 - 2)]
        [TestCase("a1a", (4 * 3) + (1 * 4) + (0) + ((3 - 2) * 2) - 0 - 0 - 1)]
        public void Analyze_ShouldCalculate_CorrectPasswordStrength(string password, int expectedStrength)
        {
            Assert.AreEqual(expectedStrength, PasswordAnalyzer.Analyze(password));
        }
        
        [TestCase("ExamplePassword123" /* Length: 18 */, (4 * 18) + (3 * 4) + ((18 - 2) * 2) + ((18 - 13) * 2) - 0 - 0 - 2)]
        [TestCase("AnotherAbCdEaA990" /* Length: 17 */, (4 * 17) + (3 * 4) + ((17 - 5) * 2) + ((17 - 9) * 2) - 0 - 0 - 3)]
        public void Analyze_ShouldCalculate_CorrectPasswordStrengthForLongPasswords(string password, int expectedStrength)
        {
            Assert.AreEqual(expectedStrength, PasswordAnalyzer.Analyze(password));
        }

        [TestCase("passwordWithAn_Underscore")]
        [TestCase("password@")]
        [TestCase("пароль")]
        [TestCase("密码")]
        [TestCase("hasło")]
        [TestCase("???")]
        public void Analyze_IfPasswordContainsWrongChars_ThrowException(string password)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                PasswordAnalyzer.Analyze(password);
            });
        }

        public class PasswordMetadataTests
        {
            [TestCase("UPPERCASEDPASSWORD")]
            [TestCase("HELLOWORLD")]
            public void ExtractPasswordMetadata_UppercaseLettersCount_ShouldEqualToLength(string password)
            {
                var meta = PasswordAnalyzer.ExtractPasswordMetadata(password);
                
                Assert.AreEqual(password.Length, meta.UppercaseLettersCount);
            }
            
            [TestCase("lowercasedpassword")]
            [TestCase("helloworld")]
            public void ExtractPasswordMetadata_LowercaseLettersCount_ShouldEqualToLength(string password)
            {
                var meta = PasswordAnalyzer.ExtractPasswordMetadata(password);

                Assert.IsTrue(meta.IsOnlyConsistsOfLetters);
                
                Assert.AreEqual(password.Length, meta.LowercaseLettersCount);
            }

            [TestCase("123123123")]
            [TestCase("999900")]
            public void ExtractPasswordMetadata_NumbersCount_ShouldEqualToLength(string password)
            {
                var meta = PasswordAnalyzer.ExtractPasswordMetadata(password);

                Assert.IsTrue(meta.IsOnlyConsistsOfDigits);
                
                Assert.AreEqual(password.Length, meta.NumbersCount);
            }

            [TestCase("11111111111111")]
            [TestCase("aaaaaaaaaaaaaaa")]
            [TestCase("AAAAAAAAAA")]
            [TestCase("ZZZZZZZZZZ")]
            public void ExtractPasswordMetadata_RepeatingCharsCount_ShouldEqualToLengthMinusOne(string password)
            {
                var meta = PasswordAnalyzer.ExtractPasswordMetadata(password);
                
                Assert.AreEqual(password.Length - 1, meta.RepeatingCharsCount);
            }

            [TestCase("11111111111a11")]
            [TestCase("aaaaAAAAAAAAA")]
            [TestCase("xxxxXXXXXXXX")]
            [TestCase("XXXXXXXxx")]
            public void ExtractPasswordMetadata_RepeatingCharsCount_ShouldNotEqualToLengthMinusOne(string password)
            {
                var meta = PasswordAnalyzer.ExtractPasswordMetadata(password);
                
                Assert.AreNotEqual(password.Length - 1, meta.RepeatingCharsCount);
            }
        }
    }
}