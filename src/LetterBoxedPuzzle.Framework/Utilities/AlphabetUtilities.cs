// ===============================================================================================================================================
// <copyright file="AlphabetUtilities.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Enums;
    using LetterBoxedPuzzle.Framework.Extensions;

    /// <summary>
    ///     Class containing utility methods related to the alphabet.
    /// </summary>
    public static class AlphabetUtilities
    {
        /// <summary>
        ///     The bit-wise enumerated letters of the alphabet indexed by their ASCII values.
        /// </summary>
        internal static readonly AlphabetBitMask[] AlphabetBitMaskByAsciiValues = Enumerable.Range(1, AlphabetConstants.EnglishAlphabetSize)
            .Aggregate(
                new AlphabetBitMask[byte.MaxValue],
                (current, index) =>
                {
                    var upperCaseLetterAsciiValue = GetExtendedAsciiValue(AlphabetConstants.UpperCaseA) + index - 1;
                    var lowerCaseLetterAsciiValue = GetExtendedAsciiValue(AlphabetConstants.LowerCaseA) + index - 1;

                    current[upperCaseLetterAsciiValue] = current[lowerCaseLetterAsciiValue] = index.ToAlphabetBitMask();

                    return current;
                }).ToArray();

        /// <summary>
        ///     Lookup Boolean dictionary keyed by characters to determine whether a given character is a letter of the alphabet.
        /// </summary>
        private static readonly Dictionary<char, bool> IsAlphabetLetterByLetter = Enumerable.Range(0, char.MaxValue).Select(x => (char)x)
            .Aggregate(
                new Dictionary<char, bool>(),
                (current, character) =>
                {
                    current[character] = AlphabetConstants.FullAlphabetSequence.Contains(character);
                    return current;
                });

        /// <summary>
        ///     Using a fast lookup method, determine whether the given character is an alphabet letter, which is either between 'a' and 'z'
        ///     or between 'A' and 'Z'.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>
        ///     <see langword="true" /> if given character is an alphabet letter, or <see langword="false" /> otherwise.
        /// </returns>
        public static bool IsAlphabetLetter(char character)
        {
            return IsAlphabetLetterByLetter[character];
        }

        /// <summary>
        ///     Gets the extended ASCII byte value for the given letter.
        /// </summary>
        /// <param name="letter">A single-character letter.</param>
        /// <returns>The extended ASCII byte value of the given letter.</returns>
        /// <exception cref="OverflowException">Thrown when the given unicode character is outside the extended ASCII range.</exception>
        public static byte GetExtendedAsciiValue(char letter)
        {
            try
            {
                return Convert.ToByte(letter);
            }
            catch (OverflowException overflowException)
            {
                throw new OverflowException(
                    $"Given unicode character '{letter}' cannot be outside of the extended ASCII range.",
                    overflowException);
            }
        }

        /// <summary>
        ///     Get the alphabet bit mask value for the given alphabet letter.
        /// </summary>
        /// <param name="alphabetLetter">The alphabet letter.</param>
        /// <returns>The bit mask of the given letter.</returns>
        /// <exception cref="ArgumentException">Thrown when given a character not in the alphabet.</exception>
        public static AlphabetBitMask GetAlphabetBitMask(char alphabetLetter)
        {
            if (!IsAlphabetLetter(alphabetLetter))
            {
                throw new ArgumentException($"Given character {alphabetLetter} must be an alphabet letter.");
            }

            return AlphabetBitMaskByAsciiValues[GetExtendedAsciiValue(alphabetLetter)];
        }

        /// <summary>
        ///     Get the alphabet bit mask value for the given alphabet letters.
        /// </summary>
        /// <param name="alphabetLetters">The alphabet letters.</param>
        /// <returns>The bit mask of the given letter.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when given a non-alphabet letter.</exception>
        public static AlphabetBitMask GetAlphabetBitMask(string alphabetLetters)
        {
            _ = alphabetLetters ?? throw new ArgumentNullException(nameof(alphabetLetters));

            return alphabetLetters.Aggregate(AlphabetBitMask.None, (current, alphabetLetter) => current | GetAlphabetBitMask(alphabetLetter));
        }

        /// <summary>
        ///     <para>
        ///         Generates an alphabetic range as a sequence of characters starting with the given letter and going alphabetically to the
        ///         next letter for the specified length.
        ///     </para>
        ///     <para>
        ///         For example, { "a", "b", ..., "z" } is the alphabetic range array starting at 'a' with a length of 26.
        ///     </para>
        /// </summary>
        /// <param name="startingLetter">The starting letter.</param>
        /// <param name="length">The length of the range.</param>
        /// <returns>The specified alphabetic range as a <see langword="char" /> array.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when given a length of zero or less.</exception>
        /// <exception cref="ArgumentException">Thrown when given a starting letter that is not in the alphabet.</exception>
        public static char[] GenerateAlphabeticRangeSequence(char startingLetter, int length)
        {
            if (!IsAlphabetLetter(startingLetter))
            {
                throw new ArgumentException($"Given starting letter {startingLetter} must be a letter of the alphabet.");
            }

            return Enumerable.Range(startingLetter, length).Select(x => (char)x).ToArray();
        }

        /// <summary>
        ///     <para>
        ///         Generates an alphabetic range as a string starting with the given letter and going alphabetically to the next letter for
        ///         the specified length.
        ///     </para>
        ///     <para>
        ///         For example, "abcdefghijklmnopqrstuvwxyz" is the alphabetic range starting at 'a' with a length of 26.
        ///     </para>
        /// </summary>
        /// <param name="startingLetter">The starting letter.</param>
        /// <param name="length">The length of the range.</param>
        /// <returns>Concatenated <see langword="string" /> of the specified alphabetic range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when given a length of zero or less.</exception>
        /// <exception cref="ArgumentException">Thrown when given a starting letter that is not in the alphabet.</exception>
        public static string GenerateAlphabeticRangeAsText(char startingLetter, int length)
        {
            return new string(GenerateAlphabeticRangeSequence(startingLetter, length));
        }

        /// <summary>
        ///     Generate all distinct two-letter pairs of letters for the given text, including the pairs of the same letter.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>All (<i>n</i> choose 2 + <i>n</i>) two-letter pairs consisting of letters of the given text of length <i>n</i>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when given text contains one or more characters not in the alphabet.</exception>
        public static string[] GenerateAllDistinctLetterPairs(string text)
        {
            _ = text ?? throw new ArgumentNullException(nameof(text));

            if (!text.All(IsAlphabetLetter))
            {
                throw new ArgumentException($"Given '{text}' can only contain letters in the alphabet.");
            }

            return (from firstLetter in text
                    from secondLetter in text
                    select firstLetter + secondLetter.ToString()).Distinct().ToArray();
        }
    }
}