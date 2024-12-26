using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileNameParser
{
    public class FileNameParserService
    {
        public static async Task<string> ParseFileNameAsync(string fileName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Default culture if not found in fileName is Nigeria
                    CultureInfo culture = new CultureInfo("en-NG");

                    // Extract the culture info from the filename, assuming it's the part like "en-US"
                    var cultureMatch = Regex.Match(fileName, @"\b([a-z]{2}-[A-Z]{2})\b");
                    if (cultureMatch.Success)
                    {
                        try
                        {
                            culture = new CultureInfo(cultureMatch.Value);
                        }
                        catch
                        {
                            culture = new CultureInfo("en-NG");
                        }
                    }

                    // Handle Format(NOW, "yyyy-MM-dd")
                    fileName = Regex.Replace(fileName, @"Format\((NOW|TODAY|YESTERDAY),\s*""([^""]+)""\)", match =>
                    {
                        string keyword = match.Groups[1].Value;
                        string format = match.Groups[2].Value;

                        DateTime baseDate = keyword switch
                        {
                            "NOW" => DateTime.UtcNow,
                            "TODAY" => DateTime.UtcNow.Date,
                            "YESTERDAY" => DateTime.UtcNow.AddDays(-1).Date,
                            _ => throw new InvalidOperationException("Invalid keyword in Format.")
                        };

                        return baseDate.ToString(format, culture);
                    });

                    // Replace NOW, YESTERDAY, TODAY keywords directly in the filename
                    fileName = Regex.Replace(fileName, @"\bTODAY\b", DateTime.UtcNow.Date.ToString("yyyy-MM-dd", culture));
                    fileName = Regex.Replace(fileName, @"\bYESTERDAY\b", DateTime.UtcNow.AddDays(-1).Date.ToString("yyyy-MM-dd", culture));
                    fileName = Regex.Replace(fileName, @"\bNOW\b", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", culture));

                    // Handle relative offsets like NOW-1d, NOW+2h
                    fileName = Regex.Replace(fileName, @"NOW([+-]\d+)([d|h|m|s])", match =>
                    {
                        int value = int.Parse(match.Groups[1].Value);
                        string unit = match.Groups[2].Value;

                        DateTime resultDate = unit switch
                        {
                            "d" => DateTime.UtcNow.AddDays(value),
                            "h" => DateTime.UtcNow.AddHours(value),
                            "m" => DateTime.UtcNow.AddMinutes(value),
                            "s" => DateTime.UtcNow.AddSeconds(value),
                            _ => throw new InvalidOperationException("Invalid time unit.")
                        };

                        return resultDate.ToString("yyyy-MM-dd HH:mm:ss", culture);
                    });

                    return fileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing file name: {ex.Message}");
                    return fileName; // Return the original fileName if parsing fails
                }
            });
        }
    }
}
