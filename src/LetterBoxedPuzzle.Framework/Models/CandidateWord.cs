// ===============================================================================================================================================
// <copyright file="CandidateWord.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Text;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Enums;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     Class for a case-insensitive candidate word for a letter boxed puzzle, containing the ASCII sequence and its alphabet bit mask.
    /// </summary>
    public class CandidateWord
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CandidateWord" /> class.
        /// </summary>
        /// <param name="word">The word.</param>
        public CandidateWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException("Cannot use null, empty string, or white space to initialize a candidate word.");
            }

            this.CaseInsensitiveWord = word.ToLowerInvariant();

            this.AsciiSequence = Encoding.ASCII.GetBytes(this.CaseInsensitiveWord);

            foreach (var asciiValue in this.AsciiSequence)
            {
                var alphabetBitMaskByAsciiValue = AlphabetBitMaskByAsciiValues[asciiValue];
                this.AlphabetBitMask |= alphabetBitMaskByAsciiValue;
            }

            this.FirstLetter = word[0];
            this.LastLetter = word[^1];
        }

        /// <summary>
        ///     Gets the first letter of the word.
        /// </summary>
        public char FirstLetter { get; }

        /// <summary>
        ///     Gets the first letter of the word.
        /// </summary>
        public char LastLetter { get; }

        /// <summary>
        ///     Gets the case-insensitive word used to initialize the candidate, which is lowercased since case is irrelevant.
        /// </summary>
        public string CaseInsensitiveWord { get; }

        /// <summary>
        ///     Gets the byte values corresponding to the ASCII sequence of the word.
        /// </summary>
        public byte[] AsciiSequence { get; }

        /// <summary>
        ///     Gets the alphabet bit mask of the word where '<see cref="Enums.AlphabetBitMask.A" />', ..., <see cref="Enums.AlphabetBitMask.Z" />
        ///     correspond to bits 1, ..., 26 to the whether the word contains the respective letters.
        /// </summary>
        public AlphabetBitMask AlphabetBitMask { get; internal set; }

        /// <summary>
        ///     Determines whether all the letters of the candidate word are contained within the given candidate letters.
        /// </summary>
        /// <param name="candidateLetters">The candidate letters.</param>
        /// <returns>
        ///     <see langword="true" /> if all the letters of the candidate word are contained in the given letters, or <see langword="false" /> otherwise.
        /// </returns>
        public bool IsContainedIn(string candidateLetters)
        {
            return (this.AlphabetBitMask & (AlphabetConstants.AlphabetBitMaskWithAllBitsSet.Value ^ GetAlphabetBitMask(candidateLetters))) == 0;
        }
    }
}