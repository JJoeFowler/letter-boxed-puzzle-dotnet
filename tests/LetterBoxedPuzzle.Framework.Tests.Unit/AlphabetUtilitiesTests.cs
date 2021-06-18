// ===============================================================================================================================================
// <copyright file="AlphabetUtilitiesTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System.Collections.Generic;
    using System.Linq;

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

        /// <summary>
        ///     Verifies whether given a lowercase or uppercase letter of the alphabet is determined to be a valid alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsAlphabetLetter_GivenLowercaseOrUppercaseAlphabetLetter_IsTrue()
        {
            // Arrange
            var alphabetLetters = LowerCaseAlphabet.Union(UpperCaseAlphabet);

            // Act
            var isAlphabetLetterByCharacter = alphabetLetters.Aggregate(
                new Dictionary<char, bool>(),
                (current, character) =>
                {
                    current[character] = IsAlphabetLetter(character);
                    return current;
                });

            // Assert
            foreach (var character in isAlphabetLetterByCharacter.Keys.OrderBy(x => x))
            {
                Assert.IsTrue(isAlphabetLetterByCharacter[character], $"Expected '{character}' to be an alphabet letter.");
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
            var nonAlphabetLetters = allCharacters.Except(LowerCaseAlphabet).Except(UpperCaseAlphabet);

            // Act
            var isAlphabetLetterByCharacter = nonAlphabetLetters.Aggregate(
                new Dictionary<char, bool>(),
                (current, character) =>
                {
                    current[character] = IsAlphabetLetter(character);
                    return current;
                });

            // Assert
            foreach (var character in isAlphabetLetterByCharacter.Keys.OrderBy(x => x))
            {
                Assert.IsFalse(isAlphabetLetterByCharacter[character], $"Expected '{character}' not to be an alphabet letter.");
            }
        }
    }
}