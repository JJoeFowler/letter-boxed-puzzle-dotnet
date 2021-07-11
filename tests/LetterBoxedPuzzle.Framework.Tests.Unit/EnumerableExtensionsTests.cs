// ===============================================================================================================================================
// <copyright file="EnumerableExtensionsTests.cs" company="Joe Fowler">
// Copyright (c) 2021 Joe Fowler.
// Licensed under the GNU Affero General Public License v3. See LICENSE file in the project root for full license information.
// </copyright>
// ===============================================================================================================================================

namespace LetterBoxedPuzzle.Framework.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LetterBoxedPuzzle.Framework.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Unit tests for the string utility methods class.
    /// </summary>
    [TestClass]
    public class EnumerableExtensionsTests
    {
        /// <summary>
        ///     Verify whether  given a null value that a single-element enumerable with a null value is returned.
        /// </summary>
        [TestMethod]
        public void AsSingleElement_GivenNullValue_ReturnsSingleElementEnumerableWithNullValue()
        {
            // Arrange
            string? nullValue = null;

            // Act
            var actualEnumerable = nullValue.AsSingleElement();
            var actualCount = actualEnumerable.Count();

            // Assert
            Assert.AreEqual(1, actualCount, $"Expected one element, not {actualCount} elements to be returned.");
            Assert.IsNull(actualEnumerable.First(), "The single element of the enumerable is not null.");
        }

        /// <summary>
        ///     Verify whether  given a non-null value that a single-element enumerable with the value is returned.
        /// </summary>
        [TestMethod]
        public void AsSingleElement_GivenNonNullValue_ReturnsAsSingleElementEnumerableWithValue()
        {
            // Arrange
            object[] values = { 'a', "B", 1, 2.3f };
            var expectedCount = values.Length;

            // Act
            var actualEnumerable = values.Select(value => value.AsSingleElement()).ToArray();
            var actualCounts = actualEnumerable.Select(x => x.Count()).ToArray();

            // Assert
            for (var index = 0; index < expectedCount; index++)
            {
                var actualCount = actualCounts[index];
                Assert.AreEqual(1, actualCount, $"Expected one element, not {actualCount} elements to be returned.");

                var expectedValue = values[index];
                var actualValue = actualEnumerable[index].First();
                Assert.AreEqual(expectedValue, actualValue, $"Expected the single value to be '{expectedValue}' instead of '{actualValue}'.");
            }
        }

        /// <summary>
        ///     Verify whether an argument null exception is thrown when given a null value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CartesianProduct_GivenNullValue_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<IEnumerable<string>> nullValue = null!;

            // Act
            _ = nullValue.CartesianProduct();
        }

        /// <summary>
        ///     Verify whether an argument exception is thrown when given sequence with an empty collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CartesianProduct_GivenSequenceWithNullValue_ThrowsArgumentNullException()
        {
            // Arrange
            var sequenceWithEmptyCollection = new[] { new[] { "a", "b" }, null, new[] { "c", "d" } };

            // Act
            _ = sequenceWithEmptyCollection!.CartesianProduct();
        }

        /// <summary>
        ///     Verify whether an argument exception is thrown when given sequence with an empty collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CartesianProduct_GivenSequenceWithEmptyCollection_ThrowsArgumentNullException()
        {
            // Arrange
            var sequenceWithEmptyCollection = new[] { new[] { "a", "b" }, Array.Empty<string>(), new[] { "c", "d" } };

            // Act
            _ = sequenceWithEmptyCollection.CartesianProduct();
        }

        /// <summary>
        ///     Verifies whether given a sequence of four pairs that the 16-element Cartesian product is returned.
        /// </summary>
        [TestMethod]
        public void CartesianProduct_GivenSequenceOfFourPairs_ReturnsSixteenElementCartesianProduct()
        {
            // Arrange
            var characterPair = new object[] { 'a', 'b' };
            var integerPair = new object[] { 1, 2 };
            var wordPair = new object[] { "three", "four" };
            var floatingPointPair = new object[] { 5.6f, 7.8f };

            var pairs = new[] { characterPair, integerPair, wordPair, floatingPointPair };

            var expectedCartesianProduct = new[]
                {
                    new object[] { 'a', 1, "three", 5.6f }, new object[] { 'a', 1, "three", 7.8f }, new object[] { 'a', 1, "four", 5.6f },
                    new object[] { 'a', 1, "four", 7.8f }, new object[] { 'a', 2, "three", 5.6f }, new object[] { 'a', 2, "three", 7.8f },
                    new object[] { 'a', 2, "four", 5.6f }, new object[] { 'a', 2, "four", 7.8f }, new object[] { 'b', 1, "three", 5.6f },
                    new object[] { 'b', 1, "three", 7.8f }, new object[] { 'b', 1, "four", 5.6f }, new object[] { 'b', 1, "four", 7.8f },
                    new object[] { 'b', 2, "three", 5.6f }, new object[] { 'b', 2, "three", 7.8f }, new object[] { 'b', 2, "four", 5.6f },
                    new object[] { 'b', 2, "four", 7.8f },
                };

            var expectedLength = expectedCartesianProduct.Length;

            // Act
            var actualCartesianProduct = pairs.CartesianProduct().Select(product => product.ToArray()).ToArray();
            var actualLength = actualCartesianProduct.Length;

            // Assert
            Assert.AreEqual(
                expectedLength,
                actualLength,
                $"Expected the Cartesian product to have {expectedLength} elements instead of {actualLength} elements.");

            for (var productIndex = 0; productIndex < expectedCartesianProduct.Length; productIndex++)
            {
                for (var itemIndex = 0; itemIndex < pairs.Length; itemIndex++)
                {
                    var expectedItem = expectedCartesianProduct[productIndex][itemIndex];
                    var actualItem = actualCartesianProduct[productIndex][itemIndex];

                    Assert.AreEqual(
                        expectedItem,
                        actualItem,
                        $"Expected item '{expectedItem}', not '{actualItem}', for item {itemIndex + 1} of element {productIndex + 1} of the product. ");
                }
            }
        }
    }
}