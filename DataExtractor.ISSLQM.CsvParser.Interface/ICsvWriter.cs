using System.Collections.Generic;

namespace DataExtractor.ISSLQM.CsvParser.Interface
{
    public interface ICsvWriter : IBaseExcelOperations
    {
        #region Method Declarations
        /// <summary>
        /// Writes data as CSV
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="path">Output path</param>
        /// <param name="data">Data to write</param>
        void Write<T>(string path, IEnumerable<T> data) where T : class, new();
        #endregion Method Declarations
    }
}
