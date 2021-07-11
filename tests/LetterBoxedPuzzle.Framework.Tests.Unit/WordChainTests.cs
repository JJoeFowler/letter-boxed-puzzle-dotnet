// ===============================================================================================================================================
// <copyright file="WordChainTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Utilities.StringUtilities;

    /// <summary>
    ///     Unit tests for word chain class methods.
    /// </summary>
    [TestClass]
    public class WordChainTests
    {
        /// <summary>
        ///     Verify whether given side letters with few solutions and a single linking letter that the two known solutions are returned.
        /// </summary>
        [TestMethod]
        public void FindSolutions_GiveSideLettersWithFewSolutionsWithSingleLinkingLetter_ReturnsTwoKnownSolutions()
        {
            // Arrange
            var sideLetters = new SideLetters("oai", "ucw", "tlq", "erd");
            var wordArchive = new WordArchive(WordConstants.EnglishWordsText);
            var wordChain = new WordChain(wordArchive.GetPuzzleCandidateWords(sideLetters), new[] { 't' }, sideLetters);

            var expectedSolutions = new[] { new[] { "wildcat", "torquate" }, new[] { "wildcat", "torque" } };
            var expectedSolutionCount = expectedSolutions.Length;

            // Act
            var actualSolutions = wordChain.FindSolutions()
                .Select(solution => solution.Words.Select(word => word.LowercaseWord).ToArray())
                .ToArray();
            var actualSolutionCount = actualSolutions.Length;

            foreach (var solution in actualSolutions)
            {
                Console.WriteLine(string.Join("-", solution));
            }

            // Assert
            Assert.AreEqual(
                expectedSolutionCount,
                actualSolutionCount,
                $"Expected {expectedSolutionCount} solutions instead of the {actualSolutionCount} found.");

            for (var solutionIndex = 0; solutionIndex < expectedSolutionCount; solutionIndex++)
            {
                for (var wordIndex = 0; wordIndex < expectedSolutionCount; wordIndex++)
                {
                    var expectedWord = expectedSolutions[solutionIndex][wordIndex];
                    var actualWord = actualSolutions[solutionIndex][wordIndex];

                    Assert.AreEqual(
                        expectedWord,
                        actualWord,
                        $"Expected '{expectedWord}' for word {wordIndex + 1} of solution {solutionIndex + 1} instead of the '{actualWord}'.");
                }
            }
        }

        /// <summary>
        ///     Verify whether given side letters with few solutions and a single linking letter that the two known solutions are returned.
        /// </summary>
        [TestMethod]
        public void FindPuzzleSolutions_GiveSideLettersWithFewSolutionsWithSingleLinkingLetter_ReturnsKnownSolutions()
        {
            // Arrange
            var wordArchive = new WordArchive(WordConstants.EnglishWordsText);

            var puzzleInputs = new[]
                {
                    new[] { "ifl", "swo", "gnm", "yae" }, new[] { "abc", "def", "ghi", "rst" }, new[] { "tyb", "oaz", "ngr", "hle" },
                    new[] { "ewb", "vcm", "lrn", "iao" }, new[] { "hap", "oil", "sew", "nzr" }, new[] { "ayg", "ocb", "rif", "tln" },
                    new[] { "aik", "buo", "fmt", "rcj" }, new[] { "nby", "aoh", "itu", "slm" }, new[] { "koz", "egb", "cnm", "iar" },
                    new[] { "clh", "drk", "vsn", "aei" }, new[] { "tqe", "gvw", "uhy", "oai" }, new[] { "srb", "ita", "feh", "lwu" },
                    new[] { "aih", "lpn", "fyt", "mco" }, new[] { "iye", "rom", "anp", "txl" }, new[] { "otr", "jcn", "eku", "sah" },
                    new[] { "hor", "egt", "inp", "cuy" },
                };

            var puzzleCount = puzzleInputs.Length;
            var sideLettersInputs = puzzleInputs.Select(letterGroups => new SideLetters(letterGroups)).ToArray();
            var puzzleSolvers = sideLettersInputs.Select(sideLetters => new PuzzleSolver(wordArchive, sideLetters, 2)).ToArray();

            // Act
            var puzzleSolutions = new PuzzleSolution[puzzleCount][];

            for (var puzzleIndex = 0; puzzleIndex < puzzleCount; puzzleIndex++)
            {
                var puzzleSolver = puzzleSolvers[puzzleIndex];
                var letterGroups = puzzleSolver.SideLetters.LetterGroups;
                var puzzleSolution = puzzleSolvers[puzzleIndex].FindPuzzleSolutions();
                var solutionCount = puzzleSolution.Length;
                puzzleSolutions[puzzleIndex] = puzzleSolution;

                Console.WriteLine($"Found {solutionCount} solutions for {QuoteJoin(letterGroups)}:");

                var lastLength = 0;
                foreach (var solutions in puzzleSolution)
                {
                    var length = solutions.Length;

                    if (lastLength != length)
                    {
                        Console.WriteLine($"\nLength {length}:");
                        lastLength = length;
                    }

                    var words = solutions.Words.Select(word => word.LowercaseWord);

                    Console.WriteLine(string.Join("-", words));
                }

                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}