// ===============================================================================================================================================
// <copyright file="StringExtensions.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Extensions
{
    using System;

    /// <summary>
    ///     Class for extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts the input string to having the first character uppercased.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The string with the first character in uppercase.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given a null value.</exception>
        /// <exception cref="ArgumentException">Thrown when given an empty string.</exception>
        public static string ToFirstCharUpper(this string input) =>
            input switch
                {
                    null => throw new ArgumentNullException(nameof(input)),

                    "" => throw new ArgumentException($"'{nameof(input)}' cannot be empty."),

                    _ => input[..1].ToUpperInvariant() + input[1..],
                };
    }
}