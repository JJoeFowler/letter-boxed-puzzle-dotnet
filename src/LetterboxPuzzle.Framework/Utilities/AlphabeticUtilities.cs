// ===============================================================================================================================================
// <copyright file="AlphabeticUtilities.cs" company="Joe Fowler">
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
    ///     Class containing alphabetic utility methods.
    /// </summary>
    public static class AlphabeticUtilities
    {
        /// <summary>
        ///     ASCII value of the upper case 'A'.
        /// </summary>
        public static readonly byte AsciiValueOfUpperCaseA = GetAsciiValue(AlphabeticConstants.UpperCaseA);

        /// <summary>
        ///     ASCII value of the lower case 'a'.
        /// </summary>
        public static readonly byte AsciiValueOfLowerCaseA = GetAsciiValue(AlphabeticConstants.LowerCaseA);

        /// <summary>
        ///     Initializes static members of the <see cref="AlphabeticUtilities" /> class.
        /// </summary>
        static AlphabeticUtilities()
        {
            for (var letterIndex = 0; letterIndex < AlphabeticConstants.EnglishAlphabetSize; letterIndex++)
            {
                var upperCaseLetterAsciiValue = AsciiValueOfUpperCaseA + letterIndex;
                var lowerCaseLetterAsciiValue = AsciiValueOfLowerCaseA + letterIndex;

                AlphabeticLettersByAsciiValues[upperCaseLetterAsciiValue] =
                    AlphabeticLettersByAsciiValues[lowerCaseLetterAsciiValue] = (letterIndex + 1).ToAlphabeticLetter();
            }
        }

        /// <summary>
        ///     Gets the alphabetic letters indexed by their ASCII values.
        /// </summary>
        public static AlphabeticLetters[] AlphabeticLettersByAsciiValues { get; } =
            new AlphabeticLetters[byte.MaxValue + 1];

        /// <summary>
        ///     Gets the ASCII byte value for the given letter, assuming can be encoded in ASCII.
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