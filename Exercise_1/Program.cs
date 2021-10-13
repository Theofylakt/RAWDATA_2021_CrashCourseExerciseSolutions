using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Exercise_1
{
    class Program
    {
        public static Regex rgx = new Regex("([0-9]+),\"([^\"]+)\"");

        static void Main(string[] args)
        {
            Console.WriteLine("Exercise_1\n");

            var lines = ReadFile("data.csv");

            Console.WriteLine("Longest title: {0}", LongestTitle(lines));

            int wordCount = 0;
            var words = ExtractWords(lines, out wordCount);

            Console.WriteLine("Number of words: {0}", wordCount);

            var outputFile = "words.csv";
            WriteWordsToFile(outputFile, words);
            Console.WriteLine("Words written to file {0}", outputFile);

        }

        public static string LongestTitle(List<Tuple<int, string>> lines)
        {
            string longestTitle = "";
            foreach (var line in lines)
            {
                if (longestTitle.Length < line.Item2.Length)
                    longestTitle = line.Item2;
            }
            return longestTitle;
        }

        public static List<Tuple<int, string>> ReadFile(string filename)
        {
            var result = new List<Tuple<int, string>>();
            try
            {
                using (var reader = new StreamReader(filename))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = Parse(reader.ReadLine(), rgx);
                        result.Add(line);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file:");
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public static List<Tuple<int, List<string>>> ExtractWords(List<Tuple<int, string>> lines, out int wordCount)
        {
            var words = new List<Tuple<int, List<string>>>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '(', ')', '-', '?', '/', '<', '>', '!', '=' };
            wordCount = 0;
            foreach (var line in lines)
            {
                var wordsInLine = line.Item2.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).ToList();
                wordCount += wordsInLine.Count;
                words.Add(Tuple.Create(line.Item1, wordsInLine));
            }
            return words;
        }

        public static void WriteWordsToFile(string outputFile, List<Tuple<int, List<string>>> words)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                foreach (var line in words)
                {
                    foreach (var word in line.Item2)
                    {
                        writer.WriteLine("{0},\"{1}\"", line.Item1, word.ToLower());
                    }
                }
            }
        }

        public static Tuple<int, string> Parse(string line, Regex regex)
        {
            var matches = regex.Matches(line);
            if (matches.Count > 0)
            {
                return Tuple.Create(
                        int.Parse(matches[0].Groups[1].Value),
                        matches[0].Groups[2].Value
                    );
            }
            return null;
        }
    }
}
