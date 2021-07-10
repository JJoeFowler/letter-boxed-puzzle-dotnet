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
        ///     The lowercase letter 'a'.
        /// </summary>
        public const char LowercaseA = 'a';

        /// <summary>
        ///     The uppercase letter 'A'.
        /// </summary>
        public const char UppercaseA = 'A';

        /// <summary>
        ///     The lowercase letter 'z'.
        /// </summary>
        public const char LowercaseZ = 'z';

        /// <summary>
        ///     The uppercase letter 'Z'.
        /// </summary>
        public const char UppercaseZ = 'Z';

        /// <summary>
        ///     The characters of the lowercase alphabet from 'a' to 'z'.
        /// </summary>
        public static readonly char[] LowercaseAlphabet =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z',
            };

        /// <summary>
        ///     The characters of the uppercase alphabet from 'A' to 'Z'.
        /// </summary>
        public static readonly char[] UppercaseAlphabet =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z',
            };

        /// <summary>
        ///     Lowercase alphabet as a string from 'a' to 'z'.
        /// </summary>
        public static readonly string LowercaseAlphabetText = new (LowercaseAlphabet);

        /// <summary>
        ///     Uppercase alphabet as a string from 'A' to 'Z'.
        /// </summary>
        public static readonly string UppercaseAlphabetText = new (UppercaseAlphabet);

        /// <summary>
        ///     Lazy initializer of the alphabet bit mask set with all bits set.
        /// </summary>
        public static readonly Lazy<AlphabetBitMask> LazyAlphabetBitMaskWithAllBitsSet = new (CalculateAlphabetBitMaskWithAllBitsSet);

        /// <summary>
        ///     Gets all 26 letters of the bit-wise enumerated alphabet from <see cref="Enums.AlphabetBitMask.A" /> to <see cref="Enums.AlphabetBitMask.Z" />
        ///     ORed together.
        /// </summary>
        public static readonly AlphabetBitMask AlphabetBitMaskWithAllBitsSet = LazyAlphabetBitMaskWithAllBitsSet.Value;

        /// <summary>
        ///     Calculates the alphabet bit mask with all of its bits set.
        /// </summary>
        /// <returns>The alphabet bit mask with all bits set.</returns>
        private static AlphabetBitMask CalculateAlphabetBitMaskWithAllBitsSet() =>
            Enumerable.Range(1, EnglishAlphabetSize).Aggregate(AlphabetBitMask.None, (current, index) => current | index.ToAlphabetBitMask());
    }
}