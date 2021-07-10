// ===============================================================================================================================================
// <copyright file="TestCommonAssertions.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Common assertions used for unit tests.
    /// </summary>
    internal static class TestCommonAssertions
    {
        /// <summary>
        ///     Asserts whether given exception type is thrown for all of the given invalid inputs to the given test method.  If not, an assert fails
        ///     with an error message using the given description of the input.
        /// </summary>
        /// <typeparam name="TInput">The type of input given to the test method.</typeparam>
        /// <typeparam name="TOutput">The type of the output given to the test method.</typeparam>
        /// <param name="testFunction">The test function.</param>
        /// <param name="expectedExceptionType">The expected exception types.</param>
        /// <param name="invalidInputs">The invalid inputs.</param>
        /// <param name="invalidInputDescriptionFunction">The description function for an invalid input.</param>
        internal static void AssertExceptionThrown<TInput, TOutput>(
            Func<TInput, TOutput> testFunction,
            Type expectedExceptionType,
            IEnumerable<TInput> invalidInputs,
            Func<TInput, string> invalidInputDescriptionFunction)
        {
            static void AssertMethod(TOutput output, string message) => Assert.AreEqual(null, output, message);
            AssertExceptionThrown(AssertMethod, testFunction, expectedExceptionType, invalidInputs, invalidInputDescriptionFunction);
        }

        /// <summary>
        ///     Asserts whether given exception type is thrown for all of the given invalid inputs to the given test method.
        ///     If not, the given assert method fails with an error message using the given description of the input.
        /// </summary>
        /// <typeparam name="TInput">The type of input given to the test method.</typeparam>
        /// <typeparam name="TOutput">The type of the output given to the test method.</typeparam>
        /// <param name="assertMethod">The assert method.</param>
        /// <param name="testFunction">The test function.</param>
        /// <param name="expectedExceptionType">The expected exception types.</param>
        /// <param name="invalidInputs">The invalid inputs.</param>
        /// <param name="invalidInputDescriptionFunction">The description function for an invalid input.</param>
        internal static void AssertExceptionThrown<TInput, TOutput>(
            Action<TOutput, string> assertMethod,
            Func<TInput, TOutput> testFunction,
            Type expectedExceptionType,
            IEnumerable<TInput> invalidInputs,
            Func<TInput, string> invalidInputDescriptionFunction)
        {
            foreach (var invalidInput in invalidInputs)
            {
                var invalidInputDescription = invalidInputDescriptionFunction(invalidInput);

                if (!IsArrayOrEnumerableType(typeof(TInput)))
                {
                    invalidInputDescription += $" input '{invalidInput?.ToString() ?? "null"}'";
                }

                var messageEnd = $"as expected for {invalidInputDescription}";
                try
                {
                    // ReSharper disable once SuggestVarOrType_SimpleTypes
                    TOutput output = testFunction(invalidInput);

                    assertMethod(output, $"Did not throw an {expectedExceptionType} {messageEnd}.");
                }
                catch (Exception exception) when (exception is not AssertFailedException)
                {
                    var exceptionType = exception.GetType();
                    Assert.AreEqual(
                        expectedExceptionType,
                        exceptionType,
                        $"Threw an '{exceptionType}' instead of '{expectedExceptionType}' {messageEnd}.");
                }
            }
        }

        /// <summary>
        ///     Determines whether the given input type is either an array or an enumerable of integers, characters, or strings.
        /// </summary>
        /// <param name="inputType">The input type.</param>
        /// <returns>
        ///     <see langword="true" /> if the input type is either <see cref="Array" /> or an <see cref="List{T}" /> of
        ///     <see langword="int" />, <see langword="char" />  or <see langword="string" />; or  <see langword="false" /> otherwise.
        /// </returns>
        private static bool IsArrayOrEnumerableType(Type inputType)
        {
            var arrayTypes = new[] { typeof(int[]), typeof(char[]), typeof(string[]) };
            var enumerableTypes = new[] { typeof(List<int>), typeof(List<char>), typeof(List<string>) };
            var bothTypes = enumerableTypes.Union(arrayTypes);

            return bothTypes.Any(type => type == inputType);
        }
    }
}