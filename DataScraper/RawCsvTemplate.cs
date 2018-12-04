using System;
using System.Collections.Generic;

namespace DataScraper
{
    class RawCsvTemplate
    {
        public RawCsvTemplate(List<string> header, List<List<string>> data)
        {
            Headers = header ?? throw new ArgumentNullException(nameof(header));
            Data = data ?? throw new ArgumentNullException(nameof(data));

            validate();
        }

        public List<string> Headers { get; }
        public List<List<string>> Data { get; }

        private void validate()
        {
            int count = 0;
            foreach (var row in Data)
            {
                if (Headers.Count != row.Count)
                {
                    throw new Exception($"Row: {count} has an incorrect amount of fields.");
                }

                count++;
            }
        }
    }
}
