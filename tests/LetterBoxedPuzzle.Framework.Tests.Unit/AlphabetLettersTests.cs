// ===============================================================================================================================================
// <copyright file="AlphabetLettersTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterboxPuzzle.Framework.Tests.Unit
{
    using System;

    using LetterboxPuzzle.Framework.Constants;
    using LetterboxPuzzle.Framework.Enums;
    using LetterboxPuzzle.Framework.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The unit tests for the letters enumeration.
    /// </summary>
    [TestClass]
    public class AlphabetLettersTests
    {
        /// <summary>
        ///     Checks whether the given alphabetic index between 0 to 26 is converted to the correct bit-wise enumerated letter of the alphabet.
        /// </summary>
        [TestMethod]
        public void ToAlphabetLetter_GivenAlphabeticIndexInRange_ReturnsCorrectAlphabetLetter()
        {
            var alphabetLetterValues = Enum.GetValues<AlphabetLetters>();

            // Explicitly check boundary cases.
            Assert.AreEqual(AlphabetLetters.A, 1.ToAlphabetLetter());
            Assert.AreEqual(AlphabetLetters.Z, 26.ToAlphabetLetter());

            // Check all values incrementally.
            for (var alphabeticIndex = 1; alphabeticIndex <= AlphabetConstants.EnglishAlphabetSize; alphabeticIndex++)
            {
                var expectedAlphabetLetter = alphabetLetterValues[alphabeticIndex];
                var actualAlphabetLetter = alphabeticIndex.ToAlphabetLetter();

                Assert.AreEqual(
                    expectedAlphabetLetter,
                    actualAlphabetLetter,
                    $"The alphabet letter for '{alphabeticIndex}' was '{actualAlphabetLetter}' instead of '{expectedAlphabetLetter}'.");
            }
        }

        /// <summary>
        ///     Checks whether the given out-of-range index, which is not between 1 to 26, is converted to '<see cref="AlphabetLetters.None"/>'.
        /// </summary>
        [TestMethod]
        public void ToAlphabetLetter_GivenIndexOutOfRange_ReturnsAlphabetLetterNone()
        {
            Assert.AreEqual(AlphabetLetters.None, int.MinValue.ToAlphabetLetter());
            Assert.AreEqual(AlphabetLetters.None, (-1).ToAlphabetLetter());
            Assert.AreEqual(AlphabetLetters.None, 0.ToAlphabetLetter());
            Assert.AreEqual(AlphabetLetters.None, 27.ToAlphabetLetter());
            Assert.AreEqual(AlphabetLetters.None, int.MaxValue.ToAlphabetLetter());
        }

        /// <summary>
        ///     Checks whether the given bit-wise enumerated letter of the alphabet is converted to the correct alphabetic index.
        /// </summary>
        [TestMethod]
        public void ToAlphabeticIndex_GivenAlphabetLetter_ReturnsCorrectAlphabeticIndex()
        {
            // Explicitly check boundary cases.
            Assert.AreEqual(0, AlphabetLetters.None.ToAlphabeticIndex());
            Assert.AreEqual(1, AlphabetLetters.A.ToAlphabeticIndex());
            Assert.AreEqual(26, AlphabetLetters.Z.ToAlphabeticIndex());

            // Check all values incrementally.
            var expectedAlphabeticIndex = 0;
            foreach (var letter in Enum.GetValues(typeof(AlphabetLetters)))
            {
                var actualAlphabeticIndex = ((AlphabetLetters)letter).ToAlphabeticIndex();
                Assert.AreEqual(
                    expectedAlphabeticIndex,
                    actualAlphabeticIndex,
                    $"The alphabetic index for '{letter}' was '{actualAlphabeticIndex}' instead of '{expectedAlphabeticIndex}'.");
                expectedAlphabeticIndex++;
            }
        }
    }
}