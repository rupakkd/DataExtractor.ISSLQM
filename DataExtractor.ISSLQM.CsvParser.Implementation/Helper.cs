using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using CuttingEdge.Conditions;

namespace DataExtractor.ISSLQM.CsvParser.Implementation
{
    internal static class Helper
    {
        #region Methods
        internal static void ValidatePath(string path, bool existingFile = true)
        {
            Condition.Requires(path, nameof(path)).IsNotNullOrWhiteSpace();
            if (existingFile) Condition.Requires(File.Exists(path), nameof(path)).IsTrue();
        }

        internal static CsvConfiguration GetConfiguration(string newLineChar = null, string[] delimeters = null)
        {
            if (String.IsNullOrWhiteSpace(newLineChar)) newLineChar = Environment.NewLine;
            if (delimeters == null || !delimeters.Any()) delimeters = new[] { "," };

            return
                new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    AllowComments = true,
                    IgnoreBlankLines = true,
                    DetectDelimiterValues = delimeters, // TODO Not working
                    NewLine = newLineChar
                };
        }
        #endregion Methods
    }
}
