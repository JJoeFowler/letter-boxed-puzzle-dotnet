// ===============================================================================================================================================
// <copyright file="CandidateWord.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Linq;
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
        /// <param name="word">The word consisting of only alphabet letters.</param>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when given an empty string or word with non-alphabet letters.</exception>
        public CandidateWord(string word)
        {
            _ = word ?? throw new ArgumentNullException(nameof(word));

            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Cannot use empty string to initialize a candidate word.");
            }

            if (!word.All(IsAlphabetLetter))
            {
                throw new ArgumentException($"Cannot use '{word}' containing non-alphabet letters to initialize a candidate word.");
            }

            this.LowercaseWord = word.ToLowerInvariant();

            this.AsciiSequence = Encoding.ASCII.GetBytes(this.LowercaseWord);

            foreach (var asciiValue in this.AsciiSequence)
            {
                var alphabetBitMaskByAsciiValue = AlphabetBitMaskByAsciiValues[asciiValue];
                this.AlphabetBitMask |= alphabetBitMaskByAsciiValue;
            }

            this.FirstLetter = this.LowercaseWord[0];
            this.LastLetter = this.LowercaseWord[^1];
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
        ///     Gets the lowercase word used to initialize the candidate, where lowercase was used since case is irrelevant.
        /// </summary>
        public string LowercaseWord { get; }

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
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when given one or more non-alphabet letters.</exception>
        public bool IsContainedIn(string candidateLetters)
        {
            _ = candidateLetters ?? throw new ArgumentNullException(nameof(candidateLetters));

            if (!candidateLetters.All(IsAlphabetLetter))
            {
                throw new ArgumentException($"Given candidate letters {candidateLetters} are not all letters of the alphabet.");
            }

            return (this.AlphabetBitMask & (AlphabetConstants.AlphabetBitMaskWithAllBitsSet ^ GetAlphabetBitMask(candidateLetters))) == 0;
        }

        /// <summary>
        ///     Determines whether the candidate words is an allowed word for a letter-boxed puzzle given its side letters, where no two
        ///     adjacent letters in the word can be a forbidden two-letter pair.
        /// </summary>
        /// <param name="sideLetters">The side letters of a letter-boxed puzzle.</param>
        /// <returns>
        ///     <see langword="true" /> if this candidate word is allowed for given the side letters, or <see langword="false" /> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value for the side letters.</exception>
        public bool IsAllowed(SideLetters sideLetters)
        {
            _ = sideLetters ?? throw new ArgumentNullException(nameof(sideLetters));

            var lastLetter = ' ';
            foreach (var letter in this.LowercaseWord)
            {
                if (sideLetters.IsForbiddenTwoLetterPair(lastLetter + letter.ToString()))
                {
                    return false;
                }

                lastLetter = letter;
            }

            return true;
        }
    }
}