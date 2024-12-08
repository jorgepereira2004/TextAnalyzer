using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class TextAnalyzer
{
    static void Main()
    {
        Console.Write("Enter the file path: ");
        string filePath = Console.ReadLine();

        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);

            // Count statistics
            int wordCount = CountWords(text);
            int sentenceCount = CountSentences(text);
            int paragraphCount = CountParagraphs(text);
            int charCount = text.Length;

            // Calculate word frequency
            Dictionary<string, int> wordFrequency = CalculateWordFrequency(text);

            // Additional features
            string mostCommonWord = wordFrequency.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;
            double averageWordsPerSentence = (double)wordCount / sentenceCount;

            // Output statistics
            Console.WriteLine($"Number of words: {wordCount}");
            Console.WriteLine($"Number of sentences: {sentenceCount}");
            Console.WriteLine($"Number of paragraphs: {paragraphCount}");
            Console.WriteLine($"Number of characters: {charCount}");

            Console.WriteLine("\nWord frequency:");
            foreach (var entry in wordFrequency)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            Console.WriteLine($"\nMost common word: {mostCommonWord}");
            Console.WriteLine($"Average words per sentence: {averageWordsPerSentence}");
        }
        else
        {
            Console.WriteLine("File not found. Please provide a valid file path.");
        }
    }

    static int CountWords(string text)
    {
        return text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    static int CountSentences(string text)
    {
        return Regex.Matches(text, @"[.!?]").Count;
    }

    static int CountParagraphs(string text)
    {
        return text.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    static Dictionary<string, int> CalculateWordFrequency(string text)
    {
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

        string[] words = text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            string cleanedWord = Regex.Replace(word, @"[^\w\s]", "").ToLower();

            if (wordFrequency.ContainsKey(cleanedWord))
            {
                wordFrequency[cleanedWord]++;
            }
            else
            {
                wordFrequency[cleanedWord] = 1;
            }
        }

        return wordFrequency;
    }
}