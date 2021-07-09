// ===============================================================================================================================================
// <copyright file="TestCommonConstants.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Enums;

    using static Constants.AlphabetConstants;

    /// <summary>
    ///     Common constants used for unit tests.
    /// </summary>
    internal static class TestCommonConstants
    {
        /// <summary>
        ///     Size of the alphabet.
        /// </summary>
        internal const int AlphabetSize = 26;

        /// <summary>
        ///     The lowercase alphabet test string.
        /// </summary>
        internal const string LowercaseAlphabetTestString = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        ///     The uppercase alphabet test string.
        /// </summary>
        internal const string UppercaseAlphabetTestString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        ///     Simple test word.
        /// </summary>
        internal const string SimpleTestWord = nameof(SimpleTestWord);

        /// <summary>
        ///     Simple test word alphabet bit mask.
        /// </summary>
        internal const AlphabetBitMask SimpleTestWordAlphabetBitMask = AlphabetBitMask.S | AlphabetBitMask.I | AlphabetBitMask.M
            | AlphabetBitMask.P | AlphabetBitMask.L | AlphabetBitMask.E | AlphabetBitMask.T | AlphabetBitMask.W | AlphabetBitMask.O
            | AlphabetBitMask.R | AlphabetBitMask.D;

        /// <summary>
        ///     A-to-Z test word.
        /// </summary>
        internal const string AToZTestWord = "TheQuickBrownFoxJumpsOverTheLazyDog";

        /// <summary>
        ///     The lowercase test alphabet.
        /// </summary>
        internal static readonly char[] LowercaseTestAlphabet =
            Enumerable.Range(0, AlphabetSize).Select(letter => LowercaseAlphabetTestString[letter]).ToArray();

        /// <summary>
        ///     The uppercase test alphabet.
        /// </summary>
        internal static readonly char[] UppercaseTestAlphabet =
            Enumerable.Range(0, AlphabetSize).Select(letter => UppercaseAlphabetTestString[letter]).ToArray();

        /// <summary>
        ///     The test white space strings.
        /// </summary>
        internal static readonly string[] TestWhitespaceStrings = { " ", "  ", "\t", "\n", "\r", "\t\n ", "\r\t", "\n\r", "\r\n", " \t \n \r " };

        /// <summary>
        ///     The test strings with the first letter lowercased.
        /// </summary>
        internal static readonly string[] TestFirstCharLowercasedStrings = { "abc", "d ef", "gH i", "jKL", "mn  O", "p qR ", "sTU", "vwX", "yZ " };

        /// <summary>
        ///     Corresponding test strings with the first letter uppercased.
        /// </summary>
        internal static readonly string[] TestFirstCharUppercasedStrings = { "Abc", "D ef", "GH i", "JKL", "Mn  O", "P qR ", "STU", "VwX", "YZ " };

        /// <summary>
        ///     Name of animals as test words.
        /// </summary>
        internal static readonly string[] AnimalTestWords = { "Aardvark", "BEAR", "cat", "doG", "elephant", "FoX", "GORILLA", "harE", "Zebra" };

        /// <summary>
        ///     The non-alphabet characters.
        /// </summary>
        internal static readonly char[] NonAlphabetCharacters = Enumerable.Range(0, char.MaxValue).Select(x => (char)x).ToArray()
            .Except(LowercaseAlphabet).Except(UppercaseAlphabet).ToArray();

        /// <summary>
        ///     The test alphabet bit masks for the full alphabet.
        /// </summary>
        internal static readonly AlphabetBitMask[] AllSingleLetterAlphabetBitMasks =
            {
                AlphabetBitMask.A, AlphabetBitMask.B, AlphabetBitMask.C, AlphabetBitMask.D, AlphabetBitMask.E, AlphabetBitMask.F,
                AlphabetBitMask.G, AlphabetBitMask.H, AlphabetBitMask.I, AlphabetBitMask.J, AlphabetBitMask.K, AlphabetBitMask.L,
                AlphabetBitMask.M, AlphabetBitMask.N, AlphabetBitMask.O, AlphabetBitMask.P, AlphabetBitMask.Q, AlphabetBitMask.R,
                AlphabetBitMask.S, AlphabetBitMask.T, AlphabetBitMask.U, AlphabetBitMask.V, AlphabetBitMask.W, AlphabetBitMask.X,
                AlphabetBitMask.Y, AlphabetBitMask.Z,
            };

        /// <summary>
        ///     Test letter groups for three sides.
        /// </summary>
        internal static readonly string[] TestLetterGroupsForThreeSides = { "abc", "DEF", "gHi" };

        /// <summary>
        ///     Test letter groups for nine sides.
        /// </summary>
        internal static readonly string[] TestLetterGroupsForNineSides = { "abc", "Def", "gHi", "jkL", "LMn", "PqR", "sTU", "UVW", "yZ" };
    }
}