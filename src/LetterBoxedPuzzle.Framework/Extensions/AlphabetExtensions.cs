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
        ///     All 26 letters of the bit-wise enumerated alphabet from <see cref="AlphabetLetters.A" /> to <see cref="AlphabetLetters.Z" />
        ///     ORed together.
        /// </summary>
        public static readonly AlphabetLetters AllAlphabetLettersBitMask;

        /// <summary>
        ///     The 26 values of the bit-wise enumeration of the letters of the alphabet.
        /// </summary>
        private static readonly AlphabetLetters[] AlphabeticLetterValues = Enum.GetValues<AlphabetLetters>();

        /// <summary>
        ///     The alphabetic indices from 0 to 26 of the bit-wise enumerated letters of the alphabet, where index '0' corresponds to
        ///     '<see cref="AlphabetLetters.None"/>'.
        /// </summary>
        private static readonly int[] AlphabeticIndices = Enumerable.Range(0, 27).ToArray();

        /// <summary>
        ///     The mapping of the alphabetic index by alphabet letters where '<see cref="AlphabetLetters.None" />' maps to '0',
        ///     '<see cref="AlphabetLetters.A" />' maps to '1', and '<see cref="AlphabetLetters.Z" />' maps to '26'.
        /// </summary>
        private static readonly Dictionary<AlphabetLetters, int> AlphabeticIndexByLetter = AlphabeticLetterValues
            .Zip(AlphabeticIndices, (letter, index) => new KeyValuePair<AlphabetLetters, int>(letter, index))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        /// <summary>
        ///     Initializes static members of the <see cref="AlphabetExtensions" /> class.
        /// </summary>
        static AlphabetExtensions()
        {
            for (var alphabeticIndex = 1; alphabeticIndex <= AlphabetConstants.EnglishAlphabetSize; alphabeticIndex++)
            {
                AllAlphabetLettersBitMask |= alphabeticIndex.ToAlphabetLetter();
            }
        }

        /// <summary>
        ///     Converts the alphabetic index between 1 and 26 to its corresponding bit-wise enumerated letter (where '1' returns
        ///     '<see cref=" AlphabetLetters.A" />' and '26' returns '<see cref="AlphabetLetters.Z" />') or '<see cref="AlphabetLetters.None"/>'
        ///     if out of range.
        /// </summary>
        /// <param name="alphabeticIndex">The alphabetic index between 1 to 26 corresponding to a letter of the alphabet.</param>
        /// <returns>
        ///     The bit-wise enumerated alphabet letter (<see cref="AlphabetLetters.A" /> to <see cref="AlphabetLetters.Z" />) of the given
        ///     alphabetic index if between 1 and 26, or <see cref="AlphabetLetters.None" /> otherwise.
        /// </returns>
        public static AlphabetLetters ToAlphabetLetter(this int alphabeticIndex)
        {
            try
            {
                return AlphabeticLetterValues[alphabeticIndex];
            }
            catch (IndexOutOfRangeException)
            {
                return AlphabetLetters.None;
            }
        }

        /// <summary>
        ///     Converts the bit-wise enumerated alphabet letter to its corresponding alphabetic index where '<see cref="AlphabetLetters.None" />'
        ///     returns '0', '<see cref="AlphabetLetters.A" />' returns '1', and '<see cref="AlphabetLetters.Z" />' returns '26'.
        /// </summary>
        /// <param name="alphabetLetter">The alphabet letter.</param>
        /// <returns>The alphabetic index between 0 and 26 of the given bit-wise enumerated alphabet letter.</returns>
        public static int ToAlphabeticIndex(this AlphabetLetters alphabetLetter)
        {
            try
            {
                return AlphabeticIndexByLetter[alphabetLetter];
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                throw new KeyNotFoundException(
                    "Internal error (should never happen) where the given alphabet letter does not have an index.",
                    keyNotFoundException);
            }
        }
    }
}