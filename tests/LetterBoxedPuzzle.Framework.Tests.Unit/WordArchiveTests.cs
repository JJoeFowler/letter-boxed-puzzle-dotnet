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
    using LetterBoxedPuzzle.Framework.Utilities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [TestMethod]
        public void WordArchive_GivenLegalWordsText_ContainsTestWord()
        {
            var wordArchive = new WordArchive(WordConstants.AllLegalWordsText);

            // var letterBoxedLetterGroups = new[] { "ewb", "vcm", "lrn", "iao" };
            // var letterBoxedLetterGroups = new[] { "hap", "oil", "sew", "nzr" };
            // var letterBoxedLetterGroups = new[] { "ayg", "ocb", "rif", "tln" };
            // var letterBoxedLetterGroups = new[] { "aik", "buo", "fmt", "rcj" };
            var letterBoxedLetterGroups = new[] { "nby", "aoh", "itu", "slm" };

            // var letterBoxedLetterGroups = new[] { "tyb", "oaz", "ngr", "hle" };

            // const string letterBoxedLetters = "hapoilsewnzr";
            // const string letterBoxedLetters = "aygocbriftln";
            // const string letterBoxedLetters = "tyboazngrhle";

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

            var letterBoxCandidateWord = new CandidateWord(letterBoxedLetters);

            var candidateWordsByName = wordArchive.CandidateWordsByName;

            var letterBoxedWords = wordArchive.AllowedWords.Where(word => candidateWordsByName[word].IsContainedIn(letterBoxCandidateWord)).ToArray();

            OutputDuplicateWords(letterBoxedWords, nameof(letterBoxedWords));

            // var nonDuplicateWords = someWords.Where(word => !candidateWordsByName[word].HasDoubleLetters).ToArray();
            var letterBoxedPairs = new HashSet<string>();

            static void AllStringPairs(string text, ISet<string> pairs)
            {
                foreach (var firstText in text)
                {
                    foreach (var secondText in text)
                    {
                        pairs.Add(firstText.ToString() + secondText.ToString());
                    }
                }
            }

            foreach (var letterBoxedLetterGroup in letterBoxedLetterGroups)
            {
                AllStringPairs(letterBoxedLetterGroup, letterBoxedPairs);
            }

            var sortedLetterBoxedPairs = letterBoxedPairs.OrderBy(x => x).ToArray();

            const int LetterBoxedMinimumWordLength = 3;

            var legalWords = letterBoxedWords.Where(word => word.Length >= LetterBoxedMinimumWordLength && !HasWordPair(word, letterBoxedPairs)).ToArray();

            OutputDuplicateWords(legalWords, nameof(legalWords));

            Console.WriteLine($"There are a total of {legalWords.Length} for the letter boxed string '{letterBoxedLetters}'");

            var legalWordsByLetter = new Dictionary<char, string[]>();

            var startsWithWordsByLetter = new Dictionary<char, string[]>();
            var endsWithWordsByLetter = new Dictionary<char, string[]>();

            foreach (var letter in letterBoxedLetters)
            {
                var letterBitMask = AlphabetUtilities.GetAlphabetBitMask(letter);
                var legalWordsForLetter = legalWords.Where(word => (int)(candidateWordsByName[word].AlphabetBitMask & letterBitMask) > 0).ToArray();
                var wordsStartWithLetter = legalWords.Where(word => word[0] == letter).ToArray();
                var wordsEndWithLetter = legalWords.Where(word => word[^1] == letter).ToArray();

                OutputDuplicateWords(legalWordsForLetter, $"{nameof(legalWordsForLetter)} '{letter}'");
                OutputDuplicateWords(wordsStartWithLetter, $"{nameof(wordsStartWithLetter)} '{letter}'");
                OutputDuplicateWords(wordsEndWithLetter, $"{nameof(wordsEndWithLetter)} '{letter}'");

                legalWordsByLetter[letter] = legalWordsForLetter;
                startsWithWordsByLetter[letter] = wordsStartWithLetter;
                endsWithWordsByLetter[letter] = wordsEndWithLetter;
            }

            var sortedLetters = letterBoxedLetters.ToCharArray().OrderBy(c => legalWordsByLetter[c].Length);

            foreach (var letter in sortedLetters)
            {
                Console.Write($"{letter} -> {legalWordsByLetter[letter].Length} total words with '{letter}' where ");
                Console.Write($"{startsWithWordsByLetter[letter].Length} words starts with '{letter}' and ");
                Console.WriteLine($"{endsWithWordsByLetter[letter].Length} words ends with '{letter}'");
            }

            var minLength = legalWords.Min(word => word.Length);
            var maxLength = legalWords.Max(word => word.Length);

            var wordGroups = new string[maxLength][];

            for (var length = minLength; length < maxLength; length++)
            {
                var wordGroup = legalWords.Where(x => x.Length == length).ToArray();
                wordGroups[length - 1] = wordGroup;
                Console.WriteLine($"Length {length} has {wordGroup.Length} words");
            }

            bool HasAllLetters(string word) => new CandidateWord(word).AlphabetBitMask == letterBoxCandidateWord.AlphabetBitMask;

            var oneWordSolutions = new List<string>();

            for (var length = 12; length < maxLength; length++)
            {
                foreach (var word in wordGroups[length - 1].Where(HasAllLetters))
                {
                    oneWordSolutions.Add(word);
                }
            }

            var twoWordSolutions = new List<(string first, string second)>();

            foreach (var letter in sortedLetters)
            {
                var endsWithWords = endsWithWordsByLetter[letter];
                var startsWithWords = startsWithWordsByLetter[letter];

                OutputDuplicateWords(endsWithWords, $"{nameof(endsWithWords)} for letter '{letter}'");
                OutputDuplicateWords(startsWithWords, $"{nameof(startsWithWords)} for letter '{letter}'");

                foreach (var endsWithWord in endsWithWords)
                {
                    foreach (var startsWithWord in startsWithWords)
                    {
                        var bothWords = endsWithWord + startsWithWord;

                        if (HasAllLetters(bothWords))
                        {
                            twoWordSolutions.Add((endsWithWord, startsWithWord));
                        }
                    }
                }
            }

            var sortedOneWordSolutions = oneWordSolutions.OrderBy(x => x.Length);
            var sortedTwoWordSolutions = twoWordSolutions.OrderBy(x => x.first.Length + x.second.Length).ThenBy(x => x.first)
                .ThenBy(x => x.second).ToArray();

            Console.WriteLine($"There are {letterBoxedWords.Length} words using only letters '{letterBoxedLetters}'.");
            Console.WriteLine($"Of those there are {legalWords.Length} words not having any of the following pairs:");
            Console.WriteLine($"  '{string.Join("', '", sortedLetterBoxedPairs)}");
            Console.WriteLine($"There were {oneWordSolutions.Count} one-word and {twoWordSolutions.Count} two-word solutions");

            Console.WriteLine("One-word solutions: " + string.Join(", ", sortedOneWordSolutions));
            Console.WriteLine("Two-word solutions: ");
            var joinedTwoWordSolutions = sortedTwoWordSolutions.Select(s => $"{s.first}-{s.second}");
            var minSolutionLength = joinedTwoWordSolutions.Min(x => x.Length);
            var maxSolutionLength = joinedTwoWordSolutions.Max(x => x.Length);

            for (var length = minSolutionLength; length < maxSolutionLength; length++)
            {
                Console.Write($"Length {length - 1}: ");
                Console.WriteLine(string.Join(", ", joinedTwoWordSolutions.Where(x => x.Length == length)));
                Console.WriteLine();
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