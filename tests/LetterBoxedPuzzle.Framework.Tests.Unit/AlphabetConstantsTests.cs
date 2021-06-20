// ===============================================================================================================================================
// <copyright file="AlphabetConstantsTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Constants.AlphabetConstants;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     Unit tests for the string utility methods class.
    /// </summary>
    [TestClass]
    public class AlphabetConstantsTests
    {
        /// <summary>
        ///     Verify whether the lowercase alphabet character array is correct.
        /// </summary>
        [TestMethod]
        public void LowercaseAlphabet_IsCorrect()
        {
            // Arrange
            var expectedLowercaseAlphabet = GenerateAlphabeticRangeSequence(LowercaseA, EnglishAlphabetSize);

            // Act
            var actualLowercaseAlphabet = LowercaseAlphabet;

            // Assert
            for (var index = 0; index < EnglishAlphabetSize; index++)
            {
                var expectedLetter = expectedLowercaseAlphabet[index];
                var actualLetter = actualLowercaseAlphabet[index];

                Assert.AreEqual(expectedLetter, actualLetter, $"Expected latter '{expectedLetter}' and not '{actualLetter}' at index {index}.");
            }
        }

        /// <summary>
        ///     Verify whether the uppercase alphabet character array is correct.
        /// </summary>
        [TestMethod]
        public void UppercaseAlphabet_IsCorrect()
        {
            // Arrange
            var expectedUppercaseAlphabet = GenerateAlphabeticRangeSequence(UppercaseA, EnglishAlphabetSize);

            // Act
            var actualUppercaseAlphabet = UppercaseAlphabet;

            // Assert
            for (var index = 0; index < EnglishAlphabetSize; index++)
            {
                var expectedLetter = expectedUppercaseAlphabet[index];
                var actualLetter = actualUppercaseAlphabet[index];

                Assert.AreEqual(expectedLetter, actualLetter, $"Expected latter '{expectedLetter}' and not '{actualLetter}' at index {index}.");
            }
        }

        /// <summary>
        ///     Verify whether the lowercase alphabet text string is correct.
        /// </summary>
        [TestMethod]
        public void LowercaseAlphabetText_IsCorrect()
        {
            // Arrange
            var expectedText = GenerateAlphabeticRangeAsText(LowercaseA, EnglishAlphabetSize);

            // Act
            var actualText = LowercaseAlphabetText;

            // Assert
            Assert.AreEqual(expectedText, actualText, $"Expected lowercase text \"{expectedText}\" and not \"{actualText}\".");
        }

        /// <summary>
        ///     Verify whether the uppercase alphabet text string is correct.
        /// </summary>
        [TestMethod]
        public void UppercaseAlphabetText_IsCorrect()
        {
            // Arrange
            var expectedText = GenerateAlphabeticRangeAsText(UppercaseA, EnglishAlphabetSize);

            // Act
            var actualText = UppercaseAlphabetText;

            // Assert
            Assert.AreEqual(expectedText, actualText, $"Expected uppercase text \"{expectedText}\" and not \"{actualText}\".");
        }
    }
}