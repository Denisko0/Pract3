using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "transactions.csv";
        string dateFormat = "yyyy-MM-dd";

        Func<string, DateTime> getDate = s => DateTime.ParseExact(s, dateFormat, CultureInfo.InvariantCulture);
        Func<string, double> getAmount = s => double.Parse(s, CultureInfo.InvariantCulture);

        Action<DateTime, double> displayTotal = (date, total) => Console.WriteLine("{0}: {1}", date.ToString(dateFormat), total);

        int batchSize = 10;
        int count = 0;
        var batch = new List<string>();

        using (var reader = new StreamReader(filePath))
        {
            var totals = new Dictionary<DateTime, double>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var date = getDate(values[0]);
                var amount = getAmount(values[1]);

                if (totals.ContainsKey(date))
                {
                    totals[date] += amount;
                }
                else
                {
                    totals[date] = amount;
                }

                batch.Add(line);
                count++;

                if (count == batchSize)
                {
                    SaveBatch(batch, filePath);
                    batch.Clear();
                    count = 0;
                }
            }

            if (count > 0)
            {
                SaveBatch(batch, filePath);
            }

            foreach (var kvp in totals)
            {
                displayTotal(kvp.Key, kvp.Value);
            }
        }
    }

    static void SaveBatch(List<string> batch, string filePath)
    {
        using (var writer = new StreamWriter(filePath, true))
        {
            foreach (var line in batch)
            {
                writer.WriteLine(line);
            }
        }
    }
}