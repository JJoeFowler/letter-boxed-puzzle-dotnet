// ===============================================================================================================================================
// <copyright file="CandidateWordTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterboxPuzzle.Framework.Tests.Unit
{
    using LetterboxPuzzle.Framework.Enums;
    using LetterboxPuzzle.Framework.Extensions;
    using LetterboxPuzzle.Framework.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static LetterboxPuzzle.Framework.Enums.AlphabeticLetters;

    /// <summary>
    ///     The unit tests for the letters enumeration.
    /// </summary>
    [TestClass]
    public class CandidateWordTests
    {
        /// <summary>
        ///     Simple test word.
        /// </summary>
        private const string SimpleTestWord = nameof(SimpleTestWord);

        /// <summary>
        ///     A-to-Z test word.
        /// </summary>
        private const string AToZTestWord = "TheQuickBrownFoxJumpsOverTheLazyDog";

        /// <summary>
        ///     Candidate word initialized with the simple test word.
        /// </summary>
        private static readonly CandidateWord SimpleCandidateWord = new(SimpleTestWord);

        /// <summary>
        ///     Candidate word initialized with the A-to-Z test word.
        /// </summary>
        private static readonly CandidateWord AToZCandidateWord = new(AToZTestWord);

        /// <summary>
        ///     Checks whether the word of the A-to-Z candidate word is in lowercased.
        /// </summary>
        [TestMethod]
        public void Word_GivenAToZCandidateWord_IsLowercased()
        {
            // Arrange
            const string expectedWordLowercased = "thequickbrownfoxjumpsoverthelazydog";

            // Act
            var actualWordLowercased = AToZCandidateWord.WordLowercased;

            // Assert
            Assert.AreEqual(expectedWordLowercased, actualWordLowercased);
        }

        /// <summary>
        ///     Checks whether ASCII sequence of the A-to-Z candidate word is correct.
        /// </summary>
        [TestMethod]
        public void AsciiSequence_GivenSimpleCandidateWord_ReturnsCorrectAsciiSequence()
        {
            // Arrange, calculated using https://www.dcode.fr/ascii-code ASCII encoder.
            var expectedAsciiSequence = new byte[]
                {
                    116, 104, 101, 113, 117, 105, 99, 107, 98, 114, 111, 119, 110, 102, 111, 120, 106, 117, 109, 112,
                    115, 111, 118, 101, 114, 116, 104, 101, 108, 97, 122, 121, 100, 111, 103,
                };

            // Act
            var actualAsciiSequence = AToZCandidateWord.AsciiSequence;

            // Assert
            for (var index = 0; index < AToZTestWord.Length; index++)
            {
                var expectedAsciiValue = expectedAsciiSequence[index];
                var actualAsciiValue = actualAsciiSequence[index];
                Assert.AreEqual(
                    expectedAsciiValue,
                    actualAsciiValue,
                    $"The ASCII value of letter {index + 1} of '{AToZTestWord}' was '{actualAsciiValue}' instead of '{expectedAsciiValue}'.");
            }
        }

        /// <summary>
        ///     Checks whether the bit-wise alphabetic letters of the simple candidate word are correctly ORed together.
        /// </summary>
        [TestMethod]
        public void AlphabeticLetters_GivenSimpleCandidateWord_AreCorrectlyORed()
        {
            // Arrange
            const AlphabeticLetters expectedAlphabeticLetters = AlphabeticLetters.S | AlphabeticLetters.I | AlphabeticLetters.M
                | AlphabeticLetters.P | AlphabeticLetters.L | AlphabeticLetters.E | AlphabeticLetters.T | AlphabeticLetters.W | AlphabeticLetters.O
                | AlphabeticLetters.R | AlphabeticLetters.D;

            // Act
            var actualAlphabeticLetters = SimpleCandidateWord.AlphabeticLetters;

            // Assert
            Assert.AreEqual(expectedAlphabeticLetters, actualAlphabeticLetters);
        }

        /// <summary>
        ///     Checks whether the bit-wise alphabetic letters of the A-to-Z candidate word are all alphabetic letters ORed together.
        /// </summary>
        [TestMethod]
        public void AlphabeticLetters_GivenAToZCandidateWord_AreCorrectlyORed()
        {
            // Arrange
            var expectedAlphabeticLetters = AlphabeticLettersExtensions.AllAlphabeticLetters;

            // Act
            var actualAlphabeticLetters = AToZCandidateWord.AlphabeticLetters;

            // Assert
            Assert.AreEqual(expectedAlphabeticLetters, actualAlphabeticLetters);
        }

        /// <summary>
        ///     Checks whether the letters of the simple candidate word is contained within all its letters is true.
        /// </summary>
        [TestMethod]
        public void IsContainedIn_SimpleCandidateWordGivenOnlyItsLetters_IsTrue()
        {
            // Arrange
            var candidateWordLetters = new CandidateWord("simpletword");

            // Act
            var actualIsContainedIn = SimpleCandidateWord.IsContainedIn(candidateWordLetters);

            // Assert
            Assert.IsFalse(actualIsContainedIn);
        }
    }
}