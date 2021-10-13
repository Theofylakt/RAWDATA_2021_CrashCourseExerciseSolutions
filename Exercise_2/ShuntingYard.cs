using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class ShuntingYard
    {
        enum Associativity
        {
            Unknown,
            Left,
            Right
        }

        public static string Parse(string input)
        {
            var stack = new Stack<string>();
            var queue = new Queue<string>();

            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            foreach (var token in input.Split())
            {
                int number;
                if (int.TryParse(token, out number))
                {
                    queue.Enqueue(token);
                }
                else
                {
                    switch (token)
                    {
                        case "sqrt":
                        case "pow":
                            stack.Push(token);
                            break;
                        case ",":
                            while (stack.Peek() != "(")
                            {
                                queue.Enqueue(stack.Pop());
                                if (stack.Count == 0)
                                {
                                    throw new ArgumentException("Missing left parentheses");
                                }
                            }
                            break;
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                        case "^":
                            if (stack.Count > 0)
                            {
                                if (CheckLeftAssociative(token, stack.Peek()) || CheckRightAssociative(token, stack.Peek()))
                                {
                                    queue.Enqueue(stack.Pop());
                                }
                            }
                            stack.Push(token);
                            break;
                        case "(":
                            stack.Push(token);
                            break;
                        case ")":
                            while (stack.Peek() != "(")
                            {
                                queue.Enqueue(stack.Pop());
                                if (stack.Count == 0)
                                {
                                    throw new ArgumentException("Missing left parentheses");
                                }
                            }
                            stack.Pop();
                            break;

                    }
                }
            }

            foreach (var oper in stack)
            {
                queue.Enqueue(oper);
            }

            return string.Join(" ", queue);
        }

        public static bool CheckRightAssociative(string o1, string o2)
        {
            return GetAssociativity(o1) == Associativity.Right
                && Precedence(o1) < Precedence(o2);
        }

        public static bool CheckLeftAssociative(string o1, string o2)
        {
            return GetAssociativity(o1) == Associativity.Left
                && Precedence(o1) <= Precedence(o2);
        }

        // used values from https://en.wikipedia.org/wiki/Order_of_operations
        private static int Precedence(string oper)
        {
            switch (oper)
            {
                case "+": case "-": return 1;
                case "*": case "/": return 2;
                case "^": case "sqrt": return 3;
            }
            return 0;
        }

        // used info from https://en.wikipedia.org/wiki/Operator_associativity
        private static Associativity GetAssociativity(string oper)
        {
            switch (oper)
            {
                case "+": case "-": case "*": case "/": return Associativity.Left;
                case "^": return Associativity.Right;
                default: return Associativity.Unknown;
            }
        }
    }
}
