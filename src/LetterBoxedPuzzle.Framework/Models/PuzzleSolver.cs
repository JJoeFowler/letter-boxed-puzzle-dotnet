// ===============================================================================================================================================
// <copyright file="PuzzleSolver.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Extensions;

    /// <summary>
    ///     Class for solving the letter boxed puzzle.
    /// </summary>
    public class PuzzleSolver
    {
        /// <summary>
        ///     Maximum number of words used to solve a puzzle.
        /// </summary>
        public const int MaximumSolutionWordCount = 6;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PuzzleSolver" /> class.
        /// </summary>
        /// <param name="wordArchive">The word archive containing the allowed words to solve the puzzle.</param>
        /// <param name="sideLetters">The side letters of the puzzle.</param>
        /// <param name="solutionWordCount">The number of words used to solve the puzzle.</param>
        /// <exception cref="ArgumentNullException">Thrown when given a null value for the candidate words.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when given a solution word count less than 1 or more than <see cref="MaximumSolutionWordCount" />.
        /// </exception>
        public PuzzleSolver(WordArchive wordArchive, SideLetters sideLetters, int solutionWordCount)
        {
            (this.WordArchive, this.SideLetters, this.SolutionWordCount) = (wordArchive, sideLetters, solutionWordCount) switch
                {
                    (null, _, _) => throw new ArgumentNullException(nameof(wordArchive)),

                    (_, null, _) => throw new ArgumentNullException(nameof(sideLetters)),

                    (_, _, _) when solutionWordCount <= 0 => throw new ArgumentException($"'{nameof(solutionWordCount)}' must be at least 1."),

                    (_, _, _) when solutionWordCount > MaximumSolutionWordCount => throw new ArgumentException(
                        $"'{nameof(solutionWordCount)}' can be at most {MaximumSolutionWordCount}."),

                    (_, _, _) => (wordArchive, sideLetters, solutionWordCount),
                };

            this.CandidateWords = wordArchive.GetPuzzleCandidateWords(sideLetters);

            this.LinkingLetters = GenerateLinkingLetters(sideLetters, solutionWordCount);
        }

        /// <summary>
        ///     Gets the word archive containing the allowed words to solve the puzzle.
        /// </summary>
        public WordArchive WordArchive { get; }

        /// <summary>
        ///     Gets the side letters of the letter-boxed puzzles.
        /// </summary>
        public SideLetters SideLetters { get; }

        /// <summary>
        ///     Gets the number of words used to solve the puzzle.
        /// </summary>
        public int SolutionWordCount { get; }

        /// <summary>
        ///     Gets the permitted puzzle words allowed by the side letters of the puzzle.
        /// </summary>
        public CandidateWord[] CandidateWords { get; }

        /// <summary>
        ///     Gets all possible linking letters used to create candidate word chains.
        /// </summary>
        public IEnumerable<char[]> LinkingLetters { get; }

        /// <summary>
        ///     Find all the solutions <see cref="SolutionWordCount" /> words long, where first letter of each subsequent word is the
        ///     last letter of the previous word whose letters collectively use all the side letters of the puzzle.
        /// </summary>
        /// <returns>All the <see cref="SolutionWordCount" />-word solutions to the puzzle.</returns>
        public PuzzleSolution[] FindPuzzleSolutions()
        {
            var solutions =
                this.LinkingLetters.SelectMany(letters => new WordChain(this.CandidateWords, letters, this.SideLetters).FindSolutions());

            return SortPuzzleSolutions(solutions, this.SolutionWordCount);
        }

        /// <summary>
        ///     Generates all possible linking letters use to create word chains for the specified side letters of the puzzle and the number of words used in
        ///     each solution.
        /// </summary>
        /// <param name="sideLetters">The side letters.</param>
        /// <param name="solutionWordCount">The number of words used for a solution.</param>
        /// <returns>All possible linking letters for the specified side letters and number of words to be used in each solution.</returns>
        private static IEnumerable<char[]> GenerateLinkingLetters(SideLetters sideLetters, int solutionWordCount)
        {
            return Enumerable.Repeat(sideLetters.SortedLetters.ToCharArray(), solutionWordCount - 1)
                .CartesianProduct()
                .Select(group => group.ToArray());
        }

        /// <summary>
        ///     Sorts the specified puzzle solutions first by the total length of all the words, then by the first word, then by the next word, and so on,
        ///     for each of the <see cref="SolutionWordCount" /> words of the solution.
        /// </summary>
        /// <param name="solutions">The solutions.</param>
        /// <param name="solutionWordCount">The number of words of each solution.</param>
        /// <returns>The solutions sorted first by total length and then by the words in the order they occur in the solution.</returns>
        private static PuzzleSolution[] SortPuzzleSolutions(IEnumerable<PuzzleSolution> solutions, int solutionWordCount) =>
            Enumerable.Range(0, solutionWordCount)
                .Aggregate(
                    solutions.OrderBy(solution => solution.Length),
                    (current, index) => current.ThenBy(solution => solution.Words[index].LowercaseWord))
                .ToArray();
    }
}