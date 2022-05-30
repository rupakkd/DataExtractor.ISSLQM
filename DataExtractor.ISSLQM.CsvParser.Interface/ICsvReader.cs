using System;
using System.Collections.Generic;
using CsvHelper;

namespace DataExtractor.ISSLQM.CsvParser.Interface
{
    public interface ICsvReader : IBaseExcelOperations
    {
        #region Method Declarations
        /// <summary>
        /// Reads CSV from file and converts into a list (works with invalid headers)
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="path">File path</param>
        /// <param name="newLineChar">New line characters (default: '\r\n')</param>
        /// <param name="delimeters">Delimeters (default: ',')</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="path"/> is null</exception>
        /// <exception cref="ArgumentException">When <paramref name="path"/> is empty</exception>
        List<T> ReadFromFile<T>(string path, string newLineChar = null, string[] delimeters = null) where T : class, new();

        /// <summary>
        /// Reads CSV and converts into a list (works with valid headers only)
        /// </summary>
        /// <param name="value">CSV</param>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="newLineChar">New line characters (default: '\r\n')</param>
        /// <param name="delimeters">Delimeters (default: ',')</param>
        /// <returns></returns>
        /// <exception cref="HeaderValidationException">When <paramref name="value"/> is empty</exception>
        List<T> ReadFromCsv<T>(string value, string newLineChar = null, string[] delimeters = null) where T : class, new();
        #endregion Method Declarations
    }
}
