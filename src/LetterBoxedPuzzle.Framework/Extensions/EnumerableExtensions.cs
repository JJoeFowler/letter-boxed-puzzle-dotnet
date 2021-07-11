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
        /// <returns>A single-element enumerable containing the specified value.</returns>
        public static IEnumerable<TValue> AsSingleElement<TValue>(this TValue value) => new[] { value };

        /// <summary>
        ///     <para>
        ///         Computes the Cartesian product of the specified sequence of <i>N</i> enumerables, each with <i>x1</i>, <i>x2</i>, ...,
        ///         <i>xN</i> items, respectively, resulting in a sequence <i>x1 * x2 * ... * xN</i> long of <i>N</i>-element groups.
        ///     </para>
        ///     <para>
        ///         Each resulting <i>N</i>-element group contains one element from each of the specified <i>N</i> enumerables of items,
        ///         preserving the order of the original sequence, such that no two groups of the Cartesian project contain the exact same items.
        ///     </para>
        /// </summary>
        /// <typeparam name="TValue">The type of the items of all the groups.</typeparam>
        /// <param name="groupsSequence">The sequences of groups of values.</param>
        /// <returns>The sequence of elements forming the Cartesian product of the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when any of the specified enumerables are empty.</exception>
        public static IEnumerable<IEnumerable<TValue>> CartesianProduct<TValue>(this IEnumerable<IEnumerable<TValue>> groupsSequence) =>
            groupsSequence switch
                {
                    null => throw new ArgumentNullException(nameof(groupsSequence)),

                    _ when groupsSequence.Any(group => group?.Any() != true) => throw new ArgumentException(
                        $"'{nameof(groupsSequence)}' cannot contain a null value or be empty."),

                    _ => groupsSequence.Aggregate(
                        Enumerable.Empty<TValue>().AsSingleElement(),
                        (current, groups) => current.SelectMany(_ => groups, (group, item) => @group.Concat(new[] { item }))),
                };
    }
}