// ===============================================================================================================================================
// <copyright file="EnumerableExtensions.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Class for enumerable extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Converts the specified value into a single-element enumerable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>A single-element enumerable.</returns>
        public static IEnumerable<TValue> AsSingleElement<TValue>(this TValue value) => new[] { value };

        /// <summary>
        ///     Computes the Cartesian product of the specified sequences of <i>N</i> groups, each with <i>K</i> values, resulting in sequence
        ///     of <i>N * K</i> products. Each <i>K</i>-element product contains one element from each of the specified <i>N</i> groups of
        ///     values, such that no two products contain the exact same elements.
        /// </summary>
        /// <typeparam name="TValue">The type of values of the groups.</typeparam>
        /// <param name="groupsSequence">The sequences of groups of values.</param>
        /// <returns>The sequences of products forming the Cartesian product of the input sequence of groups.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when any of the specified groups are empty.</exception>
        public static IEnumerable<IEnumerable<TValue>> CartesianProduct<TValue>(this IEnumerable<IEnumerable<TValue>> groupsSequence) =>
            groupsSequence switch
                {
                    null => throw new ArgumentNullException(nameof(groupsSequence)),

                    _ when groupsSequence.Any(group => group?.Any() != true) => throw new ArgumentException(
                        $"'{nameof(groupsSequence)}' cannot contain a null value or an empty group."),

                    _ => groupsSequence.Aggregate(
                        Enumerable.Empty<TValue>().AsSingleElement(),
                        (current, groups) => current.SelectMany(_ => groups, (product, value) => product.Concat(new[] { value }))),
                };
    }
}