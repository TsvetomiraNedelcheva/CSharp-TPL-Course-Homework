using System;
using AngleSharp;
using System.IO;
using System.Threading.Tasks;
using AngleSharp.Dom;
using System.Collections.Generic;

namespace PriceScraper
{
    class Program
    {
        public static async Task<string> ValueScraper(string link)
        {
            Url url = new Url(link);

            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IDocument doc = await BrowsingContext.New(config).OpenAsync(url);

            string value = doc.QuerySelector(".regular-price").TextContent.Trim();
            return value;
        }

        static async Task Main(string[] args)
        {
            string links = await File.ReadAllTextAsync(@"../../../books.txt");
            List<Task<string>> taskList = new List<Task<string>>();

            string[] splitedLinks = links.Split("\r\n");

            for (int i = 0; i < splitedLinks.Length; i++)
            {
                string link = splitedLinks[i];
                Task<string> task = Task.Run(() => ValueScraper(link));
                taskList.Add(task);
            }

            Task<string[]> fanIn = Task.WhenAll(taskList);
            Task<string[]> final = fanIn.ContinueWith(x => x.Result);
            for (int i = 0; i < final.Result.Length; i++)
            {
                Console.WriteLine($"Price -> {final.Result[i]}");
            } 
        }

      
    }

}
