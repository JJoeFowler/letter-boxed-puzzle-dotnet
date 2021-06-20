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
    using System.Text.RegularExpressions;

    using LetterBoxedPuzzle.Framework.Enums;
    using LetterBoxedPuzzle.Framework.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Constants.AlphabetConstants;

    using static TestCommonAssertions;
    using static TestCommonConstants;

    using static Utilities.StringUtilities;

    /// <summary>
    ///     Unit tests for the candidate word class.
    /// </summary>
    [TestClass]
    public class CandidateWordTests
    {
        /// <summary>
        ///     The candidate word allowed test pattern for the side letters with the test letter groups for three sides, which are the
        ///     letter groups "abc", "def", and "ghi".
        /// </summary>
        internal const string CandidateWordAllowedTestPatternForSideLettersWithTestLetterGroupsForThreeSides =
            "^[abcABC][defDEF][ghiGHI][abcABC]$";

        /// <summary>
        ///     Candidate word initialized with the simple test word.
        /// </summary>
        internal static readonly CandidateWord SimpleCandidateWord = new (SimpleTestWord);

        /// <summary>
        ///     Candidate word initialized with the A-to-Z test word.
        /// </summary>
        internal static readonly CandidateWord AToZCandidateWord = new (AToZTestWord);

        /// <summary>
        ///     Verifies that an argument null exception is thrown when candidate word is initialized with a null value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CandidateWord_GivenNull_ThrowsArgumentNullException()
        {
            // Arrange
            string? candidateWordLetters = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            _ = new CandidateWord(candidateWordLetters);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        /// <summary>
        ///     Verifies that an argument exception is thrown when candidate word is initialized with an empty string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CandidateWord_GivenEmptyString_ThrowsArgumentException()
        {
            // Arrange
            var candidateWordLetters = string.Empty;

            // Act
            _ = new CandidateWord(candidateWordLetters);
        }

        /// <summary>
        ///     Verifies that an argument exception is thrown when candidate word is initialized with a word with underscores.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CandidateWord_GivenWordWithUnderscores_ThrowsArgumentException()
        {
            // Arrange
            const string candidateWordLetters = "non_alphabet_letter_word";

            // Act
            _ = new CandidateWord(candidateWordLetters);
        }

        /// <summary>
        ///     Verifies that an argument exception is thrown when candidate word is initialized with a word with spaces.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CandidateWord_GivenWordWithSpaces_ThrowsArgumentException()
        {
            // Arrange
            const string candidateWordLetters = "non alphabet letter word";

            // Act
            _ = new CandidateWord(candidateWordLetters);
        }

        /// <summary>
        ///     Verifies whether the first letter of the animal tests word is indeed their first letter.
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
                Assert.AreEqual(
                    expectedFirstLetter,
                    actualFirstLetter,
                    $"The first letter of '{AnimalTestWords[index]}' is not '{expectedFirstLetter}'.");
            }
        }

        /// <summary>
        ///     Verifies whether the last letter of the animal tests word is indeed their last letter.
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
                Assert.AreEqual(
                    expectedLastLetter,
                    actualLastLetter,
                    $"The last letter of '{AnimalTestWords[index]}' is not '{expectedLastLetter}'.");
            }
        }

        /// <summary>
        ///     Verifies whether the lowercase word of the A-to-Z candidate word is indeed lowercased.
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
        ///     Verifies whether the lowercase characters of the candidate word instantiated with lowercase alphabet as a single word
        ///     are the characters of the lowercase alphabet.
        /// </summary>
        [TestMethod]
        public void LowercaseCharacters_GivenLowercaseAlphabetCandidateWord_IsLowercaseAlphabet()
        {
            // Arrange
            var expectedLowercaseCharacters = LowercaseAlphabet;
            var candidateWord = new CandidateWord(new string(LowercaseAlphabet));

            // Act
            var actualLowercasedCharacters = candidateWord.LowercaseCharacters;

            // Assert
            for (var index = 0; index < actualLowercasedCharacters.Length; index++)
            {
                var expectedCharacter = expectedLowercaseCharacters[index];
                var actualCharacter = actualLowercasedCharacters[index];
                Assert.AreEqual(
                    expectedCharacter,
                    actualCharacter,
                    $"Expected the character '{expectedCharacter}' and not '{actualCharacter}' at index {index}.");
            }
        }

        /// <summary>
        ///     Verifies whether the lowercase word of the A-to-Z candidate word is indeed lowercased.
        /// </summary>
        [TestMethod]
        public void SequentialLetters_GivenAToZCandidateWord_HasAllSequentialLetters()
        {
            // Arrange
            var expectedSequentialLetters = new string[]
                {
                    "th", "he", "eq", "qu", "ui", "ic", "ck", "kb", "br", "ro", "ow", "wn", "nf", "fo", "ox", "xj", "ju", "um", "mp", "ps",
                    "so", "ov", "ve", "er", "rt", "th", "he", "el", "la", "az", "zy", "yd", "do", "og",
                };

            // Act
            var actualSequentialLetters = AToZCandidateWord.SequentialLetters;

            // Assert
            for (var index = 0; index < actualSequentialLetters.Length; index++)
            {
                var expectedLetters = expectedSequentialLetters[index];
                var actualLetters = actualSequentialLetters[index];
                Assert.AreEqual(
                    expectedLetters,
                    actualLetters,
                    $"Expected the sequential letters \"{expectedLetters}\" and not \"{actualLetters}\" at index {index}.");
            }
        }

        /// <summary>
        ///     Verifies whether byte sequence of the A-to-Z candidate word is correct.
        /// </summary>
        [TestMethod]
        public void ByteSequence_GivenSimpleCandidateWord_ReturnsCorrectByteSequence()
        {
            // Arrange, calculated using https://www.dcode.fr/ascii-code ASCII encoder.
            var expectedByteSequence = new byte[]
                {
                    116, 104, 101, 113, 117, 105, 99, 107, 98, 114, 111, 119, 110, 102, 111, 120, 106, 117, 109, 112,
                    115, 111, 118, 101, 114, 116, 104, 101, 108, 97, 122, 121, 100, 111, 103,
                };

            // Act
            var actualByteSequence = AToZCandidateWord.ByteSequence;

            // Assert
            for (var index = 0; index < AToZTestWord.Length; index++)
            {
                var expectedByteValue = expectedByteSequence[index];
                var actualByteValue = actualByteSequence[index];

                Assert.AreEqual(
                    expectedByteValue,
                    actualByteValue,
                    $"The byte value of letter {index + 1} of \"{AToZTestWord}\" was '{actualByteValue}' instead of '{expectedByteValue}'.");
            }
        }

        /// <summary>
        ///     Verifies whether the alphabet bit mask of the letters of the simple candidate word are the bit-wise enumerated letters of the simple
        ///     test word ORed together.
        /// </summary>
        [TestMethod]
        public void AlphabetBitMask_GivenSimpleCandidateWord_IsBitMaskOfSimpleTestWordLettersORedTogether()
        {
            // Arrange
            const AlphabetBitMask expectedAlphabetBitMask = SimpleTestWordAlphabetBitMask;

            // Act
            var actualAlphabetBitMask = SimpleCandidateWord.AlphabetBitMask;

            // Assert
            Assert.AreEqual(expectedAlphabetBitMask, actualAlphabetBitMask);
        }

        /// <summary>
        ///     Verifies whether the alphabet bit mask of the letters of the A-to-Z candidate word are all bit-wise enumerated letters of the whole
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
        ///     Verifies whether the letters of the simple candidate word is contained within all its letters is true.
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
        ///     Verifies whether the letters of the A-to-Z candidate word is contained within all letters of the alphabet is true.
        /// </summary>
        [TestMethod]
        public void IsContainedIn_AtoZCandidateWordGivenAllLetters_IsTrue()
        {
            // Arrange and act
            var actualIsContainedIn = AToZCandidateWord.IsContainedIn(LowercaseAlphabetText);

            // Assert
            Assert.IsTrue(actualIsContainedIn);
        }

        /// <summary>
        ///     Verifies whether the letters of the A-to-Z candidate word are contained within all letters of the alphabet but one is false.
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
                    LowercaseAlphabetText.Replace(LowercaseAlphabetText[index].ToString(), string.Empty);

                allCandidateLettersButOneArray[index] = new CandidateWord(alphabetRangeTextMissingOneLetter);
            }

            // Act and arrange
            Console.WriteLine($@"Asserting the {messageStart}:");

            for (var index = 0; index < EnglishAlphabetSize; index++)
            {
                var allCandidateLettersButOne = allCandidateLettersButOneArray[index];
                var allCandidateLettersButOneText = allCandidateLettersButOne.LowercaseWord;
                var missingLetter = LowercaseAlphabetText[index];

                var messageEnd = $"\"{allCandidateLettersButOneText}\" missing the letter '{missingLetter}'";

                Console.WriteLine($@"	{messageEnd}");

                Assert.IsFalse(AToZCandidateWord.IsContainedIn(allCandidateLettersButOneText), $"The {messageStart} {messageEnd}.");
            }
        }

        /// <summary>
        ///     Verifies that an argument null exception is thrown if given a null value for the candidate letters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsContainedIn_GivenNullValueForCandidateLetters_ThrowsArgumentNullException()
        {
            // Arrange
            string? nullValue = null;

            // Act
#pragma warning disable 8604
            _ = new CandidateWord("word").IsContainedIn(nullValue);
#pragma warning restore 8604
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if given candidate letters has a non-alphabet letter.
        /// </summary>
        [TestMethod]
        public void IsContainedIn_GivenCandidateLettersWithNonAlphabetLetter_ThrowsArgumentException()
        {
            // Arrange
            var candidateLettersWithNonAlphabetCharacter = NonAlphabetCharacters.Select(c => $"abc{c.ToString()}xyz");

            static bool TestCandidateWordFunction(string invalidLetters) => new CandidateWord("abcXyz").IsContainedIn(invalidLetters);

            AssertExceptionThrown(
                TestCandidateWordFunction,
                typeof(ArgumentException),
                candidateLettersWithNonAlphabetCharacter,
                _ => "candidate letters");
        }

        /// <summary>
        ///     Verifies that an argument null exception is thrown when given a null value for the side letters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsAllowed_GivenNullValue_ThrowsArgumentNullException()
        {
            // Arrange
            SideLetters? nullValue = null;

            // Act
#pragma warning disable 8604
            _ = new CandidateWord(SimpleTestWord).IsAllowed(nullValue);
#pragma warning restore 8604
        }

        /// <summary>
        ///     Verifies whether given side letters with the three letter groups "abc", "def", "ghi" that any candidate word with
        ///     double letters from the first nine letters of the alphabet is not allowed.
        /// </summary>
        [TestMethod]
        public void IsAllowed_GivenCandidateWordsWithDoubleLettersWithSideLettersUsingTestLetterGroupsForThreeSides_IsFalse()
        {
            // Arrange
            var testLetterGroups = TestLetterGroupsForThreeSides;
            var testSideLetters = new SideLetters(testLetterGroups);

            var allowedLetters = string.Join(string.Empty, testLetterGroups);
            var expectedForbiddenWords = allowedLetters.Select(c => $"a{c}b{c}c{c}d{c}e{c}f{c}g{c}h{c}i").ToArray();

            // Act
            var actualIsAllowed = expectedForbiddenWords.Select(word => new CandidateWord(word).IsAllowed(testSideLetters)).ToArray();

            // Asset
            for (var index = 0; index < expectedForbiddenWords.Length; index++)
            {
                var forbiddenWord = expectedForbiddenWords[index];
                Assert.IsFalse(
                    actualIsAllowed[index],
                    $"Expected  \"{forbiddenWord}\" not to be allowed for side letter with letter groups {QuoteJoin(testLetterGroups)}.");
            }
        }

        /// <summary>
        ///     Verifies whether given side letters with the three letter groups "abc", "def", "ghi" that any three-letter candidate word
        ///     matching the pattern "[abc][def][ghi]" is allowed .
        /// </summary>
        [TestMethod]
        public void IsAllowed_GivenCandidateWordsMatchingTestPatternWithSideLettersUsingTestLetterGroupsForThreeSides_IsTrue()
        {
            // Arrange
            var testLetterGroups = TestLetterGroupsForThreeSides;
            var testSideLetters = new SideLetters(testLetterGroups);

            var expectedAllowedWords = (from firstCharacter in testLetterGroups[0]
                                        from secondCharacter in testLetterGroups[1]
                                        from thirdCharacter in testLetterGroups[2]
                                        from fourthCharacter in testLetterGroups[0]
                                        select firstCharacter.ToString() + secondCharacter + thirdCharacter + fourthCharacter).ToArray();

            const string allowedWordPattern = CandidateWordAllowedTestPatternForSideLettersWithTestLetterGroupsForThreeSides;
            var allowedWordRegex = new Regex(allowedWordPattern);

            // Act
            var actualIsAllowed = expectedAllowedWords.Select(word => new CandidateWord(word).IsAllowed(testSideLetters)).ToArray();

            // Asset
            for (var index = 0; index < expectedAllowedWords.Length; index++)
            {
                // Assert constructed allowed word matches its pattern as an extra sanity check.
                var allowedWord = expectedAllowedWords[index];
                Assert.IsTrue(
                    allowedWordRegex.IsMatch(allowedWord),
                    $"Candidate word for \"{allowedWord} \" does not match the test pattern \"{allowedWordPattern}\"");

                Assert.IsTrue(
                    actualIsAllowed[index],
                    $"Expected  \"{allowedWord}\" to be allowed for side letter with letter groups {QuoteJoin(testLetterGroups)}.");
            }
        }
    }
}