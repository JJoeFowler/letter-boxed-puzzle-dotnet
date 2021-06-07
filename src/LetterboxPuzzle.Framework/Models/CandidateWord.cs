// ===============================================================================================================================================
// <copyright file="CandidateWord.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterboxPuzzle.Framework.Models
{
    using System.Text;

    using LetterboxPuzzle.Framework.Enums;
    using LetterboxPuzzle.Framework.Extensions;
    using LetterboxPuzzle.Framework.Utilities;

    /// <summary>
    ///     Class for a case-insensitive candidate word for a letterbox puzzle, containing the ASCII sequence and its bit-wise enumerated value
    ///     of the word's lowercased letters.
    /// </summary>
    public class CandidateWord
    {
        /// <summary>
        ///     Local copy of alphabetic letters by ASCII values to speed array access.
        /// </summary>
        private static readonly AlphabetLetters[] AlphabetLettersByAsciiValues = AlphabetUtilities.AlphabetLettersByAsciiValues;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CandidateWord" /> class.
        /// </summary>
        /// <param name="word">The word.</param>
        public CandidateWord(string word)
        {
            this.CaseInsensitiveWord = word.ToLowerInvariant();

            this.AsciiSequence = Encoding.ASCII.GetBytes(this.CaseInsensitiveWord);

            foreach (var asciiValue in this.AsciiSequence)
            {
                this.AlphabetBitMask |= AlphabetLettersByAsciiValues[asciiValue];
            }
        }

        /// <summary>
        ///     Gets the case-insensitive word used to initialize the candidate, which is lowercased since case is irrelevant.
        /// </summary>
        public string CaseInsensitiveWord { get; }

        /// <summary>
        ///     Gets the byte values corresponding to the ASCII sequence of the word.
        /// </summary>
        public byte[] AsciiSequence { get; }

        /// <summary>
        ///     Gets the alphabet bitmask of the word (where bits 1 to 26 correspond to the whether the word contains the letters
        ///     <see cref="Enums.AlphabetLetters.A" /> to <see cref="Enums.AlphabetLetters.Z" />, respectively.
        /// </summary>
        public AlphabetLetters AlphabetBitMask { get; internal set; }

        /// <summary>
        ///     Determines whether all the letters of the candidate word are contained within the given candidate letters.
        /// </summary>
        /// <param name="candidateLetters">The candidate letters.</param>
        /// <returns>
        ///     <see langword="true" /> if all the letters of the candidate word are contained in the given letters, or <see langword="false" /> otherwise.
        /// </returns>
        public bool IsContainedIn(CandidateWord candidateLetters)
        {
            return (this.AlphabetBitMask & (AlphabetExtensions.AllAlphabetLettersBitMask ^ candidateLetters.AlphabetBitMask)) == 0;
        }
    }
}