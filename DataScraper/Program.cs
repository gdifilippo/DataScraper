using DataScraper.Dictionary;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var urlDict = LookUps.Urls;

            foreach(var statName in urlDict.Keys)
            {
                var rawCsv = TableExtract.ExtractRawCsvFromHtml(urlDict[statName]);
                RawCsvWriter.WriteCsv(@"C:\Users\gdifi\source\repos\DataScraper\DataScraper\bin\Debug\" + statName + ".csv", rawCsv);
            }
            
            Console.WriteLine("done!");
            Console.ReadKey();
        }
    }
}
