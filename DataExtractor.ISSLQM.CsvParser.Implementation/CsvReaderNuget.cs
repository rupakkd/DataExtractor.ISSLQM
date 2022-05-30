using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CuttingEdge.Conditions;
using DataExtractor.ISSLQM.CsvParser.Interface;

namespace DataExtractor.ISSLQM.CsvParser.Implementation
{
    public class CsvReaderNuget : BaseCsvOperations, ICsvReader
    {
        #region ICsvReader Implementaion
        public List<T> ReadFromFile<T>(string path, string newLineChar = null, string[] delimeters = null) where T : class, new()
        {
            Helper.ValidatePath(path);

            return ReadFile<T>(path, config: Helper.GetConfiguration(newLineChar, delimeters));
        }

        public List<T> ReadFromCsv<T>(string value, string newLineChar = null, string[] delimeters = null) where T : class, new()
        {
            Condition.Requires(value, nameof(value)).IsNotNullOrWhiteSpace();

            using (var reader = new StringReader(value))
            {
                return ExtractRecords<T>(reader, config: Helper.GetConfiguration(newLineChar, delimeters));
            }
        }
        #endregion ICsvReader Implementaion

        #region Private Methods
        private List<T> ExtractRecords<T>(TextReader reader, CsvConfiguration config) where T : class, new()
        {
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<T>();

                return records.ToList();
            }
        }

        private List<T> ReadFile<T>(string path, CsvConfiguration config, int skipLines = 0) where T : class, new()
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    for (var i = 0; i < skipLines; i++)
                    {
                        if (reader.EndOfStream) skipLines = -1;
                        reader.ReadLine();
                    }

                    return ExtractRecords<T>(reader, config);
                }
            }
            catch (HeaderValidationException)
            {
                if (skipLines == -1) throw;

                return ReadFile<T>(path, config, ++skipLines);
            }
        }
        #endregion Private Methods
    }
}
