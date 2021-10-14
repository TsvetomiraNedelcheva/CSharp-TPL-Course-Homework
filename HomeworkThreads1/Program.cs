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
            Stopwatch sw = new Stopwatch();
            var book = File.ReadAllText
                 (@"../../../book.txt",
                 Encoding.UTF8);
            Console.WriteLine("Non-threaded version: ");
            sw.Start();
            NumberOfWords(book);
            LongestWord(book); //counts intervals for a char ?
            ShortestWord(book);
            AvgLength(book);
            MostUsed(book);
            LeastUsed(book);
            sw.Stop();
            Console.WriteLine($"Time for non-threaded version: {sw.ElapsedMilliseconds}");

            Console.WriteLine("Threaded version: ");
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
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
            sw1.Stop(); //stopwatch stops before some threads end but thread.join() seems too slow
            Console.WriteLine($"Time for threaded version:{sw1.ElapsedMilliseconds}");
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

            foreach (string w in words.Select(x => x.ToLower()))
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

            foreach (string w in words.Select(x => x.ToLower()))
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
