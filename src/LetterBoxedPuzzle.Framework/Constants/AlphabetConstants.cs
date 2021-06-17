// ===============================================================================================================================================
// <copyright file="AlphabetConstants.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Constants
{
    using System;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Enums;
    using LetterBoxedPuzzle.Framework.Extensions;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     Class of constants related to the alphabet.
    /// </summary>
    public static class AlphabetConstants
    {
        /// <summary>
        ///     The size of the English alphabet.
        /// </summary>
        public const int EnglishAlphabetSize = 26;

        /// <summary>
        ///     The lower case letter 'a'.
        /// </summary>
        public const char LowerCaseA = 'a';

        /// <summary>
        ///     The upper case letter 'A'.
        /// </summary>
        public const char UpperCaseA = 'A';

        /// <summary>
        ///     ASCII value of the upper case 'A'.
        /// </summary>
        public static readonly byte AsciiValueOfUpperCaseA = GetAsciiValue(UpperCaseA);

        /// <summary>
        ///     ASCII value of the lower case 'a'.
        /// </summary>
        public static readonly byte AsciiValueOfLowerCaseA = GetAsciiValue(LowerCaseA);

        /// <summary>
        ///     Alphabet range text from 'a' to 'z', which is the string "abcdefghijklmnopqrstuvwxyz".
        /// </summary>
        public static readonly string AlphabetRangeTextFromAToZ = AlphabetRangeText(LowerCaseA, EnglishAlphabetSize);

        /// <summary>
        ///     All 26 letters of the bit-wise enumerated alphabet from <see cref="Enums.AlphabetBitMask.A" /> to <see cref="Enums.AlphabetBitMask.Z" />
        ///     ORed together.
        /// </summary>
        public static readonly Lazy<AlphabetBitMask> AlphabetBitMaskWithAllBitsSet = new (CalculateAlphabetBitMaskWithAllBitsSet);

        /// <summary>
        ///     Calculates the alphabet bit mask with all of its bits set.
        /// </summary>
        /// <returns>The alphabet bit mask with all bits set.</returns>
        private static AlphabetBitMask CalculateAlphabetBitMaskWithAllBitsSet()
        {
            var alphabeticIndices = Enumerable.Range(1, EnglishAlphabetSize);
            return alphabeticIndices.Aggregate(AlphabetBitMask.None, (current, index) => current | index.ToAlphabetBitMask());
        }
    }
}