// ===============================================================================================================================================
// <copyright file="AlphabetUtilities.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Utilities
{
    using System;
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
        ///     The minimum length of the text used to generate two-letter pairs.
        /// </summary>
        internal const int MinimumTwoLetterPairsTextLength = 2;

        /// <summary>
        ///     The bit-wise enumerated letters of the alphabet indexed by their byte values.
        /// </summary>
        internal static readonly AlphabetBitMask[] AlphabetBitMaskByByteValue = new AlphabetBitMask[byte.MaxValue + 1];

        /// <summary>
        ///     A lookup Boolean array indexed by characters to determine whether a given character is a lowercase letter of the alphabet.
        /// </summary>
        private static readonly bool[] IsLowercaseAlphabetLetterByCharacter = new bool[char.MaxValue + 1];

        /// <summary>
        ///     A lookup Boolean array indexed by characters to determine whether a given character is an uppercase letter of the alphabet.
        /// </summary>
        private static readonly bool[] IsUppercaseAlphabetLetterByCharacter = new bool[char.MaxValue + 1];

        /// <summary>
        ///     A lookup Boolean array indexed by characters to determine whether a given character is a letter of the alphabet.
        /// </summary>
        private static readonly bool[] IsAlphabetLetterByCharacter = new bool[char.MaxValue];

        /// <summary>
        ///     Initializes static members of the <see cref="AlphabetUtilities" /> class.
        /// </summary>
        static AlphabetUtilities()
        {
            for (int index = AlphabetConstants.LowercaseA; index <= AlphabetConstants.LowercaseZ; index++)
            {
                IsLowercaseAlphabetLetterByCharacter[index] = true;
                IsAlphabetLetterByCharacter[index] = true;
                AlphabetBitMaskByByteValue[index] = (index - AlphabetConstants.LowercaseA + 1).ToAlphabetBitMask();
            }

            for (int index = AlphabetConstants.UppercaseA; index <= AlphabetConstants.UppercaseZ; index++)
            {
                IsUppercaseAlphabetLetterByCharacter[index] = true;
                IsAlphabetLetterByCharacter[index] = true;
                AlphabetBitMaskByByteValue[index] = (index - AlphabetConstants.UppercaseA + 1).ToAlphabetBitMask();
            }
        }

        /// <summary>
        ///     Using a fast lookup method, determine whether the given character is an alphabet letter, which is either between 'a' and 'z'
        ///     or between 'A' and 'Z'.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>
        ///     <see langword="true" /> if given character is an alphabet letter, or <see langword="false" /> otherwise.
        /// </returns>
        public static bool IsAlphabetLetter(char character) => IsAlphabetLetterByCharacter[character];

        /// <summary>
        ///     Determine whether the given character is a lowercase alphabet letter, which is between 'a' and 'z'.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>
        ///     <see langword="true" /> if given character is a lowercase alphabet letter, or <see langword="false" /> otherwise.
        /// </returns>
        public static bool IsLowercaseAlphabetLetter(char character) => IsLowercaseAlphabetLetterByCharacter[character];

        /// <summary>
        ///     Determine whether the given character is an uppercase alphabet letter, which is between 'A' and 'Z'.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>
        ///     <see langword="true" /> if given character is an uppercase alphabet letter, or <see langword="false" /> otherwise.
        /// </returns>
        public static bool IsUppercaseAlphabetLetter(char character) => IsUppercaseAlphabetLetterByCharacter[character];

        /// <summary>
        ///     Gets the byte value for the given character assuming it can be converted to a byte.
        /// </summary>
        /// <param name="character">A single-character letter.</param>
        /// <returns>The  byte value of the given letter.</returns>
        /// <exception cref="OverflowException">Thrown when the given unicode character is outside the range of converting to a byte.</exception>
        public static byte GetByteValue(char character)
        {
            try
            {
                return Convert.ToByte(character);
            }
            catch (OverflowException overflowException)
            {
                throw new OverflowException(
                    $"Given unicode character '{character.ToString()}' is outside of the range of a byte.",
                    overflowException);
            }
        }

        /// <summary>
        ///     Get the alphabet bit mask value for the given alphabet letter.
        /// </summary>
        /// <param name="alphabetLetter">The alphabet letter.</param>
        /// <returns>The bit mask of the given letter.</returns>
        /// <exception cref="ArgumentException">Thrown when given a character not in the alphabet.</exception>
        public static AlphabetBitMask GetAlphabetBitMask(char alphabetLetter) =>
            alphabetLetter switch
                {
                    _ when !IsAlphabetLetter(alphabetLetter) => throw new ArgumentException(
                        $"'{nameof(alphabetLetter)}' must be a letter of the alphabet."),

                    _ => AlphabetBitMaskByByteValue[GetByteValue(alphabetLetter)],
                };

        /// <summary>
        ///     Get the alphabet bit mask value for the given alphabet letters.
        /// </summary>
        /// <param name="alphabetLetters">The alphabet letters.</param>
        /// <returns>The bit mask of the given letter.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when given a non-alphabet letter.</exception>
        public static AlphabetBitMask GetAlphabetBitMask(string alphabetLetters) =>
            alphabetLetters switch
                {
                    null => throw new ArgumentNullException(nameof(alphabetLetters)),

                    _ => alphabetLetters.Aggregate(
                        AlphabetBitMask.None,
                        (current, alphabetLetter) => current | GetAlphabetBitMask(alphabetLetter)),
                };

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
        /// <exception cref="ArgumentException">Thrown when given a starting letter that is not in the alphabet.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when not given a length less than 1 or a length out of range that would lead to non-alphabetic sequence.
        /// </exception>
        public static char[] GenerateAlphabeticRangeSequence(char startingLetter, int length)
        {
            var maximumLength = AlphabetConstants.EnglishAlphabetSize - startingLetter
                + (IsLowercaseAlphabetLetter(startingLetter) ? AlphabetConstants.LowercaseA : AlphabetConstants.UppercaseA);

            return (startingLetter, length) switch
                {
                    (_, _) when !IsAlphabetLetter(startingLetter) => throw new ArgumentException(
                        $"'{nameof(startingLetter)}' must be a letter of the alphabet."),

                    (_, _) when length < 1 => throw new ArgumentOutOfRangeException($"The value of '{nameof(length)}' must be at least 1."),

                    (_, _) when length > maximumLength => throw new ArgumentOutOfRangeException(
                        $"The value of '{nameof(length)}' for '{nameof(startingLetter)}' cannot exceed maximum length of {maximumLength}."),

                    (_, _) => Enumerable.Range(startingLetter, length).Select(x => (char)x).ToArray(),
                };
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
        /// <exception cref="ArgumentException">Thrown when given a starting letter that is not in the alphabet.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when not given a length less than 1 or a length out of range that would lead to non-alphabetic text.
        /// </exception>
        public static string GenerateAlphabeticRangeAsText(char startingLetter, int length) =>
            new (GenerateAlphabeticRangeSequence(startingLetter, length));

        /// <summary>
        ///     Generate all distinct two-letter pairs of letters for the given text, including the pairs of the same letter.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>All (<i>n</i> choose 2 + <i>n</i>) two-letter pairs consisting of letters of the given text of length <i>n</i>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when given text contains one or more characters not in the alphabet.</exception>
        public static string[] GenerateAllDistinctTwoLetterPairs(string text) =>
            text switch
                {
                    null => throw new ArgumentNullException(nameof(text)),

                    _ when text.Length < MinimumTwoLetterPairsTextLength => throw new ArgumentException(
                        $"'{nameof(text)}' must have at least {MinimumTwoLetterPairsTextLength} characters."),

                    _ when !text.All(IsAlphabetLetter) => throw new ArgumentException($"{nameof(text)}' must be letters of the alphabet."),

                    _ => (from firstLetter in text.ToLowerInvariant()
                          from secondLetter in text.ToLowerInvariant()
                          select firstLetter + secondLetter.ToString()).Distinct().ToArray(),
                };
    }
}