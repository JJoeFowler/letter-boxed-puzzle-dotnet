// ===============================================================================================================================================
// <copyright file="AlphabetUtilities.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Utilities
{
    using System.Linq;
    using System.Text;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Enums;
    using LetterBoxedPuzzle.Framework.Extensions;

    /// <summary>
    ///     Class containing utility methods related to the alphabet.
    /// </summary>
    public static class AlphabetUtilities
    {
        /// <summary>
        ///     Initializes static members of the <see cref="AlphabetUtilities" /> class.
        /// </summary>
        static AlphabetUtilities()
        {
            for (var alphabeticIndex = 1; alphabeticIndex <= AlphabetConstants.EnglishAlphabetSize; alphabeticIndex++)
            {
                var upperCaseLetterAsciiValue = AlphabetConstants.AsciiValueOfUpperCaseA + alphabeticIndex - 1;
                var lowerCaseLetterAsciiValue = AlphabetConstants.AsciiValueOfLowerCaseA + alphabeticIndex - 1;

                AlphabetBitMaskByAsciiValues[upperCaseLetterAsciiValue] =
                    AlphabetBitMaskByAsciiValues[lowerCaseLetterAsciiValue] = alphabeticIndex.ToAlphabetBitMask();
            }
        }

        /// <summary>
        ///     Gets the bit-wise enumerated letters of the alphabet indexed by their ASCII values.
        /// </summary>
        public static AlphabetBitMask[] AlphabetBitMaskByAsciiValues { get; } = new AlphabetBitMask[byte.MaxValue + 1];

        /// <summary>
        ///     Gets the ASCII byte value for the given letter, assuming it can be encoded in ASCII.
        /// </summary>
        /// <param name="letter">A single-character letter.</param>
        /// <returns>The ASCII byte value of the given letter.</returns>
        public static byte GetAsciiValue(char letter)
        {
            return Encoding.ASCII.GetBytes(letter.ToString())[0];
        }

        /// <summary>
        ///     Get the alphabet bit mask value for the given alphabet letter.
        /// </summary>
        /// <param name="alphabetLetter">The alphabet letter.</param>
        /// <returns>The bit mask of the given letter.</returns>
        public static AlphabetBitMask GetAlphabetBitMask(char alphabetLetter)
        {
            return AlphabetBitMaskByAsciiValues[GetAsciiValue(alphabetLetter)];
        }

        /// <summary>
        ///     Get the alphabet bit mask value for the given alphabet letters.
        /// </summary>
        /// <param name="alphabetLetters">The alphabet letters.</param>
        /// <returns>The bit mask of the given letter.</returns>
        public static AlphabetBitMask GetAlphabetBitMask(string alphabetLetters)
        {
            return alphabetLetters.Aggregate(AlphabetBitMask.None, (current, alphabetLetter) => current | GetAlphabetBitMask(alphabetLetter));
        }

        /// <summary>
        ///     <para>
        ///         Generates an alphabetic range character array starting with the given letter and going alphabetically to the
        ///         next letter for the specified length.
        ///     </para>
        ///     <para>
        ///         For example, { "a", "b", ..., "z" } is the alphabetic range array starting at 'a' with a length of 26.
        ///     </para>
        /// </summary>
        /// <param name="startingLetter">The starting letter.</param>
        /// <param name="length">The length of the range.</param>
        /// <returns>The specified alphabetic range as a <see langword="char" /> array.</returns>
        public static char[] AlphabetRangeArray(char startingLetter, int length)
        {
            return Enumerable.Range(startingLetter, length).Select(x => (char)x).ToArray();
        }

        /// <summary>
        ///     <para>
        ///         Generates an alphabetic range string starting with the given letter and going alphabetically to the next letter for
        ///         the specified length.
        ///     </para>
        ///     <para>
        ///         For example, "abcdefghijklmnopqrstuvwxyz" is the alphabetic range starting at 'a' with a length of 26.
        ///     </para>
        /// </summary>
        /// <param name="startingLetter">The starting letter.</param>
        /// <param name="length">The length of the range.</param>
        /// <returns>Concatenated <see langword="string" /> of the specified alphabetic range.</returns>
        public static string AlphabetRangeText(char startingLetter, int length)
        {
            return string.Join(string.Empty, AlphabetRangeArray(startingLetter, length));
        }
    }
}