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

    using LetterBoxedPuzzle.Framework.Enums;

    /// <summary>
    ///     Extension methods for to convert a one-letter alphabet bit mask to and from its corresponding alphabetic index.
    /// </summary>
    public static class AlphabetExtensions
    {
        /// <summary>
        ///     The values for the alphabet bit mask which bit-wise enumerates the 26 letters of the alphabet.
        /// </summary>
        private static readonly AlphabetBitMask[] AlphabetBitMaskValues = Enum.GetValues<AlphabetBitMask>();

        /// <summary>
        ///     The alphabetic indices from 0 to 26 of the bit-wise enumerated letters of the alphabet, where indices '0', '1', ..., '26'
        ///     corresponds to the bit masks '<see cref="AlphabetBitMask.None" />', '<see cref="AlphabetBitMask.A" />', ...,
        ///     '<see cref="AlphabetBitMask.Z" />', respectively.
        /// </summary>
        private static readonly int[] AlphabeticIndices = Enumerable.Range(0, 27).ToArray();

        /// <summary>
        ///     The mapping of the alphabetic index by the alphabet bit mask where '<see cref="AlphabetBitMask.None" />',
        ///     '<see cref="AlphabetBitMask.A" />', ..., '<see cref="AlphabetBitMask.Z" />' maps to '0', '1', ..., '26', respectively.
        /// </summary>
        private static readonly Dictionary<AlphabetBitMask, int> AlphabeticIndexByLetter = AlphabetBitMaskValues
            .Zip(AlphabeticIndices, (letter, index) => new KeyValuePair<AlphabetBitMask, int>(letter, index))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        /// <summary>
        ///     Converts the alphabetic index between 1 and 26 to its corresponding one-letter alphabet bit mask (where indices '1', ..., '26'
        ///     return '<see cref=" AlphabetBitMask.A" />', ..., '<see cref="AlphabetBitMask.Z" />', respectively) or the default bit mask
        ///     '<see cref="AlphabetBitMask.None" />' if out of range.
        /// </summary>
        /// <param name="alphabeticIndex">The alphabetic index between 1 to 26 corresponding to a letter of the alphabet.</param>
        /// <returns>
        ///     The one-letter alphabet bit mask ('<see cref="AlphabetBitMask.A" />' to '<see cref="AlphabetBitMask.Z" />') of the given
        ///     alphabetic index if between '1' and '26', or '<see cref="AlphabetBitMask.None" />' otherwise.
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
        ///     Converts the one-letter alphabet bit mask to its corresponding alphabetic index where '<see cref="AlphabetBitMask.None" />'
        ///     '<see cref="AlphabetBitMask.A" />', ..., '<see cref="AlphabetBitMask.Z" />' returns '0', '1', ..., '26', respectively.
        /// </summary>
        /// <param name="alphabetBitMask">The alphabet bit mask.</param>
        /// <returns>The alphabetic index between 0 and 26 of the given one-letter alphabet bit mask.</returns>
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