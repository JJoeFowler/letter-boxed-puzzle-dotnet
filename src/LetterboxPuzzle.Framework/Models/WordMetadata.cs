// ===============================================================================================================================================
// <copyright file="WordMetadata.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterboxPuzzle.Framework.Models
{
    using System;
    using System.Text;

    using LetterboxPuzzle.Framework.Enums;
    using LetterboxPuzzle.Framework.Extensions;

    /// <summary>
    ///     Word metadata class containing char array of the word's letters and bit integer properties.
    /// </summary>
    public class WordMetadata
    {
        /// <summary>
        ///     The size of the English alphabet.
        /// </summary>
        public const int EnglishAlphabetSize = 26;

        /// <summary>
        ///     The lower case letter 'a'.
        /// </summary>
        public const string LowerCaseA = "a";

        /// <summary>
        ///     The upper case letter 'A'.
        /// </summary>
        public const string UpperCaseA = "A";

        /// <summary>
        ///     ASCII value of the upper case 'A'.
        /// </summary>
        public static readonly byte AsciiValueOfUpperCaseA = GetAsciiValue(UpperCaseA);

        /// <summary>
        ///     ASCII value of the lower case 'a'.
        /// </summary>
        public static readonly byte AsciiValueOfLowerCaseA = GetAsciiValue(LowerCaseA);

        /// <summary>
        ///     Array of letters enumeration indexed by their ASCII values.
        /// </summary>
        private static readonly AlphabeticLetters[] LettersByAsciiValues = new AlphabeticLetters[byte.MaxValue + 1];

        /// <summary>
        ///     Initializes static members of the <see cref="WordMetadata" /> class.
        /// </summary>
        static WordMetadata()
        {
            for (var letterIndex = 0; letterIndex < EnglishAlphabetSize; letterIndex++)
            {
                LettersByAsciiValues[AsciiValueOfUpperCaseA + letterIndex] =
                    LettersByAsciiValues[AsciiValueOfLowerCaseA + letterIndex] = letterIndex.ToAlphabeticLetter();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WordMetadata" /> class.
        /// </summary>
        /// <param name="word">The word.</param>
        public WordMetadata(string word)
        {
            this.Word = word.ToUpper();

            this.AsciiValues = Encoding.ASCII.GetBytes(this.Word);

            foreach (var asciiValue in this.AsciiValues)
            {
                this.LettersMask |= LettersByAsciiValues[asciiValue];
            }
        }

        /// <summary>
        ///     Gets the word.
        /// </summary>
        public string Word { get; }

        /// <summary>
        ///     Gets the ASCII byte values of the word.
        /// </summary>
        public byte[] AsciiValues { get; }

        /// <summary>
        ///     Gets the bit-mask of the word where the bits 1 to 26 correspond to the whether the word contains the letters A through Z.
        /// </summary>
        public AlphabeticLetters LettersMask { get; internal set; }

        /// <summary>
        ///     Gets the ASCII byte value for the given letter, assuming can be encoded in ASCII.
        /// </summary>
        /// <param name="letter">A single-character letter.</param>
        /// <returns>The ASCII byte value of the given letter.</returns>
        private static byte GetAsciiValue(string letter)
        {
            _ = letter ?? throw new ArgumentNullException(nameof(letter));

            return Encoding.ASCII.GetBytes(letter)[0];
        }
    }
}