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

    using LetterboxPuzzle.Framework.Enums;

    /// <summary>
    ///     Extension methods for the letters enumeration.
    /// </summary>
    public static class AlphabeticLettersExtensions
    {
        /// <summary>
        ///     The alphabeticLetter values of the alphabetic letters enumeration.
        /// </summary>
        private static readonly AlphabeticLetters[] AlphabeticLetterValues = Enum.GetValues<AlphabeticLetters>();

        /// <summary>
        ///     The alphabetic index values of the alphabetic letters enumeration.
        /// </summary>
        private static readonly int[] AlphabeticIndexValues = Enumerable.Range(0, 27).ToArray();

        /// <summary>
        ///     The mapping of an alphabetic index by its alphabeticLetter where AlphabeticLetters.None maps to 0, AlphabeticLetters.A maps to 1,
        ///     and AlphabeticLetters.Z maps to 26.
        /// </summary>
        private static readonly Dictionary<AlphabeticLetters, int> AlphabeticIndexByLetter = AlphabeticLetterValues
            .Zip(AlphabeticIndexValues, (letter, index) => new KeyValuePair<AlphabeticLetters, int>(letter, index))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        /// <summary>
        ///     Converts the alphabetic index of a alphabeticLetter to its corresponding alphabetic alphabeticLetter where 0 returns AlphabeticLetters.None,
        ///     1 returns AlphabeticLetters.A, and 26 returns AlphabeticLetters.Z.
        /// </summary>
        /// <param name="letterIndex">The index of the alphabeticLetter.</param>
        /// <returns>The alphabeticLetter (AlphabeticLetters.A to AlphabeticLetters.Z) of the given index if in range of 1 to 26, or AlphabeticLetters.None otherwise.</returns>
        public static AlphabeticLetters ToAlphabeticLetter(this int letterIndex)
        {
            try
            {
                return AlphabeticLetterValues[letterIndex];
            }
            catch (IndexOutOfRangeException)
            {
                return AlphabeticLetters.None;
            }
        }

        /// <summary>
        ///     Converts the alphabetic index of the alphabeticLetter to its corresponding alphabeticLetter where 0 returns AlphabeticLetters.None, 1 returns AlphabeticLetters.A,
        ///     and 26 returns AlphabeticLetters.Z.
        /// </summary>
        /// <param name="alphabeticLetter">The alphabeticLetter.</param>
        /// <returns>The index of the given alphabeticLetter.</returns>
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