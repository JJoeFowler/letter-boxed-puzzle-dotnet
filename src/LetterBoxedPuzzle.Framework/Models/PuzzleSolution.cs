// ===============================================================================================================================================
// <copyright file="PuzzleSolution.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Class for solving the letter boxed puzzle.
    /// </summary>
    public class PuzzleSolution
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PuzzleSolution" /> class.
        /// </summary>
        /// <param name="solutionWords">The words forming a solution.</param>
        /// <param name="sideLetters">The side letters of the puzzle.</param>
        /// <exception cref="ArgumentNullException">Thrown when given a null value either parameter.</exception>
        public PuzzleSolution(IEnumerable<CandidateWord> solutionWords, SideLetters sideLetters)
        {
            (this.Words, this.SideLetters, this.Length) = (wordArchive: solutionWords, sideLetters) switch
                {
                    (null, _) => throw new ArgumentNullException(nameof(solutionWords)),

                    (_, null) => throw new ArgumentNullException(nameof(sideLetters)),

                    (_, _) => (solutionWords.ToArray(), sideLetters, solutionWords.Select(word => word.LowercaseWord.Length).Sum()),
                };
        }

        /// <summary>
        ///     Gets the word archive containing the allowed words to solve the puzzle.
        /// </summary>
        public CandidateWord[] Words { get; }

        /// <summary>
        ///     Gets the side letters of the letter-boxed puzzles.
        /// </summary>
        public SideLetters SideLetters { get; }

        /// <summary>
        ///     Gets the total number of letters of the words used to solve the puzzle.
        /// </summary>
        public int Length { get; }
    }
}