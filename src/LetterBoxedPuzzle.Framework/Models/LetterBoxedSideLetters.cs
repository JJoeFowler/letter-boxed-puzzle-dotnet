// ===============================================================================================================================================
// <copyright file="LetterBoxedSideLetters.cs" company="Joe Fowler">
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

    /// <summary>
    ///     Class for the groups of letters along each side of a letter boxed puzzle.
    /// </summary>
    public class LetterBoxedSideLetters
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LetterBoxedSideLetters" /> class.
        /// </summary>
        /// <param name="sideLetterGroups">The groups of letters along each side of a letter-boxed puzzle.</param>
        public LetterBoxedSideLetters(params string[] sideLetterGroups)
        {
            _ = sideLetterGroups ?? throw new ArgumentNullException(nameof(sideLetterGroups));

            if (sideLetterGroups.All(letters => letters.All(IsAlphabetLetter)) != true)
            {
                throw new ArgumentException($"Given side letters '{sideLetterGroups}' cannot be empty or contain non-alphabet letters.");
            }

            this.SideLetterGroups = sideLetterGroups.Select(group => group.ToLowerInvariant()).ToArray();

            this.ForbiddenTwoLetterPairSet = this.ForbiddenTwoLetterPairs.Aggregate(
                new HashSet<string>(),
                (current, pair) =>
                {
                    current.Add(pair);
                    return current;
                });
        }

        /// <summary>
        ///     Gets the letters along each side of a letter-boxed puzzle.
        /// </summary>
        public string[] SideLetterGroups { get; }

        /// <summary>
        ///     Gets the forbidden two-letter pairs that cannot be contained in a solution of a letter-boxed puzzle.
        /// </summary>
        /// <returns>The forbidden two-letter pairs of letters.</returns>
        public IEnumerable<string> ForbiddenTwoLetterPairs
        {
            get
            {
                var pairs = new List<string>();

                foreach (var sideLetterGroup in this.SideLetterGroups)
                {
                    pairs.AddRange(GenerateAllDistinctTwoLetterPairs(sideLetterGroup));
                }

                return pairs;
            }
        }

        /// <summary>
        ///     Gets the set of forbidden two-letter pairs.
        /// </summary>
        public IReadOnlyCollection<string> ForbiddenTwoLetterPairSet { get; }

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

            if (twoLetterPair.All(IsAlphabetLetter))
            {
                throw new ArgumentException($"Given two letters '{twoLetterPair}' are not both letters of the alphabet.");
            }

            return this.ForbiddenTwoLetterPairSet.Contains(twoLetterPair);
        }
    }
}