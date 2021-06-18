// ===============================================================================================================================================
// <copyright file="WordArchiveTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     The unit tests for the word archive.
    /// </summary>
    [TestClass]
    public class WordArchiveTests
    {
        /// <summary>
        ///     Test legal word.
        /// </summary>
        private const string TestLegalWord = "zymosimeter";

        /// <summary>
        ///     Checks whether the word archive initialized by all legal words contains the test word.
        /// </summary>
        [Ignore]
        public void WordArchive_GivenLegalWordsText_ContainsTestWord()
        {
            var wordArchive = new WordArchive(WordConstants.AllLegalWordsText);

            // var letterBoxedLetterGroups = new[] { "ifl", "swo", "gnm", "yae" };
            // var letterBoxedLetterGroups = new[] { "abc", "def", "ghi", "rst" };
            // var letterBoxedLetterGroups = new[] { "tyb", "oaz", "ngr", "hle" };
            // var letterBoxedLetterGroups = new[] { "ewb", "vcm", "lrn", "iao" };
            // var letterBoxedLetterGroups = new[] { "hap", "oil", "sew", "nzr" };
            // var letterBoxedLetterGroups = new[] { "ayg", "ocb", "rif", "tln" };
            // var letterBoxedLetterGroups = new[] { "aik", "buo", "fmt", "rcj" };
            var letterBoxedLetterGroups = new[] { "nby", "aoh", "itu", "slm" };

            void OutputDuplicateWords(IEnumerable<string> words, string description)
            {
                var seenWord = new HashSet<string>();
                var wordCounts = new Dictionary<string, int>();

                foreach (var word in words)
                {
                    if (!seenWord.Contains(word))
                    {
                        seenWord.Add(word);
                        continue;
                    }

                    if (!wordCounts.ContainsKey(word))
                    {
                        wordCounts[word] = 1;
                    }

                    wordCounts[word]++;
                }

                if (wordCounts.Keys.Count == 0)
                {
                    // Console.WriteLine($"Word list {description} has no duplicates.");
                    return;
                }

                Console.Write($"Word list {description} has the following duplicate words: ");

                string.Join(", ", wordCounts.OrderBy(kvp => kvp.Value).ThenBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key} {kvp.Value} times"));
            }

            var letterBoxedLetters = string.Join(string.Empty, letterBoxedLetterGroups);
            var letterBoxedLettersBitMask = GetAlphabetBitMask(letterBoxedLetters);

            var candidateWordsByName = wordArchive.CandidateWordsByName;

            var letterBoxedWords = wordArchive.AllowedWords.Where(word => candidateWordsByName[word].IsContainedIn(letterBoxedLetters)).ToArray();
            var letterBoxedWordsLength = letterBoxedWords.Length;

            var letterBoxedPairs = new HashSet<string>();

            static void AllStringPairs(string text, ISet<string> pairs)
            {
                foreach (var firstText in text)
                {
                    foreach (var secondText in text)
                    {
                        pairs.Add(firstText + secondText.ToString());
                    }
                }
            }

            foreach (var letterBoxedLetterGroup in letterBoxedLetterGroups)
            {
                AllStringPairs(letterBoxedLetterGroup, letterBoxedPairs);
            }

            var sortedLetterBoxedPairs = letterBoxedPairs.OrderBy(x => x);

            const int LetterBoxedMinimumWordLength = 3;
            var legalWords = letterBoxedWords.Where(word => word.Length >= LetterBoxedMinimumWordLength && !HasWordPair(word, letterBoxedPairs))
                .ToArray();
            var legalWordsLength = legalWords.Length;

            Console.WriteLine($"There are {letterBoxedWordsLength} words using only letters '{letterBoxedLetters}'.");
            Console.WriteLine();

            Console.WriteLine($"Of those there are {legalWordsLength} words not having any of the following pairs:");
            Console.WriteLine($"  '{string.Join("', '", sortedLetterBoxedPairs)}'");
            Console.WriteLine();

            Console.WriteLine($"Computing one-word solutions based upon length of those {legalWordsLength} words:");

            const int minOneWordLength = 12;
            var minLength = legalWords.Min(word => word.Length);
            var maxLength = legalWords.Max(word => word.Length);

            var wordGroups = new string[maxLength][];

            var totalOneWordChecks = 0;
            for (var length = minLength; length < maxLength; length++)
            {
                var wordGroup = legalWords.Where(x => x.Length == length).ToArray();
                var wordGroupLength = wordGroup.Length;
                wordGroups[length - 1] = wordGroup;
                Console.Write($"  Length {length} has {wordGroupLength} words");

                if (length >= minOneWordLength)
                {
                    totalOneWordChecks += wordGroupLength;
                    Console.Write(" that add to the total number of one-word solution checks");
                }

                Console.WriteLine(".");
            }

            Console.WriteLine();
            Console.WriteLine($"This gives a total of {totalOneWordChecks} checks for a one-word solution.");

            bool HasAllLetters(string word) => GetAlphabetBitMask(word) == letterBoxedLettersBitMask;

            var oneWordSolutions = new List<string>();

            for (var length = minOneWordLength; length < maxLength; length++)
            {
                var wordGroupWithAllLetters = wordGroups[length - 1].Where(HasAllLetters).ToArray();
                foreach (var word in wordGroupWithAllLetters)
                {
                    oneWordSolutions.Add(word);
                }
            }

            Console.WriteLine($"There were a total of {oneWordSolutions.Count} one-word solutions.");
            Console.WriteLine();

            var sortedOneWordSolutions = oneWordSolutions.OrderBy(x => x.Length);

            if (oneWordSolutions.Count > 0)
            {
                Console.WriteLine("One-word solutions: " + string.Join(", ", sortedOneWordSolutions));
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Computing two-word solutions:");

            var legalWordsByLetter = new Dictionary<char, string[]>();
            var startsWithWordsByLetter = new Dictionary<char, string[]>();
            var endsWithWordsByLetter = new Dictionary<char, string[]>();

            foreach (var letter in letterBoxedLetters)
            {
                var letterBitMask = GetAlphabetBitMask(letter);
                var legalWordsForLetter =
                    legalWords.Where(word => (int)(candidateWordsByName[word].AlphabetBitMask & letterBitMask) > 0).ToArray();

                var wordsStartWithLetter = legalWordsForLetter.Where(word => word[0] == letter).ToArray();
                var wordsEndWithLetter = legalWordsForLetter.Where(word => word[^1] == letter).ToArray();

                legalWordsByLetter[letter] = legalWordsForLetter;
                startsWithWordsByLetter[letter] = wordsStartWithLetter;
                endsWithWordsByLetter[letter] = wordsEndWithLetter;
            }

            var sortedLetters = letterBoxedLetters.ToCharArray().OrderBy(c => legalWordsByLetter[c].Length);
            var smallestLetter = sortedLetters.First();

            var legalWordsWithSmallestLetterByLetter = new Dictionary<char, string[]>();
            var startsWithWordsWithSmallestLetterByLetter = new Dictionary<char, string[]>();
            var endsWithWordsWithSmallestLetterByLetter = new Dictionary<char, string[]>();

            foreach (var letter in sortedLetters)
            {
                var letterBitMask = GetAlphabetBitMask(letter) | GetAlphabetBitMask(smallestLetter);
                var legalWordsForLetterWithSmallestLetter = legalWords
                    .Where(word => (int)(candidateWordsByName[word].AlphabetBitMask & letterBitMask) == (int)letterBitMask).ToArray();

                var wordsStartWithLetterWithSmallestLetter = legalWordsForLetterWithSmallestLetter.Where(word => word[0] == letter).ToArray();
                var wordsEndWithLetterWithSmallestLetter = legalWordsForLetterWithSmallestLetter.Where(word => word[^1] == letter).ToArray();

                legalWordsWithSmallestLetterByLetter[letter] = legalWordsForLetterWithSmallestLetter;
                startsWithWordsWithSmallestLetterByLetter[letter] = wordsStartWithLetterWithSmallestLetter;
                endsWithWordsWithSmallestLetterByLetter[letter] = wordsEndWithLetterWithSmallestLetter;
            }

            var totalTwoWordChecks = 0;
            foreach (var letter in sortedLetters)
            {
                Console.Write($"  {letter} -> {legalWordsByLetter[letter].Length} total words with '{letter}' where ");

                var endWithSmallestLetterLength = endsWithWordsWithSmallestLetterByLetter[letter].Length;
                var startLength = startsWithWordsByLetter[letter].Length;

                int startWithSmallestLetterLength;
                var endLength = startWithSmallestLetterLength = 0;

                if (letter != smallestLetter)
                {
                    endLength = endsWithWordsByLetter[letter].Length;
                    startWithSmallestLetterLength = startsWithWordsWithSmallestLetterByLetter[letter].Length;
                }

                var twoWordChecks = startLength * endWithSmallestLetterLength + startWithSmallestLetterLength * endLength;
                totalTwoWordChecks += twoWordChecks;

                Console.Write($"({startLength} words starting with '{letter}' and ");
                Console.Write($"{endWithSmallestLetterLength} words ending with '{letter}' having '{smallestLetter}') ");
                if (letter != smallestLetter)
                {
                    Console.WriteLine();
                    Console.Write(
                        $"       along with ({startWithSmallestLetterLength} words starting with '{letter}' having {smallestLetter} and ");
                    Console.Write($"{endLength} words ending with '{letter}') ");
                }

                Console.WriteLine();
                Console.WriteLine($"         adding {twoWordChecks} more checks.");
            }

            Console.WriteLine();
            Console.WriteLine($"This gives a total of {totalTwoWordChecks} checks to obtain all two-word solutions.");

            var twoWordSolutions = new HashSet<(string first, string second)>();

            foreach (var letter in sortedLetters)
            {
                var endsWithWords = endsWithWordsByLetter[letter];
                var startsWithWords = startsWithWordsByLetter[letter];
                var endsWithWordsWithSmallestLetter = endsWithWordsWithSmallestLetterByLetter[letter];
                var startsWithWordsWithSmallestLetter = startsWithWordsWithSmallestLetterByLetter[letter];

                foreach (var endsWithWord in endsWithWords)
                {
                    foreach (var startsWithWord in startsWithWordsWithSmallestLetter)
                    {
                        var bothWords = endsWithWord + startsWithWord;

                        if (HasAllLetters(bothWords))
                        {
                            twoWordSolutions.Add((endsWithWord, startsWithWord));
                        }
                    }
                }

                if (letter == smallestLetter)
                {
                    continue;
                }

                foreach (var endsWithWord in endsWithWordsWithSmallestLetter)
                {
                    foreach (var startsWithWord in startsWithWords)
                    {
                        var bothWords = endsWithWord + startsWithWord;

                        if (HasAllLetters(bothWords))
                        {
                            var twoWordSolution = (endsWithWord, startsWithWord);
                            if (!twoWordSolutions.Contains(twoWordSolution))
                            {
                                twoWordSolutions.Add(twoWordSolution);
                            }
                        }
                    }
                }
            }

            var sortedTwoWordSolutions = twoWordSolutions.OrderBy(x => x.first.Length + x.second.Length).ThenBy(x => x.first).ThenBy(x => x.second)
                .ToArray();

            Console.WriteLine();
            var sortedTwoWordSolutionsLength = sortedTwoWordSolutions.Length;
            Console.WriteLine($"There were a total of {sortedTwoWordSolutionsLength} two-word solutions.");
            Console.WriteLine();

            if (sortedTwoWordSolutionsLength > 0)
            {
                Console.WriteLine("Two-word solutions: ");

                var joinedTwoWordSolutions = sortedTwoWordSolutions.Select(s => $"{s.first}-{s.second}");
                var minSolutionLength = joinedTwoWordSolutions.Min(x => x.Length);
                var maxSolutionLength = joinedTwoWordSolutions.Max(x => x.Length);

                for (var length = minSolutionLength; length <= maxSolutionLength; length++)
                {
                    var lengthTwoWordSolutions = joinedTwoWordSolutions.Where(x => x.Length == length).ToArray();
                    Console.WriteLine($"  Length {length - 1} solutions with {lengthTwoWordSolutions.Length} words: ");
                    Console.WriteLine("    " + string.Join(", ", lengthTwoWordSolutions));
                    Console.WriteLine();
                }
            }

            Assert.IsTrue(wordArchive.CandidateWordsByName.ContainsKey(TestLegalWord));
        }

        private static bool HasWordPair(string word, HashSet<string> pairs)
        {
            var lastLetter = ' ';
            foreach (var letter in word)
            {
                var pair = $"{lastLetter}{letter}";

                if (pairs.Contains(pair))
                {
                    return true;
                }

                lastLetter = letter;
            }

            return false;
        }
    }
}