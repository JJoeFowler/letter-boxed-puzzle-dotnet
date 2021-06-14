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
    using System.Runtime.ExceptionServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Xml.Schema;

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

        /// <summary>
        ///     Checks whether the word archive initialized by all legal words contains the test word.
        /// </summary>
        [TestMethod]
        public void WordArchive_GivenLegalWordsText_ContainsTestWord()
        {
            var wordArchive = new WordArchive(WordConstants.AllLegalWordsText);

            // const string letterboxLetters = "aygocbriftln";
            const string letterboxLetters = "hapoilsewnzr";
            var letterBoxCandidateWord = new CandidateWord(letterboxLetters);

            var candidateWordsByName = wordArchive.CandidateWordsByName;

            var someWords = wordArchive.AllowedWords.Where(word => candidateWordsByName[word].IsContainedIn(letterBoxCandidateWord)).ToArray();

            var nonDuplicateWords = someWords.Where(word => !candidateWordsByName[word].HasDoubleLetters).ToArray();

            var pairs = new HashSet<string>(new[]
                { 
                    "ay", "ag", "yg",
                    "oc", "ob", "cb",
                    "ri", "rf", "if",
                    "tl", "tn", "ln",
                });

            var goodWords = nonDuplicateWords.Where(word => word.Length >= 3 && !HasWordPair(word, pairs)).ToArray();

            var goodWordsByLetter = new Dictionary<char, string[]>();

            var startsWithWordsByLetter = new Dictionary<char, string[]>();
            var endsWithWordsByLetter = new Dictionary<char, string[]>();

            foreach (var someLetter in letterboxLetters)
            {
                var someLetterBitMask = AlphabetUtilities.GetAlphabetBitMask(someLetter);
                goodWordsByLetter[someLetter] =
                    goodWords.Where(word => ((int)(candidateWordsByName[word].AlphabetBitMask & someLetterBitMask) > 0)).ToArray();
                startsWithWordsByLetter[someLetter] = goodWords.Where(word => word[0] == someLetter).ToArray();
                endsWithWordsByLetter[someLetter] = goodWords.Where(word => word[^1] == someLetter).ToArray();

                Console.WriteLine($"{someLetter} -> {goodWordsByLetter[someLetter].Length} total words, {startsWithWordsByLetter[someLetter].Length} starts with, {endsWithWordsByLetter[someLetter].Length} ends with");
            }

            var sortedLetters = letterboxLetters.ToCharArray().OrderBy(c => goodWordsByLetter[c].Length);

            var smallestLetter = sortedLetters.First();
            var smallestGroup = goodWordsByLetter[smallestLetter];

            Console.WriteLine($"Letter {smallestLetter.ToString()} has the smallest group with {smallestGroup.Length} words.");

            var minLength = goodWords.Min(word => word.Length);
            var maxLength = goodWords.Max(word => word.Length);

            var wordGroups = new string[maxLength][];

            for (var length = minLength; length < maxLength; length++)
            {
                var wordGroup = goodWords.Where(x => x.Length == length).ToArray();
                wordGroups[length - 1] = wordGroup;
                Console.WriteLine($"{length} {wordGroup.Length}");
            }

            for (var firstLength = minLength; firstLength < maxLength; firstLength++)
            {
                var firstWordsLength = wordGroups[firstLength - 1].Length;

                Console.WriteLine($"Length {firstLength} has {firstWordsLength} words");
            }

            var solutions = new List<(string first, string second)>();

            foreach (var word in smallestGroup)
            {
                var firstWords = endsWithWordsByLetter[word[0]];
                var secondWords = startsWithWordsByLetter[word[^1]];

                // Console.WriteLine($"Checking {secondWords.Length} words for the first word '{word}':");
                foreach (var secondWord in secondWords)
                {
                    var bothWords = word + secondWord;

                    if (new CandidateWord(bothWords).AlphabetBitMask == letterBoxCandidateWord.AlphabetBitMask)
                    {
                        // Console.WriteLine($"{word}-{secondWord}");
                        solutions.Add((word, secondWord));
                    }
                }

                // Console.WriteLine($"Checking {firstWords.Length} words for the second word '{word}':");
                foreach (var firstWord in firstWords)
                {
                    var bothWords = firstWord + word;

                    if (new CandidateWord(bothWords).AlphabetBitMask == letterBoxCandidateWord.AlphabetBitMask)
                    {
                        // Console.WriteLine($"{firstWord}-{word}");
                        solutions.Add((firstWord, word));
                    }
                }
            }

            var sortedSolutions = solutions.OrderBy(x => x.first.Length + x.second.Length).ThenBy(x => x.first).ThenByDescending(x => x.second).ToArray();

            Console.WriteLine($"{someWords.Length} {nonDuplicateWords.Length} {goodWords.Length} {solutions.Count}:");

            Console.WriteLine(string.Join(", ", sortedSolutions.Select(s => $"{s.first}-{s.second}")));

            Assert.IsTrue(wordArchive.CandidateWordsByName.ContainsKey(TestLegalWord));
        }
    }
}