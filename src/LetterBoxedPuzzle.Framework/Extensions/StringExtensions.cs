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
    ///     Class for string utility methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts the input string to having the first character uppercased.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The string with the first character in uppercase.</returns>
        public static string ToFirstCharUpper(this string input) =>
            input switch
                {
                    null => throw new ArgumentNullException(nameof(input)),
                    "" => throw new ArgumentException($"The parameter '{nameof(input)}' cannot be empty."),
                    _ => input[..1].ToUpperInvariant() + input[1..],
                };
    }
}