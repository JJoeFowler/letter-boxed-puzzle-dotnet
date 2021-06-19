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
    using System.Net.Sockets;

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
        ///     Verifies whether a null reference exception is thrown if given a null value for the side-letter groups to initialize the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SideLetters_GivenNullValueForSideLetterGroups_ThrowsNullReferenceException()
        {
            // Arrange
            string[]? nullValue = null;

            // Act
#pragma warning disable 8604
            _ = new SideLetters(nullValue);
#pragma warning restore 8604
        }

        /// <summary>
        ///     Verifies whether an argument exception is thrown if given no groups to initialize the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SideLetters_GivenNoSideLetterGroups_ThrowsArgumentException()
        {
            // Act
            _ = new SideLetters();
        }

        /// <summary>
        ///     Verifies whether an argument exception is thrown if given an empty array for the side-letter groups to initialize the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SideLetters_GivenEmptyArrayForSideLetterGroups_ThrowsArgumentException()
        {
            // Arrange
            var emptyArray = Array.Empty<string>();

            // Act
            _ = new SideLetters(emptyArray);
        }

        /// <summary>
        ///     Verifies whether an argument exception is thrown if given an empty array for the side-letter groups to initialize the class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SideLetters_GivenOneElementArrayForSideLetterGroups_ThrowsArgumentException()
        {
            // Arrange
            var oneElementSideElementGroup = new[] { "abc" };

            // Act
            _ = new SideLetters(oneElementSideElementGroup);
        }

        /// <summary>
        ///     Verifies that an argument exception is throw if given a text input with a non-alphabet letter when generating all distinct
        ///     two-letter pairs.
        /// </summary>
        [TestMethod]
        public void SideLetters_GivenSideLetterGroupsWithNonAlphabetLetter_ThrowsArgumentException()
        {
            // Arrange
            var letterGroups = new[] { "abc", "DEF", "gHi" };
            var letterGroupIndices = new[] { 0, 1, 2 };

            var invalidLetterGroups = from character in NonAlphabetCharacters
                                      from groupIndex in letterGroupIndices
                                      select letterGroups[groupIndex] + character.ToString() + letterGroups[groupIndex];

            var invalidSideLetterGroups = from invalidGroup in invalidLetterGroups
                                          from groupIndex in letterGroupIndices
                                          select new List<string>(letterGroups) { [groupIndex] = invalidGroup }.ToArray();

            var expectedExceptionType = typeof(ArgumentException);

            // Act and assert
            foreach (var invalidSideLetterGroup in invalidSideLetterGroups)
            {
                var messageEnd = $" as expected for invalid side-letter group {QuoteJoin(invalidSideLetterGroup)}'";
                try
                {
                    var sideLetters = new SideLetters(invalidSideLetterGroup);
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
    }
}