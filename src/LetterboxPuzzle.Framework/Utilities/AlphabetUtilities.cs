// ===============================================================================================================================================
// <copyright file="AlphabetUtilities.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterboxPuzzle.Framework.Utilities
{
    using System;
    using System.Text;

    using LetterboxPuzzle.Framework.Constants;
    using LetterboxPuzzle.Framework.Enums;
    using LetterboxPuzzle.Framework.Extensions;

    /// <summary>
    ///     Class containing utility methods related to the alphabet.
    /// </summary>
    public static class AlphabetUtilities
    {
        /// <summary>
        ///     ASCII value of the upper case 'A'.
        /// </summary>
        public static readonly byte AsciiValueOfUpperCaseA = GetAsciiValue(AlphabetConstants.UpperCaseA);

        /// <summary>
        ///     ASCII value of the lower case 'a'.
        /// </summary>
        public static readonly byte AsciiValueOfLowerCaseA = GetAsciiValue(AlphabetConstants.LowerCaseA);

        /// <summary>
        ///     Initializes static members of the <see cref="AlphabetUtilities" /> class.
        /// </summary>
        static AlphabetUtilities()
        {
            for (var alphabeticIndex = 1; alphabeticIndex <= AlphabetConstants.EnglishAlphabetSize; alphabeticIndex++)
            {
                var upperCaseLetterAsciiValue = AsciiValueOfUpperCaseA + alphabeticIndex - 1;
                var lowerCaseLetterAsciiValue = AsciiValueOfLowerCaseA + alphabeticIndex - 1;

                AlphabetLettersByAsciiValues[upperCaseLetterAsciiValue] =
                    AlphabetLettersByAsciiValues[lowerCaseLetterAsciiValue] = alphabeticIndex.ToAlphabetLetter();
            }
        }

        /// <summary>
        ///     Gets the bit-wise enumerated letters of the alphabet indexed by their ASCII values.
        /// </summary>
        public static AlphabetLetters[] AlphabetLettersByAsciiValues { get; } = new AlphabetLetters[byte.MaxValue + 1];

        /// <summary>
        ///     Gets the ASCII byte value for the given letter, assuming it can be encoded in ASCII.
        /// </summary>
        /// <param name="letter">A single-character letter.</param>
        /// <returns>The ASCII byte value of the given letter.</returns>
        public static byte GetAsciiValue(string letter)
        {
            _ = letter ?? throw new ArgumentNullException(nameof(letter));

            return Encoding.ASCII.GetBytes(letter)[0];
        }
    }
}