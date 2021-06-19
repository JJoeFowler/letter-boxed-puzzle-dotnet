// ===============================================================================================================================================
// <copyright file="SideLettersTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static TestCommonConstants;

    using static Utilities.StringUtilities;

    /// <summary>
    ///     Unit tests for the letter-boxed side letters class.
    /// </summary>
    [TestClass]
    public class SideLettersTests
    {
        /// <summary>
        ///     Verifies whether a null reference exception is thrown if given a null value to instantiate the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SideLetters_GivenNullValue_ThrowsNullReferenceException()
        {
            // Arrange
            string[]? nullValue = null;

            // Act
#pragma warning disable 8604
            _ = new SideLetters(nullValue);
#pragma warning restore 8604
        }

        /// <summary>
        ///     Verifies whether an argument exception is thrown if given no letters along each side to instantiate the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SideLetters_GivenNoLettersAlongEachSide_ThrowsArgumentException()
        {
            // Act
            _ = new SideLetters();
        }

        /// <summary>
        ///     Verifies whether an argument exception is thrown if given an empty array for the letters along each side to instantiate the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SideLetters_GivenEmptyArrayForLettersAlongEachSide_ThrowsArgumentException()
        {
            // Arrange
            var emptyArray = Array.Empty<string>();

            // Act
            _ = new SideLetters(emptyArray);
        }

        /// <summary>
        ///     Verifies whether an argument exception is thrown if given an empty array for the letters along each side to instantiate the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SideLetters_GivenOneElementArrayForLettersAlongEachSide_ThrowsArgumentException()
        {
            // Arrange
            var oneElementSideElementGroup = new[] { "abc" };

            // Act
            _ = new SideLetters(oneElementSideElementGroup);
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if any of given letter groups contain a non-alphabet character when instantiating
        ///     the class.
        /// </summary>
        [TestMethod]
        public void SideLetters_GivenGroupLettersContainNonAlphabetLetter_ThrowsArgumentException()
        {
            // Arrange
            var testLetterGroup = TestLetterGroupsForThreeSides;
            var groupIndices = new[] { 0, 1, 2 };

            var invalidLetters = NonAlphabetCharacters.Select(character => $"abc{character.ToString()}XYZ").ToArray();

            var invalidLetterGroups = from invalidGroup in invalidLetters
                                      from groupIndex in groupIndices
                                      select new List<string>(testLetterGroup) { [groupIndex] = invalidGroup }.ToArray();

            var expectedExceptionType = typeof(ArgumentException);

            // Act and assert
            foreach (var invalidLetterGroup in invalidLetterGroups)
            {
                var messageEnd = $" as expected for invalid letter group {QuoteJoin(invalidLetterGroup)}'";
                try
                {
                    var sideLetters = new SideLetters(invalidLetterGroup);
                    Assert.IsNotNull(sideLetters, $"Did not throw an {expectedExceptionType} {messageEnd}.");
                }
                catch (Exception exception)
                {
                    var exceptionType = exception.GetType();
                    Assert.IsTrue(
                        exceptionType == expectedExceptionType,
                        $"Threw an '{exceptionType}' instead of '{expectedExceptionType}' {messageEnd}.");
                }
            }
        }

        /// <summary>
        ///     Verify whether when given the test letter groups for nine sides to instantiate the class that the same letter groups are
        ///     returned lowercased.
        /// </summary>
        [TestMethod]
        public void LettersGroups_GivenTestLetterGroupsWithNineSidesToInstantiateClass_IsTestLetterGroupForNineSidesLowercased()
        {
            // Arrange
            var sideLetters = new SideLetters(TestLetterGroupsForNineSides);
            var expectedLetterGroups = TestLetterGroupsForNineSides.Select(group => group.ToLowerInvariant()).ToArray();

            // Act
            var actualLetterGroups = sideLetters.LetterGroups;

            // Assert
            for (var index = 0; index < expectedLetterGroups.Length; index++)
            {
                var expectedLetterGroup = expectedLetterGroups[index];
                var actualLetterGroup = actualLetterGroups[index];

                Assert.AreEqual(
                    expectedLetterGroup,
                    actualLetterGroup,
                    $"Expected letter group '{expectedLetterGroup}' for group {index + 1} instead of '{actualLetterGroup}'.");
            }
        }

        /// <summary>
        ///     Verify whether the sides letters for test letters groups for three sides returns true for all same-side two-letter pairs.
        /// </summary>
        [TestMethod]
        public void IsForbiddenTwoLetterPair_GivenTestLetterGroupsForThreeSidesWithAnySameSideLetterPairs_IsTrue()
        {
            // Arrange
            var testSideLettersForThreeSides = new SideLetters(TestLetterGroupsForThreeSides);

            var sameSideLetterPairs = new[]
                {
                    "aa", "ab", "ac", "ba", "bb", "bc", "ca", "cb", "cc", "dd", "de", "df", "ed", "ee", "ef", "fd", "fe", "ff", "gg", "gh",
                    "gi", "hg", "hh", "hi", "ig", "ih", "ii",
                };

            // Act
            var actualValues = sameSideLetterPairs.Select(pair => testSideLettersForThreeSides.IsForbiddenTwoLetterPair(pair)).ToArray();

            // Assert
            for (var index = 0; index < sameSideLetterPairs.Length; index++)
            {
                var testLetterGroups = testSideLettersForThreeSides.LetterGroups;
                Assert.IsTrue(
                    actualValues[index],
                    $"Expected the pair '{sameSideLetterPairs[index]}' to be forbidden for the letter groups {QuoteJoin(testLetterGroups)}.");
            }
        }

        /// <summary>
        ///     Verify whether the sides letters for test letters groups for three sides returns false for all different-side two-letter pairs.
        /// </summary>
        [TestMethod]
        public void IsForbiddenTwoLetterPair_GivenTestLetterGroupsForThreeSidesWithAnyDifferentSideLetterPairs_IsFalse()
        {
            // Arrange
            var testSideLettersForThreeSides = new SideLetters(TestLetterGroupsForThreeSides);

            var differentSideLetterPairs = new[]
                {
                    "ad", "ae", "af", "ag", "ah", "ai", "da", "ea", "fa", "ga", "ha", "ia", "bd", "be", "bf", "bg", "bh", "bi", "db", "eb",
                    "fb", "gb", "hb", "ib", "cd", "ce", "cf", "cg", "ch", "ci", "dc", "ec", "fc", "gc", "hc", "ic", "da", "db", "dc", "dg",
                    "dh", "di", "ad", "bd", "cd", "gd", "hd", "id", "ea", "eb", "ec", "eg", "eh", "ei", "ae", "be", "ce", "ge", "he", "ie",
                    "fa", "fb", "fc", "fg", "fh", "fi", "af", "bf", "cf", "gf", "hf", "if", "ga", "hb", "ic", "gd", "ge", "gf", "ag", "bh",
                    "ci", "dg", "eg", "fg", "ha", "hb", "ic", "hd", "he", "hf", "ah", "bh", "ci", "dh", "eh", "fh", "ia", "hb", "ic", "id",
                    "ie", "if", "ai", "bh", "ci", "di", "ei", "fi",
                };

            // Act
            var actualValues = differentSideLetterPairs.Select(pair => testSideLettersForThreeSides.IsForbiddenTwoLetterPair(pair)).ToArray();

            // Assert
            for (var index = 0; index < differentSideLetterPairs.Length; index++)
            {
                var testLetterGroups = testSideLettersForThreeSides.LetterGroups;
                Assert.IsFalse(
                    actualValues[index],
                    $"Expected the pair '{differentSideLetterPairs[index]}' not to be forbidden for the letter groups {QuoteJoin(testLetterGroups)}.");
            }
        }
    }
}