// ===============================================================================================================================================
// <copyright file="LettersTests.cs" company="Joe Fowler">
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
    public class LettersTests
    {
        /// <summary>
        ///     Checks whether the given index between 0 to 26 is converted to the correct letter.
        /// </summary>
        [TestMethod]
        public void ToLetter_GivenIndexInRange_ReturnsCorrectLetter()
        {
            var letterValues = Enum.GetValues<Letters>();

            // Explicitly check boundary cases.
            Assert.AreEqual(Letters.None, 0.ToLetter());
            Assert.AreEqual(Letters.A, 1.ToLetter());
            Assert.AreEqual(Letters.Z, 26.ToLetter());

            // Check remaining values.
            for (var index = 2; index <= 25; index++)
            {
                Assert.AreEqual(letterValues[index], index.ToLetter());
            }
        }

        /// <summary>
        ///     Checks whether the given index out of the range of 0 to 26 is converted to Letters.None.
        /// </summary>
        [TestMethod]
        public void ToLetter_GivenIndexOfRange_ReturnsLettersNone()
        {
            Assert.AreEqual(Letters.None, int.MinValue.ToLetter());
            Assert.AreEqual(Letters.None, (-1).ToLetter());
            Assert.AreEqual(Letters.None, 27.ToLetter());
            Assert.AreEqual(Letters.None, int.MaxValue.ToLetter());
        }

        /// <summary>
        ///     Checks whether the given letter is converted to the correct index.
        /// </summary>
        [TestMethod]
        public void ToIndex_GivenLetter_ReturnsCorrectIndex()
        {
            // Explicitly check boundary cases.
            Assert.AreEqual(0, Letters.None.ToIndex());
            Assert.AreEqual(1, Letters.A.ToIndex());
            Assert.AreEqual(26, Letters.Z.ToIndex());

            // Check all values incrementally.
            var index = 0;
            foreach (var letter in Enum.GetValues(typeof(Letters)))
            {
                Assert.AreEqual(index, ((Letters)letter).ToIndex());
                index++;
            }
        }
    }
}