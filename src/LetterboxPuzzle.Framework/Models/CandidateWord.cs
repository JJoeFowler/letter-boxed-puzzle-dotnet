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
    ///     Candidate word class containing the ASCII sequence of the word's letters and its alphabetic letters bit-wise enumeration.
    /// </summary>
    public class CandidateWord
    {
        /// <summary>
        ///     Local copy of alphabetic letters by ASCII values to speed array access.
        /// </summary>
        private static readonly AlphabeticLetters[] AlphabeticLettersByAsciiValues = AlphabeticUtilities.AlphabeticLettersByAsciiValues;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CandidateWord" /> class.
        /// </summary>
        /// <param name="word">The word.</param>
        public CandidateWord(string word)
        {
            this.WordLowercased = word.ToLowerInvariant();

            this.AsciiSequence = Encoding.ASCII.GetBytes(this.WordLowercased);

            foreach (var asciiValue in this.AsciiSequence)
            {
                this.AlphabeticLetters |= AlphabeticLettersByAsciiValues[asciiValue];
            }
        }

        /// <summary>
        ///     Gets the word used to initialize the candidate, which is lowercased since case is irrelevant.
        /// </summary>
        public string WordLowercased { get; }

        /// <summary>
        ///     Gets the byte values corresponding to the ASCII sequence of the word.
        /// </summary>
        public byte[] AsciiSequence { get; }

        /// <summary>
        ///     Gets the alphabetic letters of the word (where bits 1 to 26 correspond to the whether the word contains the letters
        ///     <see cref="AlphabeticLetters.A"/> to <see cref="AlphabeticLetters.Z"/>, respectively.
        /// </summary>
        public AlphabeticLetters AlphabeticLetters { get; internal set; }

        /// <summary>
        ///     Determines whether all the letters of the candidate word are contained within the given candidate letters.
        /// </summary>
        /// <param name="candidateLetters">The candidate letters.</param>
        /// <returns>
        ///     <see langword="true" /> if all the letters of the candidate word are contained in the given letters, or <see langword="false" /> otherwise.
        /// </returns>
        public bool IsContainedIn(CandidateWord candidateLetters)
        {
            return (this.AlphabeticLetters & (AlphabeticLettersExtensions.AllAlphabeticLetters ^ this.AlphabeticLetters)) > 0;
        }
    }
}