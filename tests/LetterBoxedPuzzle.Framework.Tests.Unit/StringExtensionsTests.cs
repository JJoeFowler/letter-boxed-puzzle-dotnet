// ===============================================================================================================================================
// <copyright file="StringExtensionsTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Extensions.StringExtensions;

    using static TestCommonConstants;

    /// <summary>
    ///     Unit tests for the string utility methods class.
    /// </summary>
    [TestClass]
    public class StringExtensionsTests
    {
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