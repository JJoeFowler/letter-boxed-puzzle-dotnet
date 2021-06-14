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
            var letterBoxedLetterGroups = new[] { "ayg", "ocb", "rif", "tln" };

            // var letterBoxedLetterGroups = new[] { "tyb", "oaz", "ngr", "hle" };

            // const string letterBoxedLetters = "hapoilsewnzr";
            // const string letterBoxedLetters = "aygocbriftln";
            // const string letterBoxedLetters = "tyboazngrhle";
            var letterBoxedLetters = string.Join(string.Empty, letterBoxedLetterGroups);

            var letterBoxCandidateWord = new CandidateWord(letterBoxedLetters);

            var candidateWordsByName = wordArchive.CandidateWordsByName;

            var someWords = wordArchive.AllowedWords.Where(word => candidateWordsByName[word].IsContainedIn(letterBoxCandidateWord)).ToArray();

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

            /*
            var pairs = new HashSet<string>(new[]
                { 
                    // "ay", "ag", "yg", "oc", "ob", "cb", "ri", "rf", "if", "tl", "tn", "ln",
                    "ty", "tb", "yb", "oa", "oz", "az", "ng", "nr", "gr", "hl", "he", "le",
                    // "zebra-anthology"
                });
            */
            const int LetterBoxedMinimumWordLength = 3;

            var goodWords = someWords.Where(word => word.Length >= LetterBoxedMinimumWordLength && !HasWordPair(word, letterBoxedPairs)).ToArray();

            // var goodWords = nonDuplicateWords.Where(word => word.Length >= LetterBoxedMinimumWordLength && !HasWordPair(word, letterBoxedPairs))
            // .ToArray();
            Console.WriteLine($"There are a total of {goodWords.Length} for the letter boxed string '{letterBoxedLetters}'");

            var goodWordsByLetter = new Dictionary<char, string[]>();

            var startsWithWordsByLetter = new Dictionary<char, string[]>();
            var endsWithWordsByLetter = new Dictionary<char, string[]>();

            foreach (var someLetter in letterBoxedLetters)
            {
                var someLetterBitMask = AlphabetUtilities.GetAlphabetBitMask(someLetter);
                goodWordsByLetter[someLetter] = goodWords.Where(word => (int)(candidateWordsByName[word].AlphabetBitMask & someLetterBitMask) > 0)
                    .ToArray();
                startsWithWordsByLetter[someLetter] = goodWords.Where(word => word[0] == someLetter).ToArray();
                endsWithWordsByLetter[someLetter] = goodWords.Where(word => word[^1] == someLetter).ToArray();
            }

            var sortedLetters = letterBoxedLetters.ToCharArray().OrderBy(c => goodWordsByLetter[c].Length);

            foreach (var someLetter in sortedLetters)
            {
                Console.Write($"{someLetter} -> {goodWordsByLetter[someLetter].Length} total words with '{someLetter}' where ");
                Console.Write($"{startsWithWordsByLetter[someLetter].Length} words starts with '{someLetter}' and ");
                Console.WriteLine($"{endsWithWordsByLetter[someLetter].Length} words ends with '{someLetter}'");
            }

            var smallestLetter = sortedLetters.First();
            var smallestGroup = goodWordsByLetter[smallestLetter];

            var minLength = goodWords.Min(word => word.Length);
            var maxLength = goodWords.Max(word => word.Length);

            var wordGroups = new string[maxLength][];

            for (var length = minLength; length < maxLength; length++)
            {
                var wordGroup = goodWords.Where(x => x.Length == length).ToArray();
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

            foreach (var word in smallestGroup)
            {
                var firstWords = endsWithWordsByLetter[word[0]];
                var secondWords = startsWithWordsByLetter[word[^1]];

                // Console.WriteLine($"Checking {secondWords.Length} words for the first word '{word}':");
                foreach (var secondWord in secondWords)
                {
                    var bothWords = word + secondWord;

                    if (HasAllLetters(bothWords))
                    {
                        // Console.WriteLine($"{word}-{secondWord}");
                        twoWordSolutions.Add((word, secondWord));
                    }
                }

                // Console.WriteLine($"Checking {firstWords.Length} words for the second word '{word}':");
                foreach (var firstWord in firstWords)
                {
                    var bothWords = firstWord + word;

                    if (HasAllLetters(bothWords))
                    {
                        // Console.WriteLine($"{firstWord}-{word}");
                        twoWordSolutions.Add((firstWord, word));
                    }
                }
            }

            var sortedOneWordSolutions = oneWordSolutions.OrderBy(x => x.Length);
            var sortedTwoWordSolutions = twoWordSolutions.OrderBy(x => x.first.Length + x.second.Length).ThenBy(x => x.first)
                .ThenByDescending(x => x.second).ToArray();

            Console.WriteLine(
                // $"{someWords.Length} {nonDuplicateWords.Length} {goodWords.Length} {oneWordSolutions.Count} {twoWordSolutions.Count}:");
                $"{someWords.Length} {goodWords.Length} {oneWordSolutions.Count} {twoWordSolutions.Count}:");

            Console.WriteLine("One word: " + string.Join(", ", sortedOneWordSolutions));
            Console.WriteLine("Two words: " + string.Join(", ", sortedTwoWordSolutions.Select(s => $"{s.first}-{s.second}")));

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