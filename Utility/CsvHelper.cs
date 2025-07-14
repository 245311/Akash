using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WBS_API.Utility
{
    public static class CsvHelper
    {
        public static string ConvertCsvToJson(string csvData)
        {
            if (string.IsNullOrWhiteSpace(csvData))
                return "{}"; // Return empty JSON object if CSV is empty

            var lines = csvData.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2) return "{}"; // No data to process

            var headers = ParseCsvLine(lines[0]); // Extract headers
            var jsonList = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var values = ParseCsvLine(lines[i]); // Extract values
                var jsonObject = new Dictionary<string, string>();

                for (int j = 0; j < headers.Count; j++)
                {
                    jsonObject[headers[j]] = j < values.Count ? values[j].Trim() : "";
                }

                jsonList.Add(jsonObject);
            }

            // If only one row, return as a single JSON object
            if (jsonList.Count == 1)
                return JsonConvert.SerializeObject(jsonList[0], Formatting.Indented);

            // If multiple rows, return as a JSON array
            return JsonConvert.SerializeObject(jsonList, Formatting.Indented);
        }

        private static List<string> ParseCsvLine(string line)
        {
            var values = new List<string>();
            var matches = Regex.Matches(line, "(\"[^\"]*\"|[^,]+)");

            foreach (Match match in matches)
            {
                string value = match.Value.Trim();
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2); // Remove surrounding quotes
                }
                values.Add(value);
            }

            return values;
        }
    }
}
