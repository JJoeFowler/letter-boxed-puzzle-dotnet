// ===============================================================================================================================================
// <copyright file="AlphabetExtensionsTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Enums;
    using LetterBoxedPuzzle.Framework.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The unit tests for the letters enumeration.
    /// </summary>
    [TestClass]
    public class AlphabetExtensionsTests
    {
        /// <summary>
        ///     Checks whether the given alphabetic index between 0 to 26 is converted to the correct bit-wise enumerated letter of the alphabet.
        /// </summary>
        [TestMethod]
        public void ToAlphabetBitMask_GivenAlphabeticIndexInRange_ReturnsCorrectAlphabetBitMask()
        {
            var alphabetBitMaskValues = Enum.GetValues<AlphabetBitMask>();

            // Explicitly check boundary cases.
            Assert.AreEqual(AlphabetBitMask.A, 1.ToAlphabetBitMask());
            Assert.AreEqual(AlphabetBitMask.Z, 26.ToAlphabetBitMask());

            // Check all values incrementally.
            for (var alphabeticIndex = 1; alphabeticIndex <= AlphabetConstants.EnglishAlphabetSize; alphabeticIndex++)
            {
                var expectedAlphabetBitMask = alphabetBitMaskValues[alphabeticIndex];
                var actualAlphabetBitMask = alphabeticIndex.ToAlphabetBitMask();

                Assert.AreEqual(
                    expectedAlphabetBitMask,
                    actualAlphabetBitMask,
                    $"The alphabet bit mask for '{alphabeticIndex}' was '{actualAlphabetBitMask}' instead of '{expectedAlphabetBitMask}'.");
            }
        }

        /// <summary>
        ///     Checks whether the given out-of-range index, which is not between 1 to 26, is converted to '<see cref="AlphabetBitMask.None"/>'.
        /// </summary>
        [TestMethod]
        public void ToAlphabetBitMask_GivenIndexOutOfRange_ReturnsAlphabetBitMaskNone()
        {
            Assert.AreEqual(AlphabetBitMask.None, int.MinValue.ToAlphabetBitMask());
            Assert.AreEqual(AlphabetBitMask.None, (-1).ToAlphabetBitMask());
            Assert.AreEqual(AlphabetBitMask.None, 0.ToAlphabetBitMask());
            Assert.AreEqual(AlphabetBitMask.None, 27.ToAlphabetBitMask());
            Assert.AreEqual(AlphabetBitMask.None, int.MaxValue.ToAlphabetBitMask());
        }

        /// <summary>
        ///     Checks whether the given bit-wise enumerated letter of the alphabet is converted to the correct alphabetic index.
        /// </summary>
        [TestMethod]
        public void ToAlphabeticIndex_GivenAlphabetBitMask_ReturnsCorrectAlphabeticIndex()
        {
            // Explicitly check boundary cases.
            Assert.AreEqual(0, AlphabetBitMask.None.ToAlphabeticIndex());
            Assert.AreEqual(1, AlphabetBitMask.A.ToAlphabeticIndex());
            Assert.AreEqual(26, AlphabetBitMask.Z.ToAlphabeticIndex());

            // Check all values incrementally.
            var expectedAlphabeticIndex = 0;
            foreach (var letter in Enum.GetValues(typeof(AlphabetBitMask)))
            {
                var actualAlphabeticIndex = ((AlphabetBitMask)letter).ToAlphabeticIndex();
                Assert.AreEqual(
                    expectedAlphabeticIndex,
                    actualAlphabeticIndex,
                    $"The alphabetic index for '{letter}' was '{actualAlphabeticIndex}' instead of '{expectedAlphabeticIndex}'.");
                expectedAlphabeticIndex++;
            }
        }
    }
}