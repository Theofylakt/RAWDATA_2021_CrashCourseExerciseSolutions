using System;
using Exercise_2;
using Xunit;

namespace Exercise_2_Tests
{
    public class ReversePolishCalculatorTests
    {
        [Fact]
        public void Compute_WithEmptyString_ReturnZero()
        {
            Assert.Equal(0, ReversePolishCalculator.Compute(""));
        }

        [Fact]
        public void Compute_WithNullString_ReturnZero()
        {
            Assert.Equal(0, ReversePolishCalculator.Compute(null));
        }

        [Fact]
        public void Compute_WithSingleNumber_ReturnTheNumber()
        {
            Assert.Equal(7, ReversePolishCalculator.Compute("7"));
        }

        [Fact]
        public void Compute_WithAdditionOfTwoNumbers_ReturnTheSum()
        {
            Assert.Equal(12, ReversePolishCalculator.Compute("7 5 +"));
        }

        [Fact]
        public void Compute_WithSubstractionOfTwoNumbers_ReturnTheDifference()
        {
            Assert.Equal(2, ReversePolishCalculator.Compute("7 5 -"));
        }

        [Fact]
        public void Compute_WithMultiplicationOfTwoNumbers_ReturnTheProduct()
        {
            Assert.Equal(35, ReversePolishCalculator.Compute("7 5 *"));
        }

        [Fact]
        public void Compute_WithDivisionOfTwoNumbers_ReturnTheQuotient()
        {
            Assert.Equal(2, ReversePolishCalculator.Compute("10 5 /"));
        }

        [Fact]
        public void Compute_WithNumberRaisedToExponent_ReturnThePower()
        {
            Assert.Equal(100000, ReversePolishCalculator.Compute("10 5 ^"));
        }

        [Fact]
        public void Compute_512Plus4TimesPlus3Minus_Return14()
        {
            Assert.Equal(14, ReversePolishCalculator.Compute("5 1 2 + 4 * + 3 -"));
        }

        [Fact]
        public void Compute_Sqrt16_Return4()
        {
            Assert.Equal(4, ReversePolishCalculator.Compute("16 sqrt"));
        }

        [Fact]
        public void Compute_Pow2To8_Return256()
        {
            Assert.Equal(256, ReversePolishCalculator.Compute("2 8 pow"));

        }

        [Fact]
        public void Compute_InvalidExpression_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>ReversePolishCalculator.Compute("5 1"));
        }

        [Fact]
        public void Compute_InvalidExpression2_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => ReversePolishCalculator.Compute("+ 1"));

        }
    }
}
