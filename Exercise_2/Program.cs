using System;

namespace Exercise_2
{
    class Program
    {
        // I'v made the reverse Polish calculator as a static class
        // Then you do not need to instantiate an object to use it.
        // It is also fine to implement it as an normal class
        // Did the same with the shooting yard class

        static ConsoleColor defaultForgroundColor = Console.ForegroundColor;

        static void Main(string[] args)
        {
            // Just for fun - coloring the console text :-)

            WriteColorLine(ConsoleColor.Cyan, "Exercise_2\n");

            // Example from exercise
            WriteColor(ConsoleColor.Green, "5 1 2 + 4 * + 3 - ");
            Console.Write("= ");
            WriteColorLine(ConsoleColor.Red, ReversePolishCalculator.Compute("5 1 2 + 4 * + 3 -"));

            // Example from https://en.wikipedia.org/wiki/Shunting-yard_algorithm


            WriteColor(ConsoleColor.Green, "3 + 4 * 2 / (1 - 5) ^ 2 ^ 3 ");
            Console.Write("= ");
            WriteColorLine(ConsoleColor.Red, ReversePolishCalculator.Compute(ShuntingYard.Parse("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3")));

            Console.WriteLine("\nComputed with doubles then");
            WriteColor(ConsoleColor.Green, "3 + 4 * 2 / (1 - 5) ^ 2 ^ 3 ");
            Console.Write("= ");
            WriteColorLine(ConsoleColor.Red, " 3.0001220703125");
        }

        static void WriteColorLine(ConsoleColor color, object output)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ForegroundColor = currentColor;
        }

        static void WriteColor(ConsoleColor color, object output)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(output);
            Console.ForegroundColor = currentColor;
        }
    }
}
