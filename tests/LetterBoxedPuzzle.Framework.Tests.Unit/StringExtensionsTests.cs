// ===============================================================================================================================================
// <copyright file="StringExtensionsTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static TestCommonConstants;

    /// <summary>
    ///     Unit tests for the string utility methods class.
    /// </summary>
    [TestClass]
    public class StringExtensionsTests
    {
        /// <summary>
        ///     The test strings with the first letter lowercased.
        /// </summary>
        private static readonly string[] TestFirstCharLowercasedStrings = { "abc", "d ef", "gH i", "jKL", "mn  O", "p qR ", "sTU", "vwX", "yZ " };

        /// <summary>
        ///     Corresponding test strings with the first letter uppercased.
        /// </summary>
        private static readonly string[] TestFirstCharUppercasedStrings = { "Abc", "D ef", "GH i", "JKL", "Mn  O", "P qR ", "STU", "VwX", "YZ " };

        /// <summary>
        ///     Verify whether given a null value throws an argument null exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToFirstCharUpper_GivenNullParameters_ThrowsArgumentNullException()
        {
            // Act, disabling warning of passing a null value for a non-nullable string parameter.
#pragma warning disable 8625
            _ = StringExtensions.ToFirstCharUpper(null);
#pragma warning restore 8625
        }

        /// <summary>
        ///     Verify whether given an empty throws an argument exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToFirstCharUpper_GivenEmptyString_ThrowsArgumentNullException()
        {
            // Act
            _ = string.Empty.ToFirstCharUpper();
        }

        /// <summary>
        ///     Verifies whether given a lowercase letter is converted to the corresponding uppercase letter.
        /// </summary>
        [TestMethod]
        public void ToFirstCharUpper_LowercaseLetter_ConvertsToCorrespondingUppercaseLetter()
        {
            // Arrange
            var expectedLetters = UppercaseTestAlphabet.Select(letter => letter.ToString()).ToArray();

            // Act
            var actualLetters = LowercaseTestAlphabet.Select(letter => letter.ToString().ToFirstCharUpper()).ToArray();

            // Assert
            for (var letterIndex = 0; letterIndex < AlphabetSize; letterIndex++)
            {
                var lowercaseLetter = LowercaseTestAlphabet[letterIndex].ToString();
                var expectedLetter = expectedLetters[letterIndex];
                var actualLetter = actualLetters[letterIndex];
                Assert.AreEqual(
                    expectedLetter,
                    actualLetter,
                    $"Expected the lowercase '{lowercaseLetter}' to be converted to '{expectedLetter}' and not '{actualLetter}'.");
            }
        }

        /// <summary>
        ///     Verify whether given the test strings with the first character lowercased are converted to the corresponding test
        ///     strings with the first character uppercased.
        /// </summary>
        [TestMethod]
        public void ToFirstCharUpper_GivenTestFirstCharLowercaseStrings_ConvertsToTestFirstCharUpperStrings()
        {
            // Arrange
            var expectedStrings = TestFirstCharUppercasedStrings;

            // Act
            var actualStrings = TestFirstCharLowercasedStrings.Select(x => x.ToFirstCharUpper()).ToArray();

            // Assert
            for (var index = 0; index < expectedStrings.Length; index++)
            {
                var expectedString = expectedStrings[index];
                var actualString = actualStrings[index];
                Assert.AreEqual(
                    expectedString,
                    actualString,
                    $"Expected the string '{TestFirstCharLowercasedStrings[index]}' to become '{expectedString}' instead of '{actualString}'.");
            }
        }

        /// <summary>
        ///     Verify whether given the test strings with the first character uppercased are converted to the same strings.
        /// </summary>
        [TestMethod]
        public void ToFirstCharUpper_GivenTestFirstCharUppercaseStrings_AreSameStrings()
        {
            // Arrange
            var expectedStrings = TestFirstCharUppercasedStrings;

            // Act
            var actualStrings = TestFirstCharUppercasedStrings.Select(x => x.ToFirstCharUpper()).ToArray();

            // Assert
            for (var index = 0; index < expectedStrings.Length; index++)
            {
                var expectedString = expectedStrings[index];
                var actualString = actualStrings[index];
                Assert.AreEqual(
                    expectedString,
                    actualString,
                    $"Expected the string to remain '{expectedString}' and not become  '{actualString}'.");
            }
        }

        /// <summary>
        ///     Verify whether given the test white space strings are converted to the same strings.
        /// </summary>
        [TestMethod]
        public void ToFirstCharUpper_GivenTestWhiteSpaceStrings_AreSameStrings()
        {
            // Arrange
            var expectedStrings = TestWhitespaceStrings;

            // Act
            var actualStrings = TestWhitespaceStrings.Select(x => x.ToFirstCharUpper()).ToArray();

            // Assert
            for (var index = 0; index < expectedStrings.Length; index++)
            {
                var expectedString = expectedStrings[index];
                var actualString = actualStrings[index];
                Assert.AreEqual(
                    expectedString,
                    actualString,
                    $"Expected the string  to remain '{expectedString}' and not become  '{actualString}'.");
            }
        }
    }
}