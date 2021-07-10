// ===============================================================================================================================================
// <copyright file="WordArchive.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Class for word archive, containing all the specified words and their corresponding candidate words.
    /// </summary>
    public class WordArchive
    {
        /// <summary>
        ///     All the words of the archive.
        /// </summary>
        private readonly string[] allWords;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WordArchive" /> class.
        /// </summary>
        /// <param name="wordText">The word text, white-space delimited.</param>
        public WordArchive(string wordText)
        {
            this.allWords = wordText switch
                {
                    null => throw new ArgumentNullException(nameof(wordText)),

                    _ => Regex.Split(wordText.ToLowerInvariant(), @"\s+").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray(),
                };

            this.AllCandidateWordsByName = GetCandidateWordsByName(this.allWords);
        }

        /// <summary>
        ///     Gets a copy of all of the words of the archive.
        /// </summary>
        public string[] AllWords => this.allWords.ToArray();

        /// <summary>
        ///     Gets the mapping of all words to their corresponding candidate word, which include the alphabet bit mask and sequential letter
        ///     pairs of the word for quick computation of whether the word is permitted for a given letter boxed puzzle.
        /// </summary>
        public IReadOnlyDictionary<string, CandidateWord> AllCandidateWordsByName { get; }

        /// <summary>
        ///     Gets the permitted words for the letter boxed puzzle, which are those words without any sequential letters that are one
        ///     of the forbidden pairs formed from the specified side letters of the puzzle.
        /// </summary>
        /// <param name="sideLetters">The side letters of the puzzle.</param>
        /// <returns>The words allowed by the puzzle.</returns>
        public string[] GetPermittedPuzzleWords(SideLetters sideLetters) =>
            this.allWords.Where(word => this.AllCandidateWordsByName[word].IsAllowed(sideLetters)).ToArray();

        /// <summary>
        ///     Gets the permitted candidate words for the specified side letters of the puzzle, which includes their alphabet bit masks
        ///     and sequential letter pairs for each candidate word.
        /// </summary>
        /// <param name="sideLetters">The side letters of the puzzle.</param>
        /// <returns>The candidate words for the specified puzzle side letters.</returns>
        public CandidateWord[] GetPuzzleCandidateWords(SideLetters sideLetters) =>
            this.GetPermittedPuzzleWords(sideLetters).Select(word => this.AllCandidateWordsByName[word]).ToArray();

        /// <summary>
        ///     Gets the candidate words keyed by their name for the given words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns>The candidate words keyed by their name.</returns>
        private static IReadOnlyDictionary<string, CandidateWord> GetCandidateWordsByName(IEnumerable<string> words) =>
            words.Select(word => new CandidateWord(word)).Distinct().ToDictionary(candidate => candidate.LowercaseWord, candidate => candidate);
    }
}