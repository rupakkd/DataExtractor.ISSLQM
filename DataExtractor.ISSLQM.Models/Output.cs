using System;
using System.ComponentModel;

namespace DataExtractor.ISSLQM.Models
{
    public class Output
    {
        #region Properties
        public string ISIN { get; set; }
        public string CFICode { get; set; }
        public string Venue { get; set; }
        [Description("Contract Size")]
        public int ContractSize { get; set; }
        #endregion Properties

        #region Constructors
        public Output() { }

        public Output(string isin, string cfiCode, string venue, string contractSize) : this()
        {
            ISIN = isin;
            CFICode = cfiCode;
            Venue = venue;

            Double.TryParse(contractSize, out var size); // Assuming integer as per sample output csv
            ContractSize = Convert.ToInt32(size);
        }
        #endregion Constructors
    }
}
