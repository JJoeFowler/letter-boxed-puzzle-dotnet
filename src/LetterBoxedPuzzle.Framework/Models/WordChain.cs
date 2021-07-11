// ===============================================================================================================================================
// <copyright file="WordChain.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Enums;
    using LetterBoxedPuzzle.Framework.Extensions;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     Class for a word chain, consisting of the chain of candidate words where each subsequent word letter starts with the letter last
    ///     letter of the previous word in the chain.
    /// </summary>
    public class WordChain
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WordChain" /> class.
        /// </summary>
        /// <param name="candidateWords">The candidate words.</param>
        /// <param name="linkingLetters">The linking letters.</param>
        /// <param name="sideLetters">The side letters.</param>
        /// <exception cref="ArgumentNullException">Thrown when given a null value for any of the parameters.</exception>
        public WordChain(CandidateWord[] candidateWords, IEnumerable<char> linkingLetters, SideLetters sideLetters)
        {
            (this.CandidateWords, this.LinkingLetters, this.SideLetters) = (candidateWords, linkingLetters, sideLetters) switch
                {
                    (null, _, _) => throw new ArgumentNullException(nameof(candidateWords)),

                    (_, null, _) => throw new ArgumentNullException(nameof(linkingLetters)),

                    (_, _, null) => throw new ArgumentNullException(nameof(sideLetters)),

                    (_, _, _) when candidateWords.Length == 0 => throw new ArgumentException($"'{nameof(candidateWords)}' cannot be empty."),

                    (_, _, _) when linkingLetters.Count() >= PuzzleSolver.MaximumSolutionWordCount => throw new ArgumentException(
                        $"'{linkingLetters}' must be less than {PuzzleSolver.MaximumSolutionWordCount}."),

                    (_, _, _) when !linkingLetters.All(IsAlphabetLetter) => throw new ArgumentException(
                        $"'{nameof(linkingLetters)}' must only be letters of the alphabet"),

                    (_, _, _) => (candidateWords, linkingLetters.ToArray(), sideLetters),
                };

            this.WordLinks = GenerateWordLinks(candidateWords, linkingLetters);
        }

        /// <summary>
        ///     Gets the candidate words.
        /// </summary>
        public CandidateWord[] CandidateWords { get; }

        /// <summary>
        ///     Gets the linking letters, which are the last letters of the previous words in the chain that match the first letters of the
        ///     next words.
        /// </summary>
        public char[] LinkingLetters { get; }

        /// <summary>
        ///     Gets the side letters for the puzzle.
        /// </summary>
        public SideLetters SideLetters { get; }

        /// <summary>
        ///     Gets the words links for the word chain.
        /// </summary>
        public WordLink[] WordLinks { get; }

        /// <summary>
        ///     Find all the solutions, one word taken from each word link of the word chain (where first letter of each subsequent word is
        ///     the last letter of the previous word) whose letters collectively use all the side letters of the puzzle.
        /// </summary>
        /// <returns>
        ///     The solutions to the puzzle where each word comes from the respective word link of the word chain.
        /// </returns>
        public PuzzleSolution[] FindSolutions()
        {
            var sideLetterBitMask = new CandidateWord(this.SideLetters.SortedLetters).AlphabetBitMask;

            return (from candidateWordChain in this.GetCandidateWordChains()
                    where IsCompleteChain(sideLetterBitMask, candidateWordChain)
                    select candidateWordChain).Select(words => new PuzzleSolution(words, this.SideLetters))
                .ToArray();
        }

        /// <summary>
        ///     Generate the words links for the specified linking letters using the specified candidate words.
        /// </summary>
        /// <returns>The words for the specified candidate words based upon the specified linking letters.</returns>
        /// <param name="candidateWords">The candidate words.</param>
        /// <param name="linkingLetters">The linking letters.</param>
        private static WordLink[] GenerateWordLinks(CandidateWord[] candidateWords, IEnumerable<char> linkingLetters) =>
            linkingLetters.Count() switch
                {
                    0 => new WordLink[] { new (candidateWords, null, null) },
                    _ => GenerateLinkingLetterPairs(linkingLetters)
                        .Select(pair => new WordLink(candidateWords, pair.Current, pair.Next))
                        .ToArray(),
                };

        /// <summary>
        ///     Generates sequence of nullable character pairs from the linking letters, where the first value of the first pair is null
        ///     and the second value of last pair is also null.
        /// </summary>
        /// <param name="linkingLetters">The linking letters.</param>
        /// <returns>The sequential pairs of characters of the linking letters.</returns>
        private static IEnumerable<(char? Current, char? Next)> GenerateLinkingLetterPairs(IEnumerable<char> linkingLetters)
        {
            var nullCharacter = new char?[] { null };
            var nullableLinkingLetters = linkingLetters.Select(letter => (char?)letter);

            return nullCharacter.Concat(nullableLinkingLetters).Zip(nullableLinkingLetters.Concat(nullCharacter)).ToArray();
        }

        /// <summary>
        ///     Determines whether the given chain is a complete chain that uses all the letters for the specified alphabet bit mask.
        /// </summary>
        /// <param name="alphabetBitMask">The alphabet bit mask.</param>
        /// <param name="candidateWordChain">The candidate word chain.</param>
        /// <returns>
        ///     <see langword="true" /> if the word chain has all the letters to match the specified alphabet bit mask, or
        ///     <see langword="false" /> otherwise.
        /// </returns>
        private static bool IsCompleteChain(AlphabetBitMask alphabetBitMask, IEnumerable<CandidateWord> candidateWordChain) =>
            alphabetBitMask
            == candidateWordChain.Aggregate(AlphabetBitMask.None, (current, candidateWord) => current | candidateWord.AlphabetBitMask);

        /// <summary>
        ///     Get the chains of candidate words, where each candidate word is from one of the word links.
        /// </summary>
        /// <returns>The next word chain.</returns>
        private IEnumerable<IEnumerable<CandidateWord>> GetCandidateWordChains() =>
            this.WordLinks.Select(wordLink => wordLink.MatchingCandidateWords).CartesianProduct();
    }
}