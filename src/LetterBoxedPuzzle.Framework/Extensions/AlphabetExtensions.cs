// ===============================================================================================================================================
// <copyright file="AlphabetExtensions.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Enums;

    /// <summary>
    ///     Extension methods for the bit-wise enumeration of the letters of the alphabet.
    /// </summary>
    public static class AlphabetExtensions
    {
        /// <summary>
        ///     All 26 letters of the bit-wise enumerated alphabet from <see cref="AlphabetBitMask.A" /> to <see cref="AlphabetBitMask.Z" />
        ///     ORed together.
        /// </summary>
        public static readonly AlphabetBitMask AlphabetBitMaskAllBitsSet;

        /// <summary>
        ///     The 26 values of the bit-wise enumeration of the letters of the alphabet.
        /// </summary>
        private static readonly AlphabetBitMask[] AlphabetBitMaskValues = Enum.GetValues<AlphabetBitMask>();

        /// <summary>
        ///     The alphabetic indices from 0 to 26 of the bit-wise enumerated letters of the alphabet, where index '0' corresponds to
        ///     '<see cref="AlphabetBitMask.None"/>'.
        /// </summary>
        private static readonly int[] AlphabeticIndices = Enumerable.Range(0, 27).ToArray();

        /// <summary>
        ///     The mapping of the alphabetic index by the alphabet bit mask where '<see cref="AlphabetBitMask.None" />' maps to '0',
        ///     '<see cref="AlphabetBitMask.A" />' maps to '1', and '<see cref="AlphabetBitMask.Z" />' maps to '26'.
        /// </summary>
        private static readonly Dictionary<AlphabetBitMask, int> AlphabeticIndexByLetter = AlphabetBitMaskValues
            .Zip(AlphabeticIndices, (letter, index) => new KeyValuePair<AlphabetBitMask, int>(letter, index))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        /// <summary>
        ///     Initializes static members of the <see cref="AlphabetExtensions" /> class.
        /// </summary>
        static AlphabetExtensions()
        {
            for (var alphabeticIndex = 1; alphabeticIndex <= AlphabetConstants.EnglishAlphabetSize; alphabeticIndex++)
            {
                AlphabetBitMaskAllBitsSet |= alphabeticIndex.ToAlphabetBitMask();
            }
        }

        /// <summary>
        ///     Converts the alphabetic index between 1 and 26 to its corresponding bit-wise enumerated letter (where '1' returns
        ///     '<see cref=" AlphabetBitMask.A" />' and '26' returns '<see cref="AlphabetBitMask.Z" />') or '<see cref="AlphabetBitMask.None"/>'
        ///     if out of range.
        /// </summary>
        /// <param name="alphabeticIndex">The alphabetic index between 1 to 26 corresponding to a letter of the alphabet.</param>
        /// <returns>
        ///     The bit-wise enumerated alphabet bit mask (<see cref="AlphabetBitMask.A" /> to <see cref="AlphabetBitMask.Z" />) of the given
        ///     alphabetic index if between 1 and 26, or <see cref="AlphabetBitMask.None" /> otherwise.
        /// </returns>
        public static AlphabetBitMask ToAlphabetBitMask(this int alphabeticIndex)
        {
            try
            {
                return AlphabetBitMaskValues[alphabeticIndex];
            }
            catch (IndexOutOfRangeException)
            {
                return AlphabetBitMask.None;
            }
        }

        /// <summary>
        ///     Converts the bit-wise enumerated alphabet bit mask to its corresponding alphabetic index where '<see cref="AlphabetBitMask.None" />'
        ///     returns '0', '<see cref="AlphabetBitMask.A" />' returns '1', and '<see cref="AlphabetBitMask.Z" />' returns '26'.
        /// </summary>
        /// <param name="alphabetBitMask">The alphabet bit mask.</param>
        /// <returns>The alphabetic index between 0 and 26 of the given bit-wise enumerated alphabet bit mask.</returns>
        public static int ToAlphabeticIndex(this AlphabetBitMask alphabetBitMask)
        {
            try
            {
                return AlphabeticIndexByLetter[alphabetBitMask];
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                throw new KeyNotFoundException(
                    "Internal error (should never happen) where the given alphabet bit mask does not have an index.",
                    keyNotFoundException);
            }
        }
    }
}