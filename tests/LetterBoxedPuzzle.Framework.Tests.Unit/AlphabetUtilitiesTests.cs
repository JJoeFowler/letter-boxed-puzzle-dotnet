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
        ///     Verifies whether given a lowercase or uppercase letter of the alphabet is determined to be a valid alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsAlphabetLetter_GivenLowercaseOrUppercaseAlphabetLetter_IsTrue()
        {
            // Arrange
            var alphabetLetters = LowerCaseAlphabet.Union(UpperCaseAlphabet).ToArray();

            // Act
            var actualIsAlphabetLetterValues = alphabetLetters.Select(character => IsAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < alphabetLetters.Length; index++)
            {
                Assert.IsTrue(actualIsAlphabetLetterValues[index], $"Expected '{alphabetLetters[index]}' to be an alphabet letter.");
            }
        }

        /// <summary>
        ///     Verifies whether given a non-alphabet character is determined not to be a valid alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsAlphabetLetter_GivenNonAlphabetLetter_IsTrue()
        {
            // Arrange
            var allCharacters = Enumerable.Range(0, char.MaxValue).Select(x => (char)x).ToArray();
            var nonAlphabetLetters = allCharacters.Except(LowerCaseAlphabet).Except(UpperCaseAlphabet).ToArray();

            // Act
            var actualIsAlphabetLetterValues = nonAlphabetLetters.Select(character => IsAlphabetLetter(character)).ToArray();

            // Assert
            for (var index = 0; index < nonAlphabetLetters.Length; index++)
            {
                Assert.IsFalse(actualIsAlphabetLetterValues[index], $"Expected '{nonAlphabetLetters[index]}' not to be an alphabet letter.");
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
            for (var index = 0; index < AlphabetConstants.EnglishAlphabetSize; index++)
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
            var actualAlphabetBitMasks = LowerCaseAlphabet.Select(character => GetAlphabetBitMask(character)).ToArray();

            // Assert
            for (var index = 0; index < AlphabetConstants.EnglishAlphabetSize; index++)
            {
                var character = LowerCaseAlphabet[index];
                var expectedBitMask = expectedAlphabetBitMasks[index];
                var actualBitMask = actualAlphabetBitMasks[index];

                Assert.AreEqual(
                    expectedBitMask,
                    actualBitMask,
                    $"Expected the alphabet bit mask for '{character} to be '{expectedBitMask}' instead of '{actualBitMask}'.");
            }
        }

        /// <summary>
        ///     Verifies whether given the empty that the default alphabet bit mask <see cref="AlphabetBitMask.None"/> is returned.
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
    }
}