using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using DataExtractor.ISSLQM.CsvParser.Interface;
using DataExtractor.ISSLQM.IoC;
using DataExtractor.ISSLQM.Models;

namespace DataExtractor.ISSLQM.ConsoleApp
{
    internal class Program
    {
        #region Declarations
        private static readonly ICsvReader _csvReader;
        private static readonly ICsvWriter _csvWriter;
        #endregion Declarations

        #region Constructor
        static Program()
        {
            _csvReader = DependencyResolver.Instance.GetInstance<ICsvReader>();
            _csvWriter = DependencyResolver.Instance.GetInstance<ICsvWriter>();
        }
        #endregion Constructor

        #region Methods
        static void Main(string[] args)
        {
            try
            {
                var assembly = Assembly.GetEntryAssembly().GetName();
                Console.WriteLine(
                    $"******************************************************\r\n" +
                    $"************ {assembly.Name} v{assembly.Version} ***********\r\n" +
                    $"******************************************************\r\n\r\n");

                var path = args.ElementAtOrDefault(0);

#if DEBUG
                if (String.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    path = @"Resources\DataExtractor_Example_Input.csv";
                }
#endif

                var records = _csvReader.ReadFromFile<Input>(path);

                Input.AlgoParser += _csvReader.ReadFromCsv<AlgoValue>;
                var output = records.Select(d => new Output(d.ISIN, d.CfiCode, d.Venue, d.AlgoValues.SingleOrDefault(a => a.Field == "PriceMultiplier")?.Value));

                var outputPath = args.ElementAtOrDefault(1) ?? $@"__Output\DataExtractor_Output_{DateTime.UtcNow.ToString("yyyyMMss_HHmmss")}.csv";
                _csvWriter.Write(outputPath, output.ToList());

#if DEBUG
                var outputSamplePath = @"Resources\DataExtractor_Example_Output.csv";
                Console.WriteLine($"{(File.ReadAllText(outputPath) == File.ReadAllText(outputSamplePath) ? "MATCHes" : "DOES NOT MATCH")} with sample");
#endif

                Console.WriteLine("Opening output file..");
                Process.Start("notepad", outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}\r\n{ex.StackTrace}");
            }

            Console.ReadKey();
        }
        #endregion Methods
    }
}
