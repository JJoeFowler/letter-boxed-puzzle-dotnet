// ===============================================================================================================================================
// <copyright file="WordLink.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Linq;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     Class for a word link, consisting of the candidate words with specified first and last letters, where either can be null allowing
    ///     for any first or last letter, respectively.
    /// </summary>
    public class WordLink
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WordLink" /> class.
        /// </summary>
        /// <param name="candidateWords">The candidate words.</param>
        /// <param name="firstLetter">The first letter lowercased if specified or null if not.</param>
        /// <param name="lastLetter">The last letter lowercased if specified or null if not.</param>
        public WordLink(CandidateWord[] candidateWords, char? firstLetter, char? lastLetter)
        {
            (this.MatchingCandidateWords, this.FirstLetter, this.LastLetter) = (candidateWords, firstLetter, lastLetter) switch
                {
                    (null, _, _) => throw new ArgumentNullException(nameof(candidateWords)),

                    (_, _, _) when firstLetter.HasValue && !IsLowercaseAlphabetLetter(firstLetter.Value) => throw new ArgumentException(
                        $"'{nameof(firstLetter)}' must be a lowercase letter of the alphabet."),

                    (_, _, _) when lastLetter.HasValue && !IsLowercaseAlphabetLetter(lastLetter.Value) => throw new ArgumentException(
                        $"'{nameof(lastLetter)}' must be a lowercase letter of the alphabet."),

                    (_, _, _) => (
                        candidateWords.Where(word => !firstLetter.HasValue || (word.FirstLetter == firstLetter))
                            .Where(word => !lastLetter.HasValue || (word.LastLetter == lastLetter)).ToArray(), firstLetter, lastLetter),
                };
        }

        /// <summary>
        ///     Gets the first letter, if any, for the linking words.
        /// </summary>
        public char? FirstLetter { get; }

        /// <summary>
        ///     Gets the last letter, if any, for the linking words.
        /// </summary>
        public char? LastLetter { get; }

        /// <summary>
        ///     Gets the matching candidate words starting with the first letter, if non-null, and ending with the last letter, if non-null.
        /// </summary>
        public CandidateWord[] MatchingCandidateWords { get; }
    }
}