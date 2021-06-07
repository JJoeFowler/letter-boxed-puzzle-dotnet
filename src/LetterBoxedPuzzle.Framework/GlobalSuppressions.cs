// ===============================================================================================================================================
// <copyright file="GlobalSuppressions.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1602:Enumeration items should be documented",
        Justification = "Letter names are self-evident.",
        Scope = "type",
        Target = "~T:LetterBoxedPuzzle.Framework.Enums.AlphabetLetters")]