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
    class TableExtract
    {
        public static RawCsvTemplate ExtractRawCsvFromHtml(string url)
        {
            var htmlString = _getHtml(url);
            return _htmlToRawCsv(htmlString);
        }

        private static string _getHtml(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var data = "";

            if (response.StatusCode != HttpStatusCode.OK)
                throw new HtmlWebException($"Unable to reach Website: {url}. Status: {response.StatusCode}");

            var receiveStream = response.GetResponseStream();

            if (receiveStream == null)
                throw new ArgumentNullException("received stream is null");

            var readStream = response.CharacterSet == null ?
                new StreamReader(receiveStream) :
                new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

            data = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            return data;
        }

        private static RawCsvTemplate _htmlToRawCsv(string html)
        {
            if (html == null || html == "")
            {
                throw new ArgumentNullException("Empty html.");
            }

            var doc = new HtmlDocument();

            doc.LoadHtml(html);

            var headers = doc.DocumentNode.SelectSingleNode("//table")
                .Descendants("tr")
                .Where(tr => tr.Elements("th").Count() > 1)
                .SelectMany(tr => tr.Elements("th").Select(th => th.InnerText.Trim()))
                .ToList();

            var table = doc.DocumentNode.SelectSingleNode("//table")
                .Descendants("tr")
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

            return new RawCsvTemplate(headers, table);
        }
    }
}
