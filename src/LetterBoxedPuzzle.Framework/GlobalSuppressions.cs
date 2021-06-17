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
        Target = "~T:LetterBoxedPuzzle.Framework.Enums.AlphabetBitMask")]
[assembly:
    SuppressMessage(
        "StyleCop.CSharp.SpacingRules",
        "SA1000:Keywords should be spaced correctly",
        Justification = "The 'new' keyword should be followed by a space when using implicit types.",
        Scope = "member",
        Target = "~F:LetterBoxedPuzzle.Framework.Constants.AlphabetConstants.AlphabetBitMaskWithAllBitsSet")]