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
    using System.Text.RegularExpressions;

    using LetterBoxedPuzzle.Framework.Constants;
    using LetterBoxedPuzzle.Framework.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static Extensions.StringExtensions;

    using static TestCommonConstants;

    using static Utilities.AlphabetUtilities;
    using static Utilities.StringUtilities;

    /// <summary>
    ///     The unit tests for the word archive.
    /// </summary>
    [TestClass]
    public class WordArchiveTests
    {
        /// <summary>
        ///     Test words consisting of obscure English words.
        /// </summary>
        private static readonly string[] TestEnglishWords =
            {
                "aardwolves", "bouillabaisses", "crepehangings", "devilishnesses", "epigrammatizing", "forciblenesses", "geopoliticians",
                "hygroscopically", "infantilization", "jellifications", "kibbitzers", "letterboxings", "mythologization", "northwesterlies",
                "osteoporoses", "paradoxicalness", "quidditches", "revalorisations", "serpentinizing", "timberdoodles", "underemphasised",
                "vaticinators", "whatchamacallit", "xylographical", "yoctoseconds", "zymosimeter",
            };

        /// <summary>
        ///     Test country names that are not English words.
        /// </summary>
        private static readonly string[] TestCountryNames =
            {
                "Afghanistan", "Belgium", "Cuba", "Denmark", "Egypt", "France", "Germany", "Hungary", "Iran", "Jamaica", "Kenya", "Libya",
                "Mexico", "Norway", "Poland", "Qatar", "Romania", "Sudan", "Thailand", "Ukraine", "Vietnam", "Yemen", "Zimbabwe",
            };

        /// <summary>
        ///     Test letter groups.
        /// </summary>
        private static readonly string[] TestLetterGroups = { "abc", "def", "ghi" };

        /// <summary>
        ///     Test side letters.
        /// </summary>
        private static readonly SideLetters TestSideLetters = new (TestLetterGroups);

        /// <summary>
        ///     Test words that should be allowed for the test side letters.
        /// </summary>
        private static readonly string[] TestAllowedWords = { "ahead", "bid", "chad", "did", "eh", "gag", "head", "ice" };

        /// <summary>
        ///     Test words that should not be allowed for the test side letters.
        /// </summary>
        private static readonly string[] TestNotAllowedWords = { "acid", "bad", "cage", "dig", "each", "fade", "gig", "high", "iced" };

        /// <summary>
        ///     Gets the test archive of all English words.
        /// </summary>
        internal static WordArchive TestArchive { get; } = new (WordConstants.EnglishWordsText);

        /// <summary>
        ///     Gets the words of the test archive.
        /// </summary>
        internal static string[] TestArchiveWords { get; } = TestArchive.AllWords;

        /// <summary>
        ///     Gets the set of words of the test archive.
        /// </summary>
        internal static ISet<string> TestArchiveWordsSet { get; private set; } = new HashSet<string>();

        /// <summary>
        ///     Gets the test candidate words keyed by their name.
        /// </summary>
        internal static IReadOnlyDictionary<string, CandidateWord> TestCandidateWordsByName { get; } = TestArchive.AllCandidateWordsByName;

        /// <summary>
        ///     Sets up the test class by initializing the test set of legal words.
        /// </summary>
        /// <param name="_">The test context.</param>
        [ClassInitialize]
        public static void Setup(TestContext _)
        {
            TestArchiveWordsSet = TestArchiveWords.Aggregate(
                TestArchiveWordsSet,
                (current, word) =>
                {
                    current.Add(word);
                    return current;
                });
        }

        /// <summary>
        ///     Verifies whether the legal words does not contain any words having white space.
        /// </summary>
        [TestMethod]
        public void LegalWords_ContainsAnyWordsWithWhiteSpace_IsFalse()
        {
            // Arrange
            var whiteSpaceRegex = new Regex(@"\s");

            // Act
            var containsWhiteSpace = TestArchiveWords.Select(word => whiteSpaceRegex.IsMatch(word)).ToArray();

            // Assert
            for (var wordIndex = 0; wordIndex < TestArchiveWords.Length; wordIndex++)
            {
                Assert.IsFalse(containsWhiteSpace[wordIndex], $"Allowed word {TestArchiveWords[wordIndex]} contains white space.");
            }
        }

        /// <summary>
        ///     Verifies whether the legal words contain all of the lowercase but not any of the capitalized or uppercase test English words.
        /// </summary>
        [TestMethod]
        public void AllWords_ContainsAllLowercaseButNotAnyCapitalizedOrUppercaseTestEnglishWord_IsTrue()
        {
            // Arrange
            var containsLowercaseEnglishWord = new bool[AlphabetSize];
            var containsCapitalizedEnglishWord = new bool[AlphabetSize];
            var containsUppercaseEnglishWord = new bool[AlphabetSize];

            var lowercaseTestEnglishWords = TestEnglishWords.Select(word => word.ToLowerInvariant()).ToArray();
            var capitalizedTestEnglishWords = TestEnglishWords.Select(word => word.ToFirstCharUpper()).ToArray();
            var uppercaseTestEnglishWords = TestEnglishWords.Select(word => word.ToUpperInvariant()).ToArray();

            // Act
            for (var letterIndex = 0; letterIndex < AlphabetSize; letterIndex++)
            {
                containsLowercaseEnglishWord[letterIndex] = TestArchiveWordsSet.Contains(lowercaseTestEnglishWords[letterIndex]);
                containsCapitalizedEnglishWord[letterIndex] = TestArchiveWordsSet.Contains(capitalizedTestEnglishWords[letterIndex]);
                containsUppercaseEnglishWord[letterIndex] = TestArchiveWordsSet.Contains(uppercaseTestEnglishWords[letterIndex]);
            }

            // Assert
            for (var letterIndex = 0; letterIndex < AlphabetSize; letterIndex++)
            {
                Assert.IsTrue(
                    containsLowercaseEnglishWord[letterIndex],
                    $"The legal words does not contain the lowercase English word '{lowercaseTestEnglishWords[letterIndex]}'.");

                Assert.IsFalse(
                    containsCapitalizedEnglishWord[letterIndex],
                    $"The legal words contains the capitalized English word '{capitalizedTestEnglishWords[letterIndex]}'.");

                Assert.IsFalse(
                    containsUppercaseEnglishWord[letterIndex],
                    $"The legal words contains the uppercase English word '{uppercaseTestEnglishWords[letterIndex]}'.");
            }
        }

        /// <summary>
        ///     Verifies whether the legal words does not contain any of the lowercase, capitalized, or uppercase test country names.
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
                containsLowercaseCountryName[countryNameIndex] = TestArchiveWordsSet.Contains(lowercaseTestCountryNames[countryNameIndex]);
                containsCapitalizedCountryName[countryNameIndex] = TestArchiveWordsSet.Contains(capitalizedTestCountryNames[countryNameIndex]);
                containsUppercaseCountryName[countryNameIndex] = TestArchiveWordsSet.Contains(uppercaseTestCountryNames[countryNameIndex]);
            }

            // Assert
            for (var countryNameIndex = 0; countryNameIndex < countryNamesLength; countryNameIndex++)
            {
                Assert.IsFalse(
                    containsLowercaseCountryName[countryNameIndex],
                    $"The legal words contains the lowercase country name '{lowercaseTestCountryNames[countryNameIndex]}'.");

                Assert.IsFalse(
                    containsCapitalizedCountryName[countryNameIndex],
                    $"The legal words contains the capitalized country name '{capitalizedTestCountryNames[countryNameIndex]}'.");

                Assert.IsFalse(
                    containsUppercaseCountryName[countryNameIndex],
                    $"The legal words contains the uppercase country name '{uppercaseTestCountryNames[countryNameIndex]}'.");
            }
        }

        /// <summary>
        ///     Verifies whether the test candidate words by name contains all of the test English words as keys.
        /// </summary>
        [TestMethod]
        public void AllCandidateWordsByName_ContainsAllTestEnglishWordsAsKeys_IsTrue()
        {
            // Arrange
            var testCandidateWordsDictionary = TestCandidateWordsByName;

            // Act
            var containsWordAsKey = TestEnglishWords.Select(word => testCandidateWordsDictionary.ContainsKey(word)).ToArray();

            // Assert
            for (var wordIndex = 0; wordIndex < TestEnglishWords.Length; wordIndex++)
            {
                var englishWord = TestEnglishWords[wordIndex];
                Assert.IsTrue(containsWordAsKey[wordIndex], $"Expected test candidate words to contain the word '{englishWord}' as a key.");
            }
        }

        /// <summary>
        ///     Verifies whether the test candidate words by name does not contains any of the test country names as keys.
        /// </summary>
        [TestMethod]
        public void AllCandidateWordsByName_ContainsAnyTestCountryNamesAsKey_IsFalse()
        {
            // Arrange
            var testCandidateWordsDictionary = TestCandidateWordsByName;

            // Act
            var containsWordAsKey = TestCountryNames.Select(word => testCandidateWordsDictionary.ContainsKey(word.ToLowerInvariant())).ToArray();

            // Assert
            for (var wordIndex = 0; wordIndex < TestCountryNames.Length; wordIndex++)
            {
                var countryName = TestCountryNames[wordIndex].ToLowerInvariant();
                Assert.IsFalse(
                    containsWordAsKey[wordIndex],
                    $"Expected test candidate words not to contain the lowercased country name '{countryName}' as a key.");
            }
        }

        /// <summary>
        ///     Verifies whether the values of test candidate words by name for the English words have alphabet bit masks that match the alphabet
        ///     bit masks of corresponding English word.
        /// </summary>
        [TestMethod]
        public void AllCandidateWordsByName_TestEnglishWordsValuesAlphabetBitMasks_MatchesEnglishWordAlphabetBitMasks()
        {
            // Arrange
            var testCandidateWords = TestEnglishWords.Select(word => TestCandidateWordsByName[word]).ToArray();
            var expectedAlphabetBitMask = TestEnglishWords.Select(word => new CandidateWord(word).AlphabetBitMask).ToArray();

            // Act
            var actualAlphabetBitMask = testCandidateWords.Select(word => word.AlphabetBitMask).ToArray();

            // Assert
            for (var wordIndex = 0; wordIndex < TestEnglishWords.Length; wordIndex++)
            {
                var englishWord = TestEnglishWords[wordIndex];
                var expectedBitMask = expectedAlphabetBitMask[wordIndex];
                var actualBitMask = actualAlphabetBitMask[wordIndex];

                Assert.IsNotNull(actualBitMask, $"The alphabet bit mask for the candidate word for '{englishWord}' was null.");

                var expectedBinaryBitMask = Convert.ToString((int)expectedBitMask, 2);
                var actualBinaryBitMask = Convert.ToString((int)actualBitMask, 2);

                Assert.AreEqual(
                    expectedBitMask,
                    actualBitMask,
                    $"The candidate word '{englishWord}' had an alphabet bit mask of '{actualBinaryBitMask}' and not '{expectedBinaryBitMask}'.");
            }
        }

        /// <summary>
        ///     Verifies whether given the test side letters that all the permitted puzzle words include the test words that should be allowed.
        /// </summary>
        [TestMethod]
        public void GetPermittedPuzzleWords_GivenTestSideLetters_ContainsAllTestAllowedWords()
        {
            // Arrange
            var testAllowedWordSet = TestAllowedWords.Aggregate(
                new HashSet<string>(),
                (current, word) =>
                {
                    current.Add(word);
                    return current;
                });

            // Act
            var permittedPuzzleWords = TestArchive.GetPermittedPuzzleWords(TestSideLetters);
            var actualAllowedWords = permittedPuzzleWords.Where(word => testAllowedWordSet.Contains(word)).ToArray();

            // Assert
            for (var wordIndex = 0; wordIndex < TestAllowedWords.Length; wordIndex++)
            {
                var allowedWord = TestAllowedWords[wordIndex];
                var sideLetterGroups = QuoteJoin(TestSideLetters.LetterGroups);
                Assert.IsTrue(
                    actualAllowedWords.Contains(allowedWord),
                    $"Expected '{allowedWord}' to be one of the words allowed for the side letters {sideLetterGroups}.");
            }
        }

        /// <summary>
        ///     Verifies whether given the test side letters that all the permitted puzzle words do not include any of the test words that
        ///     should be not be allowed.
        /// </summary>
        [TestMethod]
        public void GetPermittedPuzzleWords_GivenTestSideLetters_ContainsNoTestNotAllowedWords()
        {
            // Arrange and act
            var permittedPuzzleWords = TestArchive.GetPermittedPuzzleWords(TestSideLetters);
            var containsTestNotAllowedWord = TestNotAllowedWords.Select(word => permittedPuzzleWords.Contains(word)).ToArray();

            // Assert
            for (var wordIndex = 0; wordIndex < TestNotAllowedWords.Length; wordIndex++)
            {
                var testNotAllowedWord = TestNotAllowedWords[wordIndex];
                var sideLetterGroups = QuoteJoin(TestSideLetters.LetterGroups);
                Assert.IsFalse(
                    containsTestNotAllowedWord[wordIndex],
                    $"Expected '{testNotAllowedWord}' not to be one of the words allowed for the side letters {sideLetterGroups}.");
            }
        }

        /// <summary>
        ///     Verifies whether the word archive initialized by all English words contains the test word.
        /// </summary>
        [Ignore]
        public void WordArchive_GivenEnglishWordsText_ContainsTestWord()
        {
            var wordArchive = new WordArchive(WordConstants.EnglishWordsText);

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

            var candidateWordsByName = wordArchive.AllCandidateWordsByName;

            var sortedLetterBoxedPairs = sideLetters.ForbiddenTwoLetterPairs.OrderBy(x => x).ToArray();

            var englishWords = candidateWordsByName.Keys.Where(word => candidateWordsByName[word].IsAllowed(sideLetters)).ToArray();

            var englishWordsLength = englishWords.Length;

            var letterBoxedLetters = sideLetters.DistinctLetters;

            var letterBoxedWordsLength = candidateWordsByName.Keys.Select(word => candidateWordsByName[word].IsContainedIn(letterBoxedLetters))
                .ToArray().Length;

            Console.WriteLine($"There are {letterBoxedWordsLength} words using only letters '{letterBoxedLetters}'.");
            Console.WriteLine();

            Console.WriteLine($"Of those there are {englishWordsLength} words not having any of the following pairs:");
            Console.WriteLine($"  '{string.Join("', '", sortedLetterBoxedPairs)}'");
            Console.WriteLine();

            Console.WriteLine($"Computing one-word solutions based upon length of those {englishWordsLength} words:");

            const int minOneWordLength = 12;
            var minLength = englishWords.Min(word => word.Length);
            var maxLength = englishWords.Max(word => word.Length);

            var wordGroups = new string[maxLength][];

            var totalOneWordChecks = 0;
            for (var length = minLength; length < maxLength; length++)
            {
                var wordGroup = englishWords.Where(x => x.Length == length).ToArray();
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

            var englishWordsByLetter = new Dictionary<char, string[]>();
            var startsWithWordsByLetter = new Dictionary<char, string[]>();
            var endsWithWordsByLetter = new Dictionary<char, string[]>();

            foreach (var letter in letterBoxedLetters)
            {
                var letterBitMask = GetAlphabetBitMask(letter);
                var englishWordsForLetter = englishWords.Where(word => (int)(candidateWordsByName[word].AlphabetBitMask & letterBitMask) > 0)
                    .ToArray();

                var wordsStartWithLetter = englishWordsForLetter.Where(word => word[0] == letter).ToArray();
                var wordsEndWithLetter = englishWordsForLetter.Where(word => word[^1] == letter).ToArray();

                englishWordsByLetter[letter] = englishWordsForLetter;
                startsWithWordsByLetter[letter] = wordsStartWithLetter;
                endsWithWordsByLetter[letter] = wordsEndWithLetter;
            }

            var sortedLetters = letterBoxedLetters.ToCharArray().OrderBy(c => englishWordsByLetter[c].Length);
            var smallestLetter = sortedLetters.First();

            var englishWordsWithSmallestLetterByLetter = new Dictionary<char, string[]>();
            var startsWithWordsWithSmallestLetterByLetter = new Dictionary<char, string[]>();
            var endsWithWordsWithSmallestLetterByLetter = new Dictionary<char, string[]>();

            foreach (var letter in sortedLetters)
            {
                var letterBitMask = GetAlphabetBitMask(letter) | GetAlphabetBitMask(smallestLetter);
                var englishWordsForLetterWithSmallestLetter = englishWords
                    .Where(word => (int)(candidateWordsByName[word].AlphabetBitMask & letterBitMask) == (int)letterBitMask).ToArray();

                var wordsStartWithLetterWithSmallestLetter = englishWordsForLetterWithSmallestLetter.Where(word => word[0] == letter).ToArray();
                var wordsEndWithLetterWithSmallestLetter = englishWordsForLetterWithSmallestLetter.Where(word => word[^1] == letter).ToArray();

                englishWordsWithSmallestLetterByLetter[letter] = englishWordsForLetterWithSmallestLetter;
                startsWithWordsWithSmallestLetterByLetter[letter] = wordsStartWithLetterWithSmallestLetter;
                endsWithWordsWithSmallestLetterByLetter[letter] = wordsEndWithLetterWithSmallestLetter;
            }

            var totalTwoWordChecks = 0;
            foreach (var letter in sortedLetters)
            {
                Console.Write($"  {letter} -> {englishWordsByLetter[letter].Length} total words with '{letter}' where ");

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

            Assert.IsTrue(wordArchive.AllCandidateWordsByName.ContainsKey(TestEnglishWords[0]));
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