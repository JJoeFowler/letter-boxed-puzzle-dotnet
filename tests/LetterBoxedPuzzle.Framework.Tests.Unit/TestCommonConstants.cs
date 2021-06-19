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
        ///     Name of animals as test words.
        /// </summary>
        internal static readonly string[] AnimalTestWords = { "Aardvark", "BEAR", "cat", "doG", "elephant", "FoX", "GORILLA", "harE", "Zebra" };

        /// <summary>
        ///     The characters of the lowercase alphabet from 'a' to 'z'.
        /// </summary>
        internal static readonly char[] LowercaseAlphabet =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z',
            };

        /// <summary>
        ///     The characters of the uppercase alphabet from 'A' to 'Z'.
        /// </summary>
        internal static readonly char[] UppercaseAlphabet =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z',
            };

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