// ===============================================================================================================================================
// <copyright file="AlphabetConstants.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Constants
{
    using LetterBoxedPuzzle.Framework.Utilities;

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
        ///     Alphabet range text from 'a' to 'z', which is the string "abcdefghijklmnopqrstuvwxyz".
        /// </summary>
        public static readonly string AlphabetRangeTextFromAToZ = AlphabetUtilities.AlphabetRangeText(LowerCaseA, EnglishAlphabetSize);
    }
}