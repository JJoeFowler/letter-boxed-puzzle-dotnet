// ===============================================================================================================================================
// <copyright file="AlphabetUtilitiesTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Enums;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static TestCommonConstants;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     Unit tests for the alphabet utility methods class.
    /// </summary>
    [TestClass]
    public class AlphabetUtilitiesTests
    {
        /// <summary>
        ///     Verifies whether given a lowercase letter of the alphabet is determined to be a valid lowercase alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsLowercaseAlphabetLetter_GivenLowercaseAlphabetLetter_IsTrue()
        {
            // Arrange
            var lowercaseAlphabetLetters = LowercaseAlphabet;

            // Act
            var actualValues = lowercaseAlphabetLetters.Select(character => IsLowercaseAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < lowercaseAlphabetLetters.Length; index++)
            {
                Assert.IsTrue(actualValues[index], $"Expected '{lowercaseAlphabetLetters[index]}' to be an lowercase alphabet letter.");
            }
        }

        /// <summary>
        ///     Verifies whether given a character that is not a lowercase letter of the alphabet that it is determined not to be one.
        /// </summary>
        [TestMethod]
        public void IsLowercaseAlphabetLetter_GivenNonLowercaseAlphabetLetter_IsFalse()
        {
            // Arrange
            var nonLowercaseAlphabetLetters = NonAlphabetCharacters.Except(LowercaseAlphabet).ToArray();

            // Act
            var actualValues = nonLowercaseAlphabetLetters.Select(character => IsLowercaseAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < nonLowercaseAlphabetLetters.Length; index++)
            {
                Assert.IsFalse(actualValues[index], $"Expected '{nonLowercaseAlphabetLetters[index]}' not to be a lowercase alphabet letter.");
            }
        }

        /// <summary>
        ///     Verifies whether given a uppercase letter of the alphabet is determined to be a valid uppercase alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsUppercaseAlphabetLetter_GivenUppercaseAlphabetLetter_IsTrue()
        {
            // Arrange
            var uppercaseAlphabetLetters = UppercaseAlphabet;

            // Act
            var actualValues = uppercaseAlphabetLetters.Select(character => IsUppercaseAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < uppercaseAlphabetLetters.Length; index++)
            {
                Assert.IsTrue(actualValues[index], $"Expected '{uppercaseAlphabetLetters[index]}' to be an uppercase alphabet letter.");
            }
        }

        /// <summary>
        ///     Verifies whether given a character that is not an uppercase letter of the alphabet that it is determined not to be one.
        /// </summary>
        [TestMethod]
        public void IsUppercaseAlphabetLetter_GivenNonUppercaseAlphabetLetter_IsFalse()
        {
            // Arrange
            var nonUppercaseAlphabetLetters = NonAlphabetCharacters.Except(UppercaseAlphabet).ToArray();

            // Act
            var actualValues = nonUppercaseAlphabetLetters.Select(character => IsUppercaseAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < nonUppercaseAlphabetLetters.Length; index++)
            {
                Assert.IsFalse(actualValues[index], $"Expected '{nonUppercaseAlphabetLetters[index]}' not to be an uppercase alphabet letter.");
            }
        }

        /// <summary>
        ///     Verifies whether given a lowercase or uppercase letter of the alphabet is determined to be a valid alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsAlphabetLetter_GivenLowercaseOrUppercaseAlphabetLetter_IsTrue()
        {
            // Arrange
            var alphabetLetters = LowercaseAlphabet.Union(UppercaseAlphabet).ToArray();

            // Act
            var actualValues = alphabetLetters.Select(character => IsAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < alphabetLetters.Length; index++)
            {
                Assert.IsTrue(actualValues[index], $"Expected '{alphabetLetters[index]}' to be an alphabet letter.");
            }
        }

        /// <summary>
        ///     Verifies whether given a non-alphabet character is determined not to be a valid alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsAlphabetLetter_GivenNonAlphabetLetter_IsTrue()
        {
            // Arrange
            var nonAlphabetCharacters = NonAlphabetCharacters;

            // Act
            var actualValues = nonAlphabetCharacters.Select(character => IsAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < nonAlphabetCharacters.Length; index++)
            {
                Assert.IsFalse(actualValues[index], $"Expected '{nonAlphabetCharacters[index]}' not to be an alphabet letter.");
            }
        }

        /// <summary>
        ///     Verifies whether given an extended ASCII character that the correct extended ASCII value is returned.
        /// </summary>
        [TestMethod]
        public void GetExtendedAsciiValue_GivenExtendedAsciiCharacter_ReturnsCorrectExtendedAsciiValue()
        {
            // Arrange
            var extendedAsciiCharacters = Enumerable.Range(0, byte.MaxValue).Select(x => (char)x).ToArray();
            var expectedExtendedAsciiValues = extendedAsciiCharacters.Select(character => (byte)character).ToArray();

            // Act
            var actualExtendedAsciiValues = extendedAsciiCharacters.Select(character => GetExtendedAsciiValue(character)).ToArray();

            // Assert
            for (var index = 0; index < extendedAsciiCharacters.Length; index++)
            {
                var character = extendedAsciiCharacters[index];
                var expectedValue = expectedExtendedAsciiValues[index];
                var actualValue = actualExtendedAsciiValues[index];

                Assert.AreEqual(
                    expectedValue,
                    actualValue,
                    $"Expected the extended ASCII value of '{character}' to be '{expectedValue} and not '{actualValue}'.");
            }
        }

        /// <summary>
        ///     Verifies whether given the first non-extended ASCII unicode character that an overflow exception is thrown.
        /// </summary>
        [ExpectedException(typeof(OverflowException))]
        [TestMethod]
        public void GetExtendedAsciiValue_GivenNonExtendedAsciiUnicodeCharacter_ThrowsOverflowException()
        {
            // Arrange
            const char firstNonExtendedAsciiCharacter = (char)(byte.MaxValue + 1);

            // Act
            _ = GetExtendedAsciiValue(firstNonExtendedAsciiCharacter);
        }

        /// <summary>
        ///     Verifies whether given lowercase alphabet letters that the corresponding alphabet bit mask is returned.
        /// </summary>
        [TestMethod]
        public void GetAlphabetBitMask_GivenLowercaseAlphabetLetter_ReturnsCorrespondingAlphabetBitMask()
        {
            // Arrange
            var expectedAlphabetBitMasks = AllSingleLetterAlphabetBitMasks;

            // Act
            var actualAlphabetBitMasks = LowercaseAlphabet.Select(character => GetAlphabetBitMask(character)).ToArray();

            // Assert
            for (var index = 0; index < expectedAlphabetBitMasks.Length; index++)
            {
                var character = LowercaseAlphabet[index];
                var expectedBitMask = expectedAlphabetBitMasks[index];
                var actualBitMask = actualAlphabetBitMasks[index];

                Assert.AreEqual(
                    expectedBitMask,
                    actualBitMask,
                    $"Expected the alphabet bit mask for '{character} to be '{expectedBitMask}' instead of '{actualBitMask}'.");
            }
        }

        /// <summary>
        ///     Verifies whether given the empty that the default alphabet bit mask <see cref="AlphabetBitMask.None" /> is returned.
        /// </summary>
        [TestMethod]
        public void GetAlphabetBitMask_GivenEmptyString_ReturnsNoneAlphabetBitMask()
        {
            // Arrange
            const AlphabetBitMask expectedAlphabetBitMask = AlphabetBitMask.None;

            // Act
            var actualAlphabetBitMask = GetAlphabetBitMask(string.Empty);

            // Assert
            Assert.AreEqual(expectedAlphabetBitMask, actualAlphabetBitMask);
        }

        /// <summary>
        ///     Verifies whether given a-to-z test word that the alphabet bit mask with all bits set is returned.
        /// </summary>
        [TestMethod]
        public void GetAlphabetBitMask_GivenAToZTestWord_ReturnsAlphabetBitMaskWithAllBitsSet()
        {
            // Arrange
            var expectedAlphabetBitMask = AlphabetConstants.AlphabetBitMaskWithAllBitsSet;

            // Act
            var actualAlphabetBitMask = GetAlphabetBitMask(AToZTestWord);

            // Assert
            Assert.AreEqual(expectedAlphabetBitMask, actualAlphabetBitMask);
        }

        /// <summary>
        ///     Verifies whether given simple test word that the corresponding alphabet bit mask with for all its letters is returned.
        /// </summary>
        [TestMethod]
        public void GetAlphabetBitMask_GivenSimpleTestWord_ReturnsCorrespondingAlphabetBitMask()
        {
            // Arrange
            const AlphabetBitMask expectedAlphabetBitMask = SimpleTestWordAlphabetBitMask;

            // Act
            var actualAlphabetBitMask = GetAlphabetBitMask(SimpleTestWord);

            // Assert
            Assert.AreEqual(expectedAlphabetBitMask, actualAlphabetBitMask);
        }

        /// <summary>
        ///     Verifies an argument exception will be thrown when given an at-symbol character.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAlphabetBitMask_GivenAtSymbolCharacter_ThrowsArgumentException()
        {
            // Arrange
            const char atSymbol = '@';

            // Act
            _ = GetAlphabetBitMask(atSymbol);
        }

        /// <summary>
        ///     Verifies whether given the lower case letter 'a' with size of the alphabet that sequence is the lowercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeSequence_GivenLowercaseAWithAlphabetSize_ReturnsFullLowercaseAlphabet()
        {
            // Arrange
            const int alphabetSize = 26;
            char[] expectedSequence = LowercaseAlphabet;

            // Act
            var actualSequence = GenerateAlphabeticRangeSequence('a', alphabetSize).ToArray();

            // Assert
            for (var index = 0; index < alphabetSize; index++)
            {
                var expectedCharacter = expectedSequence[index];
                var actualCharacter = actualSequence[index];

                Assert.AreEqual(
                    expectedCharacter,
                    actualCharacter,
                    $"Expected character '{expectedCharacter}' and not '{actualCharacter}' in the sequence '{string.Join("', '", actualSequence)}'.");
            }
        }

        /// <summary>
        ///     Verifies whether given the upper case letter 'A' with size of the alphabet that sequence is the uppercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeSequence_GivenUppercaseAWithAlphabetSize_ReturnsFullUppercaseAlphabet()
        {
            // Arrange
            const int alphabetSize = 26;
            char[] expectedSequence = UppercaseAlphabet;

            // Act
            var actualSequence = GenerateAlphabeticRangeSequence('A', alphabetSize).ToArray();

            // Assert
            for (var index = 0; index < alphabetSize; index++)
            {
                var expectedCharacter = expectedSequence[index];
                var actualCharacter = actualSequence[index];

                Assert.AreEqual(
                    expectedCharacter,
                    actualCharacter,
                    $"Expected character '{expectedCharacter}' and not '{actualCharacter}' in the sequence '{string.Join("', '", actualSequence)}'.");
            }
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an lowercase 'a' and a length of the alphabet size
        ///     plus one.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeSequence_GivenLowercaseAWithAlphabetSizePlusOneLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = AlphabetSize + 1;

            // Act
            _ = GenerateAlphabeticRangeSequence('a', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an lowercase 'z' and a length of two.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeSequence_GivenLowercaseZWithLengthTwo_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = 2;

            // Act
            _ = GenerateAlphabeticRangeSequence('z', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an uppercase 'A' and a length of the alphabet size
        ///     plus one.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeSequence_GivenUppercaseAWithAlphabetSizePlusOneLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = AlphabetSize + 1;

            // Act
            _ = GenerateAlphabeticRangeSequence('A', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an uppercase 'A' and a length of two.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeSequence_GivenUpperCaseZWithLengthTwo_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = 2;

            // Act
            _ = GenerateAlphabeticRangeSequence('Z', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies out of range exception will be thrown when given a zero length that an argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeSequence_GivenZeroLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int zeroLength = 0;

            // Act
            _ = GenerateAlphabeticRangeSequence('A', zeroLength);
        }

        /// <summary>
        ///     Verifies argument exception will be thrown when given an underscore character.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateAlphabeticRangeSequence_GivenUnderscoreCharacter_ThrowsArgumentException()
        {
            // Arrange
            const char underscore = '_';

            // Act
            _ = GenerateAlphabeticRangeSequence(underscore, 1);
        }

        /// <summary>
        ///     Verifies whether given the lower case letter 'a' with size of the alphabet that the text is the lowercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeAsText_GivenLowercaseAWithAlphabetSize_ReturnsFullLowercaseAlphabet()
        {
            // Arrange
            const int alphabetSize = 26;
            var expectedText = new string(LowercaseAlphabet);

            // Act
            var actualText = GenerateAlphabeticRangeAsText('a', alphabetSize).ToArray();

            // Assert
            for (var index = 0; index < alphabetSize; index++)
            {
                var expectedCharacter = expectedText[index];
                var actualCharacter = actualText[index];

                Assert.AreEqual(
                    expectedCharacter,
                    actualCharacter,
                    $"Expected character '{expectedCharacter}' and not '{actualCharacter}' in the text '{actualText}'.");
            }
        }

        /// <summary>
        ///     Verifies whether given the upper case letter 'A' with size of the alphabet that the text is the uppercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeAsText_GivenUppercaseAWithAlphabetSize_ReturnsFullUppercaseAlphabet()
        {
            // Arrange
            const int alphabetSize = 26;
            var expectedText = new string(UppercaseAlphabet);

            // Act
            var actualText = GenerateAlphabeticRangeAsText('A', alphabetSize).ToArray();

            // Assert
            for (var index = 0; index < alphabetSize; index++)
            {
                var expectedCharacter = expectedText[index];
                var actualCharacter = actualText[index];

                Assert.AreEqual(
                    expectedCharacter,
                    actualCharacter,
                    $"Expected character '{expectedCharacter}' and not '{actualCharacter}' in the text '{actualText}'.");
            }
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an lowercase 'a' and a length the size of the
        ///     alphabet plus one.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeAsText_GivenLowercaseAWithAlphabetSizePlusOneLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = AlphabetSize + 1;

            // Act
            _ = GenerateAlphabeticRangeAsText('a', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an lowercase 'z' and a length of two.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeAsText_GivenLowercaseZWithLengthTwo_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = 2;

            // Act
            _ = GenerateAlphabeticRangeAsText('z', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an uppercase 'A' and a length the size of the
        ///     alphabet plus one.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeAsText_GivenUppercaseAWithAlphabetSizePlusOneLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = AlphabetSize + 1;

            // Act
            _ = GenerateAlphabeticRangeAsText('A', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an uppercase 'A' and a length of two.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeAsText_GivenUpperCaseZWithLengthTwo_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int lengthOutOfRange = 2;

            // Act
            _ = GenerateAlphabeticRangeAsText('Z', lengthOutOfRange);
        }

        /// <summary>
        ///     Verifies out of range exception will be thrown when given a zero length that an argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateAlphabeticRangeAsText_GivenZeroLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            const int zeroLength = 0;

            // Act
            _ = GenerateAlphabeticRangeAsText('A', zeroLength);
        }

        /// <summary>
        ///     Verifies argument exception will be thrown when given an underscore character.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateAlphabeticRangeAsText_GivenUnderscoreCharacter_ThrowsArgumentException()
        {
            // Arrange
            const char underscore = '_';

            // Act
            _ = GenerateAlphabeticRangeAsText(underscore, 1);
        }
    }
}