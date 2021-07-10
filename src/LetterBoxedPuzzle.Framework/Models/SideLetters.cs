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
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when the letter group is empty, contains a non-alphabet letter, or when there are fewer than two groups.
        /// </exception>
        public SideLetters(params string[] letterGroups)
        {
            this.LetterGroups = letterGroups switch
                {
                    null => throw new ArgumentNullException(nameof(letterGroups)),

                    _ when letterGroups.Any(string.IsNullOrEmpty) => throw new ArgumentException(
                        $"The values of '{nameof(letterGroups)}' cannot be null or empty."),

                    _ when letterGroups.All(letters => letters.All(IsAlphabetLetter)) != true => throw new ArgumentException(
                        $"The values of '{nameof(letterGroups)}' must be letters of the alphabet."),

                    _ when letterGroups.Length < MinimumLetterBoxedSides => throw new ArgumentException(
                        $"'{nameof(letterGroups)}' must have at {MinimumLetterBoxedSides} values."),

                    _ => letterGroups.Select(group => group.ToLowerInvariant()).ToArray(),
                };

            this.LetterGroups = letterGroups.Select(group => group.ToLowerInvariant()).ToArray();

            this.SortedLetters = new string(this.LetterGroups.SelectMany(x => x).Distinct().OrderBy(x => x).ToArray());

            this.ForbiddenTwoLetterPairs = GenerateForbiddenTwoLetterPairs(this.LetterGroups);

            this.ForbiddenTwoLetterPairSet = this.ForbiddenTwoLetterPairs.ToHashSet();
        }

        /// <summary>
        ///     Gets group of letters along the sides of a letter-boxed puzzle.
        /// </summary>
        public string[] LetterGroups { get; }

        /// <summary>
        ///     Gets the distinct alphabetically sorted letters among all the letter groups.
        /// </summary>
        public string SortedLetters { get; }

        /// <summary>
        ///     Gets the forbidden two-letter pairs that cannot be contained in a solution of a letter-boxed puzzle.
        /// </summary>
        /// <returns>The forbidden two-letter pairs of letters.</returns>
        public IOrderedEnumerable<string> ForbiddenTwoLetterPairs { get; }

        /// <summary>
        ///     Gets the set of forbidden two-letter pairs.
        /// </summary>
        private IReadOnlySet<string> ForbiddenTwoLetterPairSet { get; }

        /// <summary>
        ///     Determines whether the given pair of two letters is forbidden and cannot be contained in any solution to the letter-boxed puzzle.
        /// </summary>
        /// <param name="twoLetterPair">The two-letter pair.</param>
        /// <returns><see langword="true" /> if the given two-letter is forbidden, of <see langword="false" /> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value for the two-letter pair.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when given a string not of length two or when given a two-letter pair with a non-alphabet character.
        /// </exception>
        public bool IsForbiddenTwoLetterPair(string twoLetterPair) =>
            twoLetterPair switch
                {
                    null => throw new ArgumentNullException(nameof(twoLetterPair)),

                    _ when twoLetterPair.Length != 2 => throw new ArgumentException($"'{twoLetterPair}' does not have length 2."),

                    _ when !twoLetterPair.All(IsAlphabetLetter) => throw new ArgumentException(
                        $"'{twoLetterPair}' must be letters of the alphabet."),

                    _ => this.ForbiddenTwoLetterPairSet.Contains(twoLetterPair.ToLowerInvariant()),
                };

        /// <summary>
        ///     Generate the distinct forbidden two-letter pairs that cannot be contained in a solution of a letter-boxed puzzle.
        /// </summary>
        /// <param name="letterGroups">The letter groups.</param>
        /// <returns>The forbidden two-letter pairs.</returns>
        private static IOrderedEnumerable<string> GenerateForbiddenTwoLetterPairs(IEnumerable<string> letterGroups)
        {
            return letterGroups.SelectMany(GenerateAllDistinctTwoLetterPairs).Distinct().OrderBy(pair => pair);
        }
    }
}