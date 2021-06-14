﻿// ===============================================================================================================================================
// <copyright file="WordArchive.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Class for word archive, containing all the legal words as candidate words.
    /// </summary>
    public class WordArchive
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WordArchive" /> class.
        /// </summary>
        /// <param name="whiteSpaceDelimitedAllowedWordText">The text of allowed words, white-spaced delimited.</param>
        public WordArchive(string whiteSpaceDelimitedAllowedWordText)
        {
            this.AllowedWords = Regex.Split(whiteSpaceDelimitedAllowedWordText.ToLowerInvariant(), @"\s+").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        }

        /// <summary>
        ///     Gets the dictionary of allowed candidate words keyed by their name.
        /// </summary>
        public IDictionary<string, CandidateWord> CandidateWordsByName => GetCandidateWordsByName(this.AllowedWords);

        /// <summary>
        ///     Gets the allowed words for the archive.
        /// </summary>
        public string[] AllowedWords { get; }

        /// <summary>
        ///     Gets the candidate words keyed by their name for the given words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns>The candidate words keyed by their name.</returns>
        private static IDictionary<string, CandidateWord> GetCandidateWordsByName(IEnumerable<string> words)
        {
            _ = words ?? throw new ArgumentNullException(nameof(words));

            var candidateWords = words.Select(word => new CandidateWord(word)).Distinct().ToArray();

            return candidateWords.ToDictionary(candidate => candidate.CaseInsensitiveWord, candidate => candidate);
        }
    }
}