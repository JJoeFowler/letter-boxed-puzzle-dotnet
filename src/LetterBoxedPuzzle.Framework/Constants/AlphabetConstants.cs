﻿// ===============================================================================================================================================
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
        ///     The lower case letter 'z'.
        /// </summary>
        public const char LowerCaseZ = 'z';

        /// <summary>
        ///     The upper case letter 'Z'.
        /// </summary>
        public const char UpperCaseZ = 'Z';

        /// <summary>
        ///     Full lowercase alphabet as a string from 'a' to 'z', which is "abcdefghijklmnopqrstuvwxyz".
        /// </summary>
        public static readonly string FullLowercaseAlphabetText = GenerateAlphabeticRangeAsText(LowerCaseA, EnglishAlphabetSize);

        /// <summary>
        ///     Full lowercase alphabet as a sequence of characters from 'a' to 'z'.
        /// </summary>
        public static readonly char[] FullLowercaseAlphabetSequence = GenerateAlphabeticRangeSequence(LowerCaseA, EnglishAlphabetSize);

        /// <summary>
        ///     Full uppercase alphabet as a sequence of characters from 'A' to 'Z'.
        /// </summary>
        public static readonly char[] FullUppercaseAlphabetSequence = GenerateAlphabeticRangeSequence(UpperCaseA, EnglishAlphabetSize);

        /// <summary>
        ///     Full alphabet as a sequence of characters from 'a' to 'z' and from 'A' to 'Z'.
        /// </summary>
        public static readonly char[] FullAlphabetSequence = FullLowercaseAlphabetSequence.Union(FullUppercaseAlphabetSequence).ToArray();

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
        private static AlphabetBitMask CalculateAlphabetBitMaskWithAllBitsSet()
        {
            var alphabeticIndices = Enumerable.Range(1, EnglishAlphabetSize);
            return alphabeticIndices.Aggregate(AlphabetBitMask.None, (current, index) => current | index.ToAlphabetBitMask());
        }
    }
}