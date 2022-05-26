using System.Collections.Generic;
using System.IO;
using CsvHelper;
using DataExtractor.ISSLQM.CsvParser.Interface;

namespace DataExtractor.ISSLQM.CsvParser.Implementation
{
    public class CsvWriterNuget : BaseCsvOperations, ICsvWriter
    {
        #region ICsvWriter Implementaion
        public void Write<T>(string path, IEnumerable<T> data) where T : class, new()
        {
            Helper.ValidatePath(path, false);

            using (var writer = new StreamWriter(path))
            {
                using (var csv = new CsvWriter(writer, Helper.GetConfiguration()))
                {
                    csv.WriteRecords(data);
                }
            }
        }
        #endregion ICsvWriter Implementaion
    }
}
