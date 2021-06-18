// ===============================================================================================================================================
// <copyright file="CandidateWordTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Enums;
    using LetterBoxedPuzzle.Framework.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Constants.AlphabetConstants;

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
        ///     Name of animals as test words.
        /// </summary>
        private static readonly string[] AnimalTestWords = new[] { "Aardvark", "BEAR", "cat", "doG", "elephant", "FoX", "GORILLA", "harE", "Zebra" };

        /// <summary>
        ///     Candidate word initialized with the simple test word.
        /// </summary>
        private static readonly CandidateWord SimpleCandidateWord = new (SimpleTestWord);

        /// <summary>
        ///     Candidate word initialized with the A-to-Z test word.
        /// </summary>
        private static readonly CandidateWord AToZCandidateWord = new (AToZTestWord);

        /// <summary>
        ///     Checks whether the first letter of the animal tests word is indeed their first letter.
        /// </summary>
        [TestMethod]
        public void FirstLetter_GivenAnimalTestWord_IsFirstLetterOfAnimal()
        {
            // Arrange
            // ReSharper disable once StringLiteralTypo
            var expectedFirstLetters = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'z' };

            // Act
            var actualFirstLetters = AnimalTestWords.Select(animal => new CandidateWord(animal).FirstLetter).ToArray();

            // Assert
            for (var index = 0; index < expectedFirstLetters.Length; index++)
            {
                var actualFirstLetter = actualFirstLetters[index];
                var expectedFirstLetter = expectedFirstLetters[index];
                Assert.AreEqual(expectedFirstLetter, actualFirstLetter, $"The first letter of '{AnimalTestWords[index]}' is not '{expectedFirstLetter}'.");
            }
        }

        /// <summary>
        ///     Checks whether the last letter of the animal tests word is indeed their last letter.
        /// </summary>
        [TestMethod]
        public void LastLetter_GivenAnimalTestWord_IsLastLetterOfAnimal()
        {
            // Arrange
            // ReSharper disable once StringLiteralTypo
            var expectedLastLetters = new[] { 'k', 'r', 't', 'g', 't', 'x', 'a', 'e', 'a' };

            // Act
            var actualLastLetters = AnimalTestWords.Select(animal => new CandidateWord(animal).LastLetter).ToArray();

            // Assert
            for (var index = 0; index < expectedLastLetters.Length; index++)
            {
                var actualLastLetter = actualLastLetters[index];
                var expectedLastLetter = expectedLastLetters[index];
                Assert.AreEqual(expectedLastLetter, actualLastLetter, $"The last letter of '{AnimalTestWords[index]}' is not '{expectedLastLetter}'.");
            }
        }

        /// <summary>
        ///     Checks whether the lowercase word of the A-to-Z candidate word is indeed lowercased.
        /// </summary>
        [TestMethod]
        public void LowercaseWord_GivenAToZCandidateWord_IsLowercased()
        {
            // Arrange
            // ReSharper disable once StringLiteralTypo
            const string expectedWordLowercased = "thequickbrownfoxjumpsoverthelazydog";

            // Act
            var actualWordLowercased = AToZCandidateWord.LowercaseWord;

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
        ///     Checks whether the alphabet bit mask of the letters of the simple candidate word are the bit-wise enumerated letters of the simple
        ///     test word ORed together.
        /// </summary>
        [TestMethod]
        public void AlphabetBitMask_GivenSimpleCandidateWord_IsBitMaskOfSimpleTestWordLettersORedTogether()
        {
            // Arrange
            const AlphabetBitMask expectedAlphabetBitMask = AlphabetBitMask.S | AlphabetBitMask.I | AlphabetBitMask.M | AlphabetBitMask.P
                | AlphabetBitMask.L | AlphabetBitMask.E | AlphabetBitMask.T | AlphabetBitMask.W | AlphabetBitMask.O | AlphabetBitMask.R
                | AlphabetBitMask.D;

            // Act
            var actualAlphabetBitMask = SimpleCandidateWord.AlphabetBitMask;

            // Assert
            Assert.AreEqual(expectedAlphabetBitMask, actualAlphabetBitMask);
        }

        /// <summary>
        ///     Checks whether the alphabet bit mask of the letters of the A-to-Z candidate word are all bit-wise enumerated letters of the whole
        ///     alphabet ORed together.
        /// </summary>
        [TestMethod]
        public void AlphabetBitMask_GivenAToZCandidateWord_IsBitMaskOfAllLettersORedTogether()
        {
            // Arrange
            var expectedAlphabetBitMask = AlphabetBitMaskWithAllBitsSet;

            // Act
            var actualAlphabetBitMask = AToZCandidateWord.AlphabetBitMask;

            // Assert
            Assert.AreEqual(expectedAlphabetBitMask, actualAlphabetBitMask);
        }

        /// <summary>
        ///     Checks whether the letters of the simple candidate word is contained within all its letters is true.
        /// </summary>
        [TestMethod]
        public void IsContainedIn_SimpleCandidateWordGivenOnlyItsLetters_IsTrue()
        {
            // Arrange and act
            // ReSharper disable once StringLiteralTypo
            var actualIsContainedIn = SimpleCandidateWord.IsContainedIn("simpletestword");

            // Assert
            Assert.IsTrue(actualIsContainedIn);
        }

        /// <summary>
        ///     Checks whether the letters of the A-to-Z candidate word is contained within all letters of the alphabet is true.
        /// </summary>
        [TestMethod]
        public void IsContainedIn_AtoZCandidateWordGivenAllLetters_IsTrue()
        {
            // Arrange and act
            var actualIsContainedIn = AToZCandidateWord.IsContainedIn(AlphabetRangeTextFromAToZ);

            // Assert
            Assert.IsTrue(actualIsContainedIn);
        }

        /// <summary>
        ///     Checks whether the letters of the A-to-Z candidate word are contained within all letters of the alphabet but one is false.
        /// </summary>
        [TestMethod]
        public void IsContainedIn_AtoZCandidateWordGivenAllLettersButOne_IsFalse()
        {
            // Arrange
            var allCandidateLettersButOneArray = new CandidateWord[EnglishAlphabetSize];
            var messageStart = $"A-to-Z test word '{AToZTestWord}' cannot be contained within the letters";

            for (var index = 0; index < EnglishAlphabetSize; index++)
            {
                var alphabetRangeTextMissingOneLetter =
                    AlphabetRangeTextFromAToZ.Replace(AlphabetRangeTextFromAToZ[index].ToString(), string.Empty);

                allCandidateLettersButOneArray[index] = new CandidateWord(alphabetRangeTextMissingOneLetter);
            }

            // Act and arrange
            Console.WriteLine($"Asserting the {messageStart}:");

            for (var index = 0; index < EnglishAlphabetSize; index++)
            {
                var allCandidateLettersButOne = allCandidateLettersButOneArray[index];
                var allCandidateLettersButOneText = allCandidateLettersButOne.LowercaseWord;
                var missingLetter = AlphabetRangeTextFromAToZ[index];

                var messageEnd = $"'{allCandidateLettersButOneText}' missing the letter '{missingLetter}'";

                Console.WriteLine($"\t{messageEnd}");

                Assert.IsFalse(AToZCandidateWord.IsContainedIn(allCandidateLettersButOneText), $"The {messageStart} {messageEnd}.");
            }
        }
    }
}