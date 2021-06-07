// ===============================================================================================================================================
// <copyright file="AlphabeticLettersExtensions.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterboxPuzzle.Framework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterboxPuzzle.Framework.Constants;
    using LetterboxPuzzle.Framework.Enums;

    /// <summary>
    ///     Extension methods for the letters enumeration.
    /// </summary>
    public static class AlphabeticLettersExtensions
    {
        /// <summary>
        ///     All the bit-wise alphabetic letters from <see cref="AlphabeticLetters.A" /> to <see cref="AlphabeticLetters.Z" />.
        /// </summary>
        public static readonly AlphabeticLetters AllAlphabeticLetters;

        /// <summary>
        ///     The bit-wise alphabetic letter values of the alphabetic letters enumeration.
        /// </summary>
        private static readonly AlphabeticLetters[] AlphabeticLetterValues = Enum.GetValues<AlphabeticLetters>();

        /// <summary>
        ///     The alphabetic indices of the bit-wise alphabetic letters enumeration.
        /// </summary>
        private static readonly int[] AlphabeticIndices = Enumerable.Range(0, 27).ToArray();

        /// <summary>
        ///     The mapping of the alphabetic index by alphabetic letters where <see cref="AlphabeticLetters.None" /> maps to 0,
        ///     <see cref="AlphabeticLetters.A" /> maps to 1, and <see cref="AlphabeticLetters.Z" /> maps to 26.
        /// </summary>
        private static readonly Dictionary<AlphabeticLetters, int> AlphabeticIndexByLetter = AlphabeticLetterValues
            .Zip(AlphabeticIndices, (letter, index) => new KeyValuePair<AlphabeticLetters, int>(letter, index))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        /// <summary>
        ///     Initializes static members of the <see cref="AlphabeticLettersExtensions" /> class.
        /// </summary>
        static AlphabeticLettersExtensions()
        {
            for (var alphabeticLetterIndex = 1; alphabeticLetterIndex <= AlphabeticConstants.EnglishAlphabetSize; alphabeticLetterIndex++)
            {
                AllAlphabeticLetters |= alphabeticLetterIndex.ToAlphabeticLetter();
            }
        }

        /// <summary>
        ///     Converts the index of an alphabetic letter to its corresponding letter where '0' returns '<see cref=" AlphabeticLetters.None" />',
        ///     '1' returns '<see cref=" AlphabeticLetters.A" />', and '26' returns '<see cref="AlphabeticLetters.Z" />'.
        /// </summary>
        /// <param name="alphabeticLetterIndex">The index of the alphabetic letter.</param>
        /// <returns>
        ///     The alphabetic letter (<see cref="AlphabeticLetters.A" /> to <see cref="AlphabeticLetters.Z" />) of the given alphabetic
        ///     letter index if it is in range of 1 to 26, or <see cref="AlphabeticLetters.None" /> otherwise.
        /// </returns>
        public static AlphabeticLetters ToAlphabeticLetter(this int alphabeticLetterIndex)
        {
            try
            {
                return AlphabeticLetterValues[alphabeticLetterIndex];
            }
            catch (IndexOutOfRangeException)
            {
                return AlphabeticLetters.None;
            }
        }

        /// <summary>
        ///     Converts the alphabetic letter to its corresponding alphabetic index where '<see cref="AlphabeticLetters.None" />' returns '0',
        ///     '<see cref="AlphabeticLetters.A" />' returns '1', and '<see cref="AlphabeticLetters.Z" />' returns '26'.
        /// </summary>
        /// <param name="alphabeticLetter">The alphabetic letter.</param>
        /// <returns>The alphabetic index between 0 to 26 of the given alphabetic letter.</returns>
        public static int ToAlphabeticIndex(this AlphabeticLetters alphabeticLetter)
        {
            try
            {
                return AlphabeticIndexByLetter[alphabeticLetter];
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                throw new KeyNotFoundException(
                    "Internal error (should never happen) where the given alphabeticLetter does not have an index.",
                    keyNotFoundException);
            }
        }
    }
}