using System;
using Exercise_2;
using Xunit;

namespace Exercise_2_Tests
{
    public class ShuntingYardTests
    {
        // using the operations from https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        // as input for the tests

        [Fact]
        public void Parse_Number_ReturnNumber()
        {
            Assert.Equal("3", ShuntingYard.Parse("3"));
        }

        [Fact]
        public void Parse_NumberAddition_ReturnRpn()
        {
            Assert.Equal("3 4 +", ShuntingYard.Parse("3 + 4"));
            Assert.Equal(7, Calculate("3 + 4"));
        }

        [Fact]
        public void Parse_AdditionAndMultiplication_ReturnRpn()
        {
            Assert.Equal("3 4 2 * +", ShuntingYard.Parse("3 + 4 * 2"));
            Assert.Equal(11, Calculate("3 + 4 * 2"));
        }

        [Fact]
        public void Parse_ExpressionInParentheses_ReturnRpn()
        {
            Assert.Equal("1 5 -", ShuntingYard.Parse("( 1 - 5 )"));
            Assert.Equal(-4, Calculate("1 5 -"));
        }

        [Fact]
        public void Parse_MissingLeftParenthesesInExpression_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => ShuntingYard.Parse("1 - 5 )"));
        }

        [Fact]
        public void Parse_CheckLeftAssociative_ReturnBool()
        {
            Assert.True(ShuntingYard.CheckLeftAssociative("+", "-"));
            Assert.True(ShuntingYard.CheckLeftAssociative("+", "*"));
            Assert.False(ShuntingYard.CheckLeftAssociative("*", "-"));
            Assert.True(ShuntingYard.CheckLeftAssociative("*", "/"));
            Assert.True(ShuntingYard.CheckLeftAssociative("*", "^"));
            Assert.False(ShuntingYard.CheckLeftAssociative("^", "*"));
        }

        [Fact]
        public void Parse_CheckRightAssociative_ReturnBool()
        {
            Assert.False(ShuntingYard.CheckRightAssociative("^", "-"));
            Assert.False(ShuntingYard.CheckRightAssociative("^", "*"));
            Assert.False(ShuntingYard.CheckRightAssociative("^", "+"));
            Assert.False(ShuntingYard.CheckRightAssociative("^", "/"));
        }

        [Fact]
        public void Parse_MixedExpressionWithParentheses_ReturnRpn()
        {
            Assert.Equal("3 4 2 * 1 5 - / +", ShuntingYard.Parse("3 + 4 * 2 / ( 1 - 5 )"));
            Assert.Equal(1, Calculate("3 + 4 * 2 / ( 1 - 5 )"));
        }

        [Fact]
        public void Parse_MixedExpressionWithParenthesesAndPower_ReturnRpn()
        {
            Assert.Equal("3 4 2 * 1 5 - 2 3 ^ ^ / +", ShuntingYard.Parse("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"));
        }

        [Fact]
        public void Parse_Sqrt_ReturnRpn()
        {
            Assert.Equal("16 sqrt", ShuntingYard.Parse("sqrt ( 16 )"));
            Assert.Equal(4, Calculate("sqrt ( 16 )"));
        }

        [Fact]
        public void Parse_Pow_ReturnRpn()
        {
            Assert.Equal("2 8 pow", ShuntingYard.Parse("pow ( 2 , 8 )"));
            Assert.Equal(256, Calculate("pow ( 2 , 8 )"));
        }

        [Fact]
        public void Parse_MissingLeftParentheses_ThrowsException()
        {
           Assert.Throws<ArgumentException>(() => ShuntingYard.Parse("pow 2 , 8 )"));
        }


        // Helper methods 
        // to make the tests more readable
        private int Calculate(string expression)
        {
            return ReversePolishCalculator.Compute(ShuntingYard.Parse(expression));
        }
    }
}
