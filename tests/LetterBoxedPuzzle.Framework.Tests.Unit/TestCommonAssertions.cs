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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Common assertions used for unit tests.
    /// </summary>
    internal static class TestCommonAssertions
    {
        /// <summary>
        ///     Asserts whether given exception type is throw for all of the given invalid inputs to the given test method.  If not, an assert fails
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
            static void AssertFunction(TOutput output, string message) => Assert.IsNotNull(output, message);
            AssertExceptionThrown(AssertFunction, testFunction, expectedExceptionType, invalidInputs, invalidInputDescriptionFunction);
        }

        /// <summary>
        ///     Asserts whether given exception type is throw for all of the given invalid inputs to the given test method.  If not, an assert fails
        ///     with an error message using the given description of the input.
        /// </summary>
        /// <typeparam name="TInput">The type of input given to the test method.</typeparam>
        /// <typeparam name="TOutput">The type of the output given to the test method.</typeparam>
        /// <param name="assertFunction">The assert function.</param>
        /// <param name="testFunction">The test function.</param>
        /// <param name="expectedExceptionType">The expected exception types.</param>
        /// <param name="invalidInputs">The invalid inputs.</param>
        /// <param name="invalidInputDescriptionFunction">The description function for an invalid input.</param>
        internal static void AssertExceptionThrown<TInput, TOutput>(
            Action<TOutput, string> assertFunction,
            Func<TInput, TOutput> testFunction,
            Type expectedExceptionType,
            IEnumerable<TInput> invalidInputs,
            Func<TInput, string> invalidInputDescriptionFunction)
        {
            foreach (var invalidInput in invalidInputs)
            {
                var invalidInputDescription = invalidInputDescriptionFunction(invalidInput);

                var messageEnd = $" as expected for {invalidInputDescription} input '{invalidInput?.ToString() ?? "null"}'";
                try
                {
                    var output = testFunction(invalidInput);

                    assertFunction(output, $"Did not throw an {expectedExceptionType} {messageEnd}");
                }
                catch (Exception exception)
                {
                    var exceptionType = exception.GetType();
                    Assert.IsTrue(
                        exceptionType == expectedExceptionType,
                        $"Threw an '{exceptionType}' instead of '{expectedExceptionType}' {messageEnd}");
                }
            }
        }
    }
}