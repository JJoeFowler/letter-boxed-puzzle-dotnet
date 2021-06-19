// ===============================================================================================================================================
// <copyright file="StringUtilitiesTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static TestCommonConstants;

    using static Utilities.StringUtilities;

    /// <summary>
    ///     Unit tests for the string utility methods class.
    /// </summary>
    [TestClass]
    public class StringUtilitiesTests
    {
        /// <summary>
        ///     The single quoted lowercase alphabet delimited by commas with a space.
        /// </summary>
        internal const string SingleQuotedLowercaseAlphabet = "'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', "
            + "'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'";

        /// <summary>
        ///     Verifies whether the given an empty character array that the string "''" is returned.
        /// </summary>
        [TestMethod]
        public void QuoteJoin_GivenEmptyCharacterArray_ReturnsEmptyStringQuoted()
        {
            // Arrange
            const string expectedEmptyQuotes = "''";

            // Act
            var actualEmptyQuotes = QuoteJoin(Array.Empty<char>());

            // Assert
            Assert.AreEqual(expectedEmptyQuotes, actualEmptyQuotes);
        }

        /// <summary>
        ///     Verifies whether the given an empty string array that the string "''" is returned.
        /// </summary>
        [TestMethod]
        public void QuoteJoin_GivenEmptyStringArray_ReturnsEmptyStringQuoted()
        {
            // Arrange
            const string expectedEmptyQuotes = "''";

            // Act
            var actualEmptyQuotes = QuoteJoin(Array.Empty<string>());

            // Assert
            Assert.AreEqual(expectedEmptyQuotes, actualEmptyQuotes);
        }

        /// <summary>
        ///     Verifies whether the given the single character 'a' that the string "'a'" is returned.
        /// </summary>
        [TestMethod]
        public void QuoteJoin_GivenSingleCharacter_ReturnsStringOfCharacterQuoted()
        {
            // Arrange
            const char singleCharacter = 'a';
            const string expectedQuotedCharacter = "'a'";

            // Act
            var actualQuotedCharacter = QuoteJoin(singleCharacter);

            // Assert
            Assert.AreEqual(expectedQuotedCharacter, actualQuotedCharacter);
        }

        /// <summary>
        ///     Verifies whether the given the simple test word that the single-quoted simple test word is returned.
        /// </summary>
        [TestMethod]
        public void QuoteJoin_GivenSimpleTestWordAsOnlyInput_ReturnsSimpleTestInputQuoted()
        {
            // Arrange
            const string textInput = SimpleTestWord;
            const string expectedQuotedTestInput = "'" + SimpleTestWord + "'";

            // Act
            var actualQuotedTestInput = QuoteJoin(textInput);

            // Assert
            Assert.AreEqual(expectedQuotedTestInput, actualQuotedTestInput);
        }

        /// <summary>
        ///     Verifies whether the given the character array of lowercase alphabet letters that a string of them quoted delimited by a
        ///     comma and a space is returned.
        /// </summary>
        [TestMethod]
        public void QuoteJoin_GivenLowercaseAlphabetCharacterArray_ReturnsStringOfEachCharacterQuotedDelimitedByCommaAndSpace()
        {
            // Arrange
            var lowercaseAlphabetCharacterArray = LowercaseAlphabet;
            const string expectedQuotedLowercaseAlphabet = SingleQuotedLowercaseAlphabet;

            // Act
            var actualQuotedLowercaseAlphabet = QuoteJoin(lowercaseAlphabetCharacterArray);

            // Assert
            Assert.AreEqual(expectedQuotedLowercaseAlphabet, actualQuotedLowercaseAlphabet);
        }

        /// <summary>
        ///     Verifies whether the given the string array of lowercase alphabet letters that a string of them quoted delimited by a
        ///     comma and a space is returned.
        /// </summary>
        [TestMethod]
        public void QuoteJoin_GivenLowercaseAlphabetStringArray_ReturnsStringOfEachCharacterQuotedDelimitedByCommaAndSpace()
        {
            // Arrange
            var lowercaseAlphabetCharacterArray = LowercaseAlphabet.Select(character => character.ToString()).ToArray();
            const string expectedQuotedLowercaseAlphabet = SingleQuotedLowercaseAlphabet;

            // Act
            var actualQuotedLowercaseAlphabet = QuoteJoin(lowercaseAlphabetCharacterArray);

            // Assert
            Assert.AreEqual(expectedQuotedLowercaseAlphabet, actualQuotedLowercaseAlphabet);
        }
    }
}