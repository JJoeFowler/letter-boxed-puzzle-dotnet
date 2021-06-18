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

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     Unit tests for the alphabet utility methods class.
    /// </summary>
    [TestClass]
    public class AlphabetUtilitiesTests
    {
        /// <summary>
        ///     The characters of the lowercase alphabet from 'a' to 'z'.
        /// </summary>
        public static readonly char[] LowerCaseAlphabet =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z',
            };

        /// <summary>
        ///     The characters of the uppercase alphabet from 'A' to 'Z'.
        /// </summary>
        public static readonly char[] UpperCaseAlphabet =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z',
            };

        public static readonly AlphabetBitMask[] AlphabetBitMasks =
            {
                AlphabetBitMask.A, AlphabetBitMask.B, AlphabetBitMask.C, AlphabetBitMask.D, AlphabetBitMask.E, AlphabetBitMask.F,
                AlphabetBitMask.G, AlphabetBitMask.H, AlphabetBitMask.I, AlphabetBitMask.J, AlphabetBitMask.K, AlphabetBitMask.L,
                AlphabetBitMask.M, AlphabetBitMask.N, AlphabetBitMask.O, AlphabetBitMask.P, AlphabetBitMask.Q, AlphabetBitMask.R,
                AlphabetBitMask.S, AlphabetBitMask.T, AlphabetBitMask.U, AlphabetBitMask.V, AlphabetBitMask.W, AlphabetBitMask.X,
                AlphabetBitMask.Y, AlphabetBitMask.Z,
            };

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
    }
}