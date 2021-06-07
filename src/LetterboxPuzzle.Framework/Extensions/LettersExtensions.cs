// ===============================================================================================================================================
// <copyright file="LettersExtensions.cs" company="Joe Fowler">
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
    public static class LettersExtensions
    {
        /// <summary>
        ///     The letter values of the letters enumeration.
        /// </summary>
        private static readonly Letters[] LetterValues = Enum.GetValues<Letters>();

        /// <summary>
        ///     The integer values of the letters enumeration.
        /// </summary>
        private static readonly int[] IndexValues = Enumerable.Range(0, 27).ToArray();

        /// <summary>
        ///     The mapping of a letter to its index where Letters.None maps to 0, Letters.A maps to 1, and Letters.Z maps to 26.
        /// </summary>
        private static readonly Dictionary<Letters, int> LetterToIndex = LetterValues
            .Zip(IndexValues, (letter, index) => new KeyValuePair<Letters, int>(letter, index))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        /// <summary>
        ///     Converts the index of the letter to its corresponding letter where 0 returns Letters.None, 1 returns Letters.A, and 26 returns Letters.Z.
        /// </summary>
        /// <param name="letterIndex">The index of the letter.</param>
        /// <returns>The letter (Letters.A to Letters.Z) of the given index if in range of 1 to 26, or Letters.None otherwise.</returns>
        public static Letters ToLetter(this int letterIndex)
        {
            try
            {
                return LetterValues[letterIndex];
            }
            catch (IndexOutOfRangeException)
            {
                return Letters.None;
            }
        }

        /// <summary>
        ///     Converts the index of the letter to its corresponding letter where 0 returns Letters.None, 1 returns Letters.A, and 26 returns Letters.Z.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <returns>The index of the given letter.</returns>
        public static int ToIndex(this Letters letter)
        {
            try
            {
                return LetterToIndex[letter];
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                throw new KeyNotFoundException(
                    "Internal error (should never happen) where the given letter does not have an index.",
                    keyNotFoundException);
            }
        }
    }
}