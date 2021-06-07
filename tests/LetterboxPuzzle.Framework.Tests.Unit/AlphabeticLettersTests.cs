// ===============================================================================================================================================
// <copyright file="AlphabeticLettersTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterboxPuzzle.Framework.Tests.Unit
{
    using System;

    using LetterboxPuzzle.Framework.Enums;
    using LetterboxPuzzle.Framework.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The unit tests for the letters enumeration.
    /// </summary>
    [TestClass]
    public class AlphabeticLettersTests
    {
        /// <summary>
        ///     Checks whether the given index between 0 to 26 is converted to the correct alphabeticLetter.
        /// </summary>
        [TestMethod]
        public void ToAlphabeticLetter_GivenAlphabeticIndexInRange_ReturnsCorrectAlphabeticLetter()
        {
            var letterValues = Enum.GetValues<AlphabeticLetters>();

            // Explicitly check boundary cases.
            Assert.AreEqual(AlphabeticLetters.A, 1.ToAlphabeticLetter());
            Assert.AreEqual(AlphabeticLetters.Z, 26.ToAlphabeticLetter());

            // Check remaining values.
            for (var index = 2; index <= 25; index++)
            {
                Assert.AreEqual(letterValues[index], index.ToAlphabeticLetter());
            }
        }

        /// <summary>
        ///     Checks whether the given index out of the range of 0 to 26 is converted to AlphabeticLetters.None.
        /// </summary>
        [TestMethod]
        public void ToAlphabeticLetter_GivenIndexOutOfRange_ReturnsAlphabeticLetterNone()
        {
            Assert.AreEqual(AlphabeticLetters.None, int.MinValue.ToAlphabeticLetter());
            Assert.AreEqual(AlphabeticLetters.None, (-1).ToAlphabeticLetter());
            Assert.AreEqual(AlphabeticLetters.None, 0.ToAlphabeticLetter());
            Assert.AreEqual(AlphabeticLetters.None, 27.ToAlphabeticLetter());
            Assert.AreEqual(AlphabeticLetters.None, int.MaxValue.ToAlphabeticLetter());
        }

        /// <summary>
        ///     Checks whether the given alphabeticLetter is converted to the correct index.
        /// </summary>
        [TestMethod]
        public void ToAlphabeticIndex_GivenAlphabeticLetter_ReturnsCorrectAlphabeticIndex()
        {
            // Explicitly check boundary cases.
            Assert.AreEqual(0, AlphabeticLetters.None.ToAlphabeticIndex());
            Assert.AreEqual(1, AlphabeticLetters.A.ToAlphabeticIndex());
            Assert.AreEqual(26, AlphabeticLetters.Z.ToAlphabeticIndex());

            // Check all values incrementally.
            var index = 0;
            foreach (var letter in Enum.GetValues(typeof(AlphabeticLetters)))
            {
                Assert.AreEqual(index, ((AlphabeticLetters)letter).ToAlphabeticIndex());
                index++;
            }
        }
    }
}