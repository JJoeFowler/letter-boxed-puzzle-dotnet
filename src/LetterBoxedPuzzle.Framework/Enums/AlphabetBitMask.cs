// ===============================================================================================================================================
// <copyright file="AlphabetBitMask.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================
namespace LetterBoxedPuzzle.Framework.Enums
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     The bit-wise enumeration for the 26 letters of the English alphabet.
    /// </summary>
    [Flags]
    [SuppressMessage("ReSharper", "StyleCop.SA1602", Justification = "Letter names are self-evident.")]
    public enum AlphabetBitMask
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        None = 0,
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2,
        D = 1 << 3,
        E = 1 << 4,
        F = 1 << 5,
        G = 1 << 6,
        H = 1 << 7,
        I = 1 << 8,
        J = 1 << 9,
        K = 1 << 10,
        L = 1 << 11,
        M = 1 << 12,
        N = 1 << 13,
        O = 1 << 14,
        P = 1 << 15,
        Q = 1 << 16,
        R = 1 << 17,
        S = 1 << 18,
        T = 1 << 19,
        U = 1 << 20,
        V = 1 << 21,
        W = 1 << 22,
        X = 1 << 23,
        Y = 1 << 24,
        Z = 1 << 25,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}