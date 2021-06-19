// ===============================================================================================================================================
// <copyright file="StringUtilities.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Utilities
{
    using System.Linq;

    /// <summary>
    ///     Class for string utility methods.
    /// </summary>
    public static class StringUtilities
    {
        /// <summary>
        ///     Quote and join the given text values with a comma and space, where given text array [ text1, text2, ..., text<i>N</i>
        ///     of length <i>N</i> ] would yield "'text1', 'text2', ..., 'text<i>N</i>'", where <see langword="null " /> values are replaced
        ///     by the word "null".
        /// </summary>
        /// <param name="textValues">The text values.</param>
        /// <returns>The join of each text value quoted and separated by a comma and a space.</returns>
        public static string QuoteJoin(params string[] textValues)
        {
            return $"'{string.Join("', '", textValues.Select(value => value ?? "null"))}'";
        }
    }
}