using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataScraper
{
    class RawCsvWriter
    {
        public static void WriteCsv(string filePath, RawCsvTemplate rawCsv)
        {
            var sb = new StringBuilder(_buildLine(rawCsv.Headers));

            foreach(var line in rawCsv.Data)
            {
                sb.Append("\n");
                sb.Append(_buildLine(line));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private static string _buildLine(List<string> line)
        {
            var sb = new StringBuilder();

            foreach(var value in line)
            {
                sb.Append(value);
                sb.Append(",");
            }

            return sb.ToString().TrimEnd(',');
        }
    }
}
