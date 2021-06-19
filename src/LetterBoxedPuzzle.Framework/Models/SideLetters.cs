// ===============================================================================================================================================
// <copyright file="SideLetters.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Utilities.AlphabetUtilities;
    using static Utilities.StringUtilities;

    /// <summary>
    ///     Class for the groups of letters along the sides of a letter-boxed puzzle.
    /// </summary>
    public class SideLetters
    {
        /// <summary>
        ///     The minimum number of sides for letter-boxed puzzle, which is two here since only two sides are required to construct words.
        /// </summary>
        public const int MinimumLetterBoxedSides = 2;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SideLetters" /> class.
        /// </summary>
        /// <param name="letterGroups">The groups of letters along the sides of a letter-boxed puzzle.</param>
        /// <exception cref="ArgumentException">
        ///     Thrown when any letter group is null, empty, or contains a non-alphabet letter or when there are fewer than two groups.
        /// </exception>
        public SideLetters(params string[] letterGroups)
        {
            var sideCount = letterGroups.Length;

            if (letterGroups.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException(
                    $"One or more of the given groups of letters along the sides {QuoteJoin(letterGroups)}' is null or empty.");
            }

            if (sideCount < MinimumLetterBoxedSides)
            {
                throw new ArgumentException($"Must provide at least {MinimumLetterBoxedSides} groups of letters rather than {sideCount}.");
            }

            if (letterGroups.All(letters => letters.All(IsAlphabetLetter)) != true)
            {
                throw new ArgumentException(
                    $"Given groups of letters along the sides {QuoteJoin(letterGroups)}' cannot contain non-alphabet characters.");
            }

            this.LetterGroups = letterGroups.Select(group => group.ToLowerInvariant()).ToArray();
        }

        /// <summary>
        ///     Gets group of letters along the sides of a letter-boxed puzzle.
        /// </summary>
        public string[] LetterGroups { get; }

        /// <summary>
        ///     Gets the forbidden two-letter pairs that cannot be contained in a solution of a letter-boxed puzzle.
        /// </summary>
        /// <returns>The forbidden two-letter pairs of letters.</returns>
        private IEnumerable<string> ForbiddenTwoLetterPairs =>
            this.LetterGroups.Aggregate(
                new List<string>(),
                (current, group) =>
                {
                    current.AddRange(GenerateAllDistinctTwoLetterPairs(group));
                    return current;
                }).Distinct();

        /// <summary>
        ///     Gets the set of forbidden two-letter pairs.
        /// </summary>
        private ISet<string> ForbiddenTwoLetterPairSet =>
            this.ForbiddenTwoLetterPairs.Aggregate(
                new HashSet<string>(),
                (current, pair) =>
                {
                    current.Add(pair);
                    return current;
                });

        /// <summary>
        ///     Determines whether the given pair of two letters is forbidden and cannot be contained in any solution to the letter-boxed puzzle.
        /// </summary>
        /// <param name="twoLetterPair">The two-letter pair.</param>
        /// <returns><see langword="true" /> if the given two-letter is forbidden, of <see langword="false" /> otherwise.</returns>
        public bool IsForbiddenTwoLetterPair(string twoLetterPair)
        {
            _ = twoLetterPair ?? throw new ArgumentNullException(nameof(twoLetterPair));

            if (twoLetterPair.Length != 2)
            {
                throw new ArgumentException($"Given letters '{twoLetterPair}' do not have length 2.");
            }

            if (!twoLetterPair.All(IsAlphabetLetter))
            {
                throw new ArgumentException($"Given two letters '{twoLetterPair}' are not both letters of the alphabet.");
            }

            return this.ForbiddenTwoLetterPairSet.Contains(twoLetterPair.ToLowerInvariant());
        }
    }
}