// ===============================================================================================================================================
// <copyright file="AlphabetUtilitiesTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Enums;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Constants.AlphabetConstants;

    using static TestCommonAssertions;

    using static TestCommonConstants;

    using static Utilities.AlphabetUtilities;
    using static Utilities.StringUtilities;

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
        ///     Verifies whether given a byte-sized character (whose value is between 0 and 255) that the converted byte value is returned.
        /// </summary>
        [TestMethod]
        public void GetByteValue_GivenByteSizedCharacter_ReturnsCorrectByteValue()
        {
            // Arrange
            var byteCharacters = Enumerable.Range(0, byte.MaxValue).Select(x => (char)x).ToArray();
            var expectedByteValues = byteCharacters.Select(character => (byte)character).ToArray();

            // Act
            var actualByteValues = byteCharacters.Select(character => GetByteValue(character)).ToArray();

            // Assert
            for (var index = 0; index < byteCharacters.Length; index++)
            {
                var character = byteCharacters[index];
                var expectedValue = expectedByteValues[index];
                var actualValue = actualByteValues[index];

                Assert.AreEqual(
                    expectedValue,
                    actualValue,
                    $"Expected the extended ASCII value of '{character}' to be '{expectedValue} and not '{actualValue}'.");
            }
        }

        /// <summary>
        ///     Verifies whether given the first non-byte sized unicode character that an overflow exception is thrown.
        /// </summary>
        [ExpectedException(typeof(OverflowException))]
        [TestMethod]
        public void GetByteValue_GivenNonByteSizedUnicodeCharacter_ThrowsOverflowException()
        {
            // Arrange
            const char firstNonByteSizedCharacter = (char)(byte.MaxValue + 1);

            // Act
            _ = GetByteValue(firstNonByteSizedCharacter);
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
            var expectedAlphabetBitMask = AlphabetBitMaskWithAllBitsSet;

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
        ///     Verifies that an argument exception is throw if given a non-alphabet character when determining its alphabet bit mask.
        /// </summary>
        [TestMethod]
        public void GetAlphabetBitMask_GivenNonAlphabetLetter_ThrowsArgumentException()
        {
            // Arrange
            var invalidCharacters = NonAlphabetCharacters;

            // Act and assert
            AssertExceptionThrown(GetAlphabetBitMask, typeof(ArgumentException), invalidCharacters, _ => "non-alphabet character");
        }

        /// <summary>
        ///     Verifies that an argument null exception is throw if given a null value for a word when determining its the alphabet bit mask.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAlphabetBitMask_GivenNullValueForWord_ThrowsArgumentNullException()
        {
            // Arrange
            string? nullValue = null;

            // Act
#pragma warning disable 8604
            _ = GetAlphabetBitMask(nullValue);
#pragma warning restore 8604
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if given a word with a non-alphabet letter when determining its alphabet bit mask.
        /// </summary>
        [TestMethod]
        public void GetAlphabetBitMask_GivenWordWithNonAlphabetLetter_ThrowsArgumentException()
        {
            // Arrange
            var invalidWords = NonAlphabetCharacters.Select(character => $"aBc{character}XyZ");

            // Act and assert
            AssertExceptionThrown(GetAlphabetBitMask, typeof(ArgumentException), invalidWords, _ => "invalid word");
        }

        /// <summary>
        ///     Verifies whether given the lower case letter 'a' with size of the alphabet that the generated sequence is the lowercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeSequence_GivenLowercaseAWithAlphabetSize_ReturnsLowercaseAlphabetSequenceOf()
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
                    $"Expected character '{expectedCharacter}' and not '{actualCharacter}' in the sequence {QuoteJoin(actualSequence)}.");
            }
        }

        /// <summary>
        ///     Verifies whether given the upper case letter 'A' with size of the alphabet that the generated sequence is the uppercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeSequence_GivenUppercaseAWithAlphabetSize_ReturnsUppercaseAlphabetSequence()
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
        ///     Verifies for each pair of alphabet letters and their maximum lengths that the generated sequences has maximum length.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeSequence_GivenAlphabetLettersWithMaximumValidLengths_ReturnsSequenceOfMaximumLengths()
        {
            // Arrange
            var maximumLowercaseLengthPairs = LowercaseAlphabet.Select(character => (c: character, 'a' + AlphabetSize - character));
            var maximumUppercaseLengthPairs = UppercaseAlphabet.Select(character => (c: character, 'A' + AlphabetSize - character));

            (char character, int length)[] maximumAlphabetLetterLengthPairs =
                maximumLowercaseLengthPairs.Union(maximumUppercaseLengthPairs).ToArray();

            var expectedLengths = maximumAlphabetLetterLengthPairs.Select(pair => pair.length).ToArray();

            // Act
            var actualSequences = maximumAlphabetLetterLengthPairs.Select(pair => GenerateAlphabeticRangeSequence(pair.character, pair.length))
                .ToArray();

            // Assert
            for (var index = 0; index < 2 * AlphabetSize; index++)
            {
                var (character, _) = maximumAlphabetLetterLengthPairs[index];
                var expectedLength = expectedLengths[index];
                var actualLength = actualSequences[index].Length;

                Assert.AreEqual(
                    expectedLength,
                    actualLength,
                    $"Expected length {expectedLength} does not match generated sequence length '{actualLength}' for alphabet letter '{character}'.");
            }
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if given any non-alphabet character as a starting letter to generate an alphabetic
        ///     range sequence.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeSequence_GivenAnyNonAlphabetCharacterAsStartingLetter_ThrowsArgumentException()
        {
            // Arrange
            var invalidStartingLetters = NonAlphabetCharacters;

            // Act and assert
            AssertExceptionThrown(
                letter => GenerateAlphabeticRangeAsText(letter, 1),
                typeof(ArgumentException),
                invalidStartingLetters,
                _ => "starting letter");
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown for each pair of an alphabet letter and an invalid length
        ///     (either zero or one more than its maximum) when generating a sequence of letters of the alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeSequence_GivenAlphabetLettersWithInvalidLengths_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var zeroLengthPair = new[] { ('A', 0) };
            var invalidLowercaseLengthPairs = LowercaseAlphabet.Select(c => (c, 'a' + AlphabetSize + 1 - c)).ToArray();
            var invalidUppercaseLengthPairs = UppercaseAlphabet.Select(c => (c, 'A' + AlphabetSize + 1 - c)).ToArray();

            IEnumerable<(char character, int lengh)> invalidAlphabetLengthPairs =
                zeroLengthPair.Union(invalidLowercaseLengthPairs).Union(invalidUppercaseLengthPairs);

            // Act and assert
            AssertExceptionThrown(
                pair => GenerateAlphabeticRangeAsText(pair.character, pair.lengh),
                typeof(ArgumentOutOfRangeException),
                invalidAlphabetLengthPairs,
                pair => $"character '{pair.character}' with length {pair.lengh}");
        }

        /// <summary>
        ///     Verifies whether given the lower case letter 'a' with size of the alphabet that the generated text is the lowercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeAsText_GivenLowercaseAWithAlphabetSize_ReturnsLowercaseAlphabet()
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
        ///     Verifies whether given the upper case letter 'A' with size of the alphabet that the generated text is the uppercase alphabet.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeAsText_GivenUppercaseAWithAlphabetSize_ReturnsUppercaseAlphabet()
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
        ///     Verifies for each pair of alphabet letters and their maximum lengths that the generated text has maximum length.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeAsText_GivenAlphabetLettersWithMaximumValidLengths_ReturnsTextOfMaximumLengths()
        {
            // Arrange
            var maximumLowercaseLengthPairs = LowercaseAlphabet.Select(character => (c: character, 'a' + AlphabetSize - character));
            var maximumUppercaseLengthPairs = UppercaseAlphabet.Select(character => (c: character, 'A' + AlphabetSize - character));

            (char character, int length)[] maximumAlphabetLetterLengthPairs =
                maximumLowercaseLengthPairs.Union(maximumUppercaseLengthPairs).ToArray();

            var expectedLengths = maximumAlphabetLetterLengthPairs.Select(pair => pair.length).ToArray();

            // Act
            var actualTexts = maximumAlphabetLetterLengthPairs.Select(pair => GenerateAlphabeticRangeAsText(pair.character, pair.length))
                .ToArray();

            // Assert
            for (var index = 0; index < 2 * AlphabetSize; index++)
            {
                var (character, _) = maximumAlphabetLetterLengthPairs[index];
                var expectedLength = expectedLengths[index];
                var actualLength = actualTexts[index].Length;

                Assert.AreEqual(
                    expectedLength,
                    actualLength,
                    $"Expected length {expectedLength} does not match generated text length '{actualLength}' for alphabet letter '{character}'.");
            }
        }

        /// <summary>
        ///     Verifies an out of range exception will be thrown when be thrown when given an lowercase 'a' and a length the size of the
        ///     alphabet plus one.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeAsText_GivenAlphabetLettersWithInvalidLengths_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var zeroLengthPair = new[] { ('A', 0) };
            var invalidLowercaseLengthPairs = LowercaseAlphabet.Select(c => (c, 'a' + AlphabetSize + 1 - c)).ToArray();
            var invalidUppercaseLengthPairs = UppercaseAlphabet.Select(c => (c, 'A' + AlphabetSize + 1 - c)).ToArray();

            IEnumerable<(char character, int lengh)> invalidAlphabetLengthPairs =
                zeroLengthPair.Union(invalidLowercaseLengthPairs).Union(invalidUppercaseLengthPairs);

            // Act and assert
            AssertExceptionThrown(
                pair => GenerateAlphabeticRangeAsText(pair.character, pair.lengh),
                typeof(ArgumentOutOfRangeException),
                invalidAlphabetLengthPairs,
                pair => $"character '{pair.character}' with length {pair.lengh}");
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if given any non-alphabet character as a starting letter to generate an alphabetic
        ///     range as text.
        /// </summary>
        [TestMethod]
        public void GenerateAlphabeticRangeAsText_GivenAnyNonAlphabetCharacterAsStartingLetter_ThrowsArgumentException()
        {
            // Arrange
            var invalidStartingLetters = NonAlphabetCharacters;

            // Act and assert
            AssertExceptionThrown(
                character => GenerateAlphabeticRangeAsText(character, 1),
                typeof(ArgumentException),
                invalidStartingLetters,
                _ => "starting non-alphabet character");
        }

        /// <summary>
        ///     Verifies that when given the "xY" as the text input that the generated distinct two-letter pairs are all four possible
        ///     lowercase pairs of the lowercase letters 'x' and 'y'.
        /// </summary>
        [TestMethod]
        public void GenerateAllDistinctTwoLetterPairs_GivenXyText_ReturnAllFourTwoLetterLowercasePairs()
        {
            // Arrange
            const string abcText = "xY";
            var expectedPairsSet = new HashSet<string>(new[] { "xx", "yy", "xy", "yx" });

            // Act
            var actualPairsSet = new HashSet<string>(GenerateAllDistinctTwoLetterPairs(abcText).ToArray());

            // Assert
            foreach (var actualPair in actualPairsSet)
            {
                Assert.IsTrue(expectedPairsSet.Contains(actualPair), $"For text '{abcText}' the actual pair '{actualPair}' was unexpected.");
            }

            foreach (var expectedPair in expectedPairsSet)
            {
                Assert.IsTrue(actualPairsSet.Contains(expectedPair), $"For text '{abcText}' the pair '{expectedPair}' was expected.");
            }
        }

        /// <summary>
        ///     Verifies that when given the "AbC" as the text input that the generated distinct two-letter pairs are all nine possible
        ///     lowercase pairs of the lowercase letters 'a', 'b', and 'c'.
        /// </summary>
        [TestMethod]
        public void GenerateAllDistinctTwoLetterPairs_GivenAbCText_ReturnAllNineTwoLetterLowercasePairs()
        {
            // Arrange
            const string abcText = "AbC";
            var expectedPairsSet = new HashSet<string>(new[] { "aa", "bb", "cc", "ab", "ba", "ac", "ca", "bc", "cb" });

            // Act
            var actualPairsSet = new HashSet<string>(GenerateAllDistinctTwoLetterPairs(abcText).ToArray());

            // Assert
            foreach (var actualPair in actualPairsSet)
            {
                Assert.IsTrue(expectedPairsSet.Contains(actualPair), $"For text '{abcText}' the actual pair '{actualPair}' was unexpected.");
            }

            foreach (var expectedPair in expectedPairsSet)
            {
                Assert.IsTrue(actualPairsSet.Contains(expectedPair), $"For text '{abcText}' the pair '{expectedPair}' was expected.");
            }
        }

        /// <summary>
        ///     Verifies that when given the full alphabet (both lowercase and uppercase) as the input text that 26^2 = 676 distinct lowercase
        ///     two-letter pairs are returned.
        /// </summary>
        [TestMethod]
        public void GenerateAllDistinctTwoLetterPairs_GivenLowercaseAndUppercaseAlphabet_ReturnAlphabetSizeSquaredPairs()
        {
            // Arrange
            var fullAlphabet = new string(LowercaseAlphabet.Union(UppercaseAlphabet).ToArray());
            const int expectedCount = AlphabetSize * AlphabetSize;

            // Act
            var actualCount = GenerateAllDistinctTwoLetterPairs(fullAlphabet).Length;

            // Assert
            Assert.AreEqual(expectedCount, actualCount, $"Expected '{expectedCount}' pairs for '{fullAlphabet}', not '{actualCount}' pairs.");
        }

        /// <summary>
        ///     Verifies that when given the same lowercase and uppercase letter repeated a hundred times each as the input text that only the
        ///     single two-letter pair of the lowercase letter is returned.
        /// </summary>
        [TestMethod]
        public void GenerateAllDistinctTwoLetterPairs_GivenSameLowercaseAndUppercaseLetterRepeatedManyTimes_ReturnsSinglePairOfLowercaseLetter()
        {
            // Arrange
            const int count = 100;
            var sameAlphabetLetters = LowercaseAlphabet.Select(letter => letter + letter.ToString().ToUpper());
            var repeatedAlphabetLetters = sameAlphabetLetters.Select(letters => string.Join(string.Empty, Enumerable.Repeat(letters, count)));
            var expectedPairs = LowercaseAlphabet.Select(letter => $"{letter}{letter}").ToArray();

            // Act
            var actualPairsForEachLetter = repeatedAlphabetLetters.Select(text => GenerateAllDistinctTwoLetterPairs(text)).ToArray();

            // Assert
            for (var index = 0; index < AlphabetSize; index++)
            {
                var actualPairs = actualPairsForEachLetter[index];
                var actualLength = actualPairs.Length;
                Assert.AreEqual(1, actualLength, $"Expected only one pair but the {actualLength} pairs {QuoteJoin(actualPairs)} were returned.");

                var actualPair = actualPairs[0];
                var expectedPair = expectedPairs[index];
                Assert.AreEqual(expectedPair, actualPair, $"Expected the pair to be '{expectedPair}' instead of the pair '{actualPair}'");
            }
        }

        /// <summary>
        ///     Verifies that an argument null exception is throw if given a null value when generating all distinct two-letter pairs.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateAllDistinctTwoLetterPairs_GivenNull_ThrowsArgumentNullException()
        {
            // Arrange
            string? textValue = null;

            // Act
#pragma warning disable 8604
            _ = GenerateAllDistinctTwoLetterPairs(textValue);
#pragma warning restore 8604
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if given either an empty string or a single-letter string when generating
        ///     all distinct two-letter pairs.
        /// </summary>
        [TestMethod]
        public void GenerateAllDistinctTwoLetterPairs_GivenEmptyStringOrSingleLetter_ThrowsArgumentException()
        {
            // Arrange
            var invalidLengthInputs = new[] { string.Empty }.Union(LowercaseAlphabet.Select(x => x.ToString()))
                .Union(UppercaseAlphabet.Select(x => x.ToString()));

            // Act and assert
            AssertExceptionThrown(GenerateAllDistinctTwoLetterPairs, typeof(ArgumentException), invalidLengthInputs, x => $"length of {x.Length}");
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if given a text input with a non-alphabet letter when generating all distinct
        ///     two-letter pairs.
        /// </summary>
        [TestMethod]
        public void GenerateAllDistinctTwoLetterPairs_GivenTextInputWithNonAlphabetLetter_ThrowsArgumentException()
        {
            // Arrange
            var invalidNonAlphabeticInputs = NonAlphabetCharacters.Select(c => $"{AToZTestWord}{c.ToString()}{SimpleTestWord}");

            // Act and assert
            AssertExceptionThrown(GenerateAllDistinctTwoLetterPairs, typeof(ArgumentException), invalidNonAlphabeticInputs, _ => "non-alphabetic");
        }
    }
}