using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public static class ReversePolishCalculator
    {
        public static int Compute(string input)
        {
            int number = 0;
            var stack = new Stack<int>();
            if (string.IsNullOrEmpty(input))
            {
                return number;
            }

            foreach (var token in input.Split())
            {
                if (int.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                switch (token)
                {
                    case "+":
                        CheckBinaryOperation(stack, token);
                        stack.Push(stack.Pop() + stack.Pop());
                        break;
                    case "-":
                        CheckBinaryOperation(stack, token);
                        number = stack.Pop();
                        stack.Push(stack.Pop() - number);
                        break;
                    case "*":
                        CheckBinaryOperation(stack, token);
                        stack.Push(stack.Pop() * stack.Pop());
                        break;
                    case "/":
                        CheckBinaryOperation(stack, token);
                        number = stack.Pop();
                        stack.Push(stack.Pop() / number);
                        break;
                    case "^":
                    case "pow":
                        CheckBinaryOperation(stack, token);
                        number = stack.Pop();
                        stack.Push((int)Math.Pow(stack.Pop(), number));
                        break;
                    case "sqrt":
                        CheckUnaryOperation(stack, token);
                        stack.Push((int)Math.Sqrt(stack.Pop()));
                        break;
                }
            }
            if (stack.Count > 1)
            {
                throw new ArgumentException("Invalid expression: {0}", input);
            }
            return stack.Pop();
        }


        private static void CheckBinaryOperation(Stack<int> stack, string oper)
        {
            if (stack.Count < 2)
            {
                throw new ArgumentException("Missing operands for binary operation '{0}'", oper);
            }
        }

        private static void CheckUnaryOperation(Stack<int> stack, string oper)
        {
            if (stack.Count < 1)
            {
                throw new ArgumentException("Missing operands for unary operation '{0}'", oper);
            }
        }
    }
}
