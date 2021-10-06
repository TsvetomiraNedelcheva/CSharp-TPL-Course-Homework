using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace HomeworkThreads1
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = File.ReadAllText
                 (@"../../../book.txt",
                 Encoding.UTF8);
            Console.WriteLine("Non-threaded version: ");
            NumberOfWords(book);
            LongestWord(book); //counts intervals for a char ?
            ShortestWord(book);
            AvgLength(book);
            MostUsed(book);
            LeastUsed(book);

            Console.WriteLine("Threaded version: ");
            Thread numbers = new Thread(() => NumberOfWords(book));
            numbers.Start();
            Thread longest = new Thread(() => LongestWord(book));
            longest.Start();
            Thread shortest = new Thread(() => ShortestWord(book));
            shortest.Start();
            Thread average = new Thread(() => AvgLength(book));
            average.Start();
            Thread mostUsed = new Thread(() => MostUsed(book));
            mostUsed.Start();
            Thread leastUsed = new Thread(() => LeastUsed(book));
            leastUsed.Start();
        }

        static void NumberOfWords(string book)
        {
            Console.WriteLine($"Number of words: {book.Split(" ",StringSplitOptions.RemoveEmptyEntries).Length}");
        }

        static void LongestWord(string book)
        {
            var words = book.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine
                ($"Longest word: {words.OrderByDescending(x => x.Length).FirstOrDefault()}");
        }

        static void ShortestWord(string book)
        {
            var words = book.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine
                ($"Shortest word: {words.OrderBy(x => x.Length).FirstOrDefault()}");
        }

        static void AvgLength(string book)
        {
            var words = book.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine
                ($"Average word length: {words.Average(x => x.Length):f2}");
        }

        static void MostUsed(string book)
        {
            var words = book.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> dic = new Dictionary<string, int>();

            foreach (string w in words)
            {
                if (dic.Keys.Contains(w))
                {
                    dic[w]++;
                }
                else
                {
                    dic.Add(w, 1);
                }
            }

            var mostUsedWord = dic.OrderByDescending(v => v.Value).FirstOrDefault();
            Console.WriteLine($"Most used word: {mostUsedWord.Key} - {mostUsedWord.Value}");
        }

        static void LeastUsed(string book)
        {
            var words = book.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> dic = new Dictionary<string, int>();

            foreach (string w in words)
            {
                if (dic.Keys.Contains(w))
                {
                    dic[w]++;
                }
                else
                {
                    dic.Add(w, 1);
                }
            }

            var leastUsed = dic.OrderBy(v => v.Value).Skip(1).FirstOrDefault();
            Console.WriteLine($"Least used word: {leastUsed.Key} - {leastUsed.Value}");
        }

    }
}
