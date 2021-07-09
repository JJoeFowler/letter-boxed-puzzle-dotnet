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

    using static Extensions.StringExtensions;

    using static TestCommonConstants;

    using static Utilities.AlphabetUtilities;

    /// <summary>
    ///     The unit tests for the word archive.
    /// </summary>
    [TestClass]
    public class WordArchiveTests
    {
        /// <summary>
        ///     Test capitalized legal words.
        /// </summary>
        private static readonly string[] TestLegalWords =
            {
                "aardwolves", "bouillabaisses", "crepehangings", "devilishnesses", "epigrammatizing", "forciblenesses", "geopoliticians",
                "hygroscopically", "infantilization", "jellifications", "kibbitzers", "letterboxings", "mythologization", "northwesterlies",
                "osteoporoses", "paradoxicalness", "quidditches", "revalorisations", "serpentinizing", "timberdoodles", "underemphasised",
                "vaticinators", "whatchamacallit", "xylographical", "yoctoseconds", "zymosimeter",
            };

        /// <summary>
        ///     Test country names that are not legal words.
        /// </summary>
        private static readonly string[] TestCountryNames =
            {
                "Afghanistan", "Belgium", "Cuba", "Denmark", "Egypt", "France", "Germany", "Hungary", "Iran", "Jamaica", "Kenya", "Libya",
                "Mexico", "Norway", "Poland", "Qatar", "Romania", "Sudan", "Thailand", "Ukraine", "Vietnam", "Yemen", "Zimbabwe",
            };

        /// <summary>
        ///     Gets the test set of allowed words.
        /// </summary>
        internal static IReadOnlySet<string> TestAllowedWordsSet { get; private set; } = new HashSet<string>();

        /// <summary>
        ///     Sets up the test class by initializing the test set of allowed words.
        /// </summary>
        /// <param name="_">The test context.</param>
        [ClassInitialize]
        public static void Setup(TestContext _)
        {
            var allowedWords = new WordArchive(WordConstants.AllowedEnglishWordsText).AllowedWords;

            TestAllowedWordsSet = allowedWords.Aggregate(
                new HashSet<string>(),
                (current, word) =>
                {
                    current.Add(word);
                    return current;
                });
        }

        /// <summary>
        ///     Verifies whether.
        /// </summary>
        [TestMethod]
        public void Ack()
        {
            // Arrange

            // Act

            // Assert
        }

        /// <summary>
        ///     Verifies whether the allowed words contain all of the lowercase but not any of the capitalized or uppercase test legal words.
        /// </summary>
        [TestMethod]
        public void AllWords_ContainsAllLowercaseButNotAnyCapitalizedOrUppercaseTestLegalWord_IsTrue()
        {
            // Arrange
            var containsLowercaseLegalWord = new bool[AlphabetSize];
            var containsCapitalizedLegalWord = new bool[AlphabetSize];
            var containsUppercaseLegalWord = new bool[AlphabetSize];

            var lowercaseTestLegalWords = TestLegalWords.Select(word => word.ToLowerInvariant()).ToArray();
            var capitalizedTestLegalWords = TestLegalWords.Select(word => word.ToFirstCharUpper()).ToArray();
            var uppercaseTestLegalWords = TestLegalWords.Select(word => word.ToUpperInvariant()).ToArray();

            // Act
            for (var letterIndex = 0; letterIndex < AlphabetSize; letterIndex++)
            {
                containsLowercaseLegalWord[letterIndex] = TestAllowedWordsSet.Contains(lowercaseTestLegalWords[letterIndex]);
                containsCapitalizedLegalWord[letterIndex] = TestAllowedWordsSet.Contains(capitalizedTestLegalWords[letterIndex]);
                containsUppercaseLegalWord[letterIndex] = TestAllowedWordsSet.Contains(uppercaseTestLegalWords[letterIndex]);
            }

            // Assert
            for (var letterIndex = 0; letterIndex < AlphabetSize; letterIndex++)
            {
                Assert.IsTrue(
                    containsLowercaseLegalWord[letterIndex],
                    $"The allowed words does not contain the lowercase legal word '{lowercaseTestLegalWords[letterIndex]}'.");

                Assert.IsFalse(
                    containsCapitalizedLegalWord[letterIndex],
                    $"The allowed words contains the capitalized legal word '{capitalizedTestLegalWords[letterIndex]}'.");

                Assert.IsFalse(
                    containsUppercaseLegalWord[letterIndex],
                    $"The allowed words contains the uppercase legal word '{uppercaseTestLegalWords[letterIndex]}'.");
            }
        }

        /// <summary>
        ///     Verifies whether the allowed words does not contain any of the lowercase, capitalized, or uppercase test country names.
        /// </summary>
        [TestMethod]
        public void AllWords_ContainsAnyLowercaseOrCapitalizedOrUppercaseTestCountryName_IsFalse()
        {
            // Arrange
            var countryNamesLength = TestCountryNames.Length;
            var containsLowercaseCountryName = new bool[countryNamesLength];
            var containsCapitalizedCountryName = new bool[countryNamesLength];
            var containsUppercaseCountryName = new bool[countryNamesLength];

            var lowercaseTestCountryNames = TestCountryNames.Select(word => word.ToLowerInvariant()).ToArray();
            var capitalizedTestCountryNames = TestCountryNames.Select(word => word.ToFirstCharUpper()).ToArray();
            var uppercaseTestCountryNames = TestCountryNames.Select(word => word.ToUpperInvariant()).ToArray();

            // Act
            for (var countryNameIndex = 0; countryNameIndex < countryNamesLength; countryNameIndex++)
            {
                containsLowercaseCountryName[countryNameIndex] = TestAllowedWordsSet.Contains(lowercaseTestCountryNames[countryNameIndex]);
                containsCapitalizedCountryName[countryNameIndex] = TestAllowedWordsSet.Contains(capitalizedTestCountryNames[countryNameIndex]);
                containsUppercaseCountryName[countryNameIndex] = TestAllowedWordsSet.Contains(uppercaseTestCountryNames[countryNameIndex]);
            }

            // Assert
            for (var countryNameIndex = 0; countryNameIndex < countryNamesLength; countryNameIndex++)
            {
                Assert.IsFalse(
                    containsLowercaseCountryName[countryNameIndex],
                    $"The allowed words contains the lowercase country name '{lowercaseTestCountryNames[countryNameIndex]}'.");

                Assert.IsFalse(
                    containsCapitalizedCountryName[countryNameIndex],
                    $"The allowed words contains the capitalized country name '{capitalizedTestCountryNames[countryNameIndex]}'.");

                Assert.IsFalse(
                    containsUppercaseCountryName[countryNameIndex],
                    $"The allowed words contains the uppercase country name '{uppercaseTestCountryNames[countryNameIndex]}'.");
            }
        }

        /// <summary>
        ///     Verifies whether the word archive initialized by all legal words contains the test word.
        /// </summary>
        [Ignore]
        public void WordArchive_GivenLegalWordsText_ContainsTestWord()
        {
            var wordArchive = new WordArchive(WordConstants.AllowedEnglishWordsText);

            // var letterBoxedLetterGroups = new[] { "ifl", "swo", "gnm", "yae" };
            // var letterBoxedLetterGroups = new[] { "abc", "def", "ghi", "rst" };
            // var letterBoxedLetterGroups = new[] { "tyb", "oaz", "ngr", "hle" };
            // var letterBoxedLetterGroups = new[] { "ewb", "vcm", "lrn", "iao" };
            // var letterBoxedLetterGroups = new[] { "hap", "oil", "sew", "nzr" };
            // var letterBoxedLetterGroups = new[] { "ayg", "ocb", "rif", "tln" };
            // var letterBoxedLetterGroups = new[] { "aik", "buo", "fmt", "rcj" };
            // var letterBoxedLetterGroups = new[] { "nby", "aoh", "itu", "slm" };
            // var letterBoxedLetterGroups = new[] { "koz", "egb", "cnm", "iar" };
            // var sideLetters = new SideLetters("clh", "drk", "vsn", "aei");
            // var sideLetters = new SideLetters("tqe", "gvw", "uhy", "oai");

            // var sideLetters = new SideLetters("srb", "ita", "feh", "lwu");
            // var sideLetters = new SideLetters("aih", "lpn", "fyt", "mco");
            // var sideLetters = new SideLetters("iye", "rom", "anp", "txl");
            // var sideLetters = new SideLetters("otr", "jcn", "eku", "sah");
            // var sideLetters = new SideLetters("hor", "egt", "inp", "cuy");
            var sideLetters = new SideLetters("oai", "ucw", "tlq", "erd");

            var candidateWordsByName = wordArchive.CandidateWordsByName;

            var sortedLetterBoxedPairs = sideLetters.ForbiddenTwoLetterPairs.OrderBy(x => x).ToArray();

            var legalWords = candidateWordsByName.Keys.Where(word => candidateWordsByName[word].IsAllowed(sideLetters)).ToArray();

            var legalWordsLength = legalWords.Length;

            var letterBoxedLetters = sideLetters.DistinctLetters;

            var letterBoxedWordsLength = candidateWordsByName.Keys.Select(word => candidateWordsByName[word].IsContainedIn(letterBoxedLetters))
                .ToArray().Length;

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

            var letterBoxedLettersBitMask = new CandidateWord(letterBoxedLetters).AlphabetBitMask;
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

            Assert.IsTrue(wordArchive.CandidateWordsByName.ContainsKey(TestLegalWords[0]));
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