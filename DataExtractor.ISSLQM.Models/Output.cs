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
        public string ContractSize { get; set; }
        #endregion Properties

        #region Constructors
        public Output() { }

        public Output(string isin, string cfiCode, string venue, string contractSize) : this()
        {
            ISIN = isin;
            CFICode = cfiCode;
            Venue = venue;
            ContractSize = contractSize;
        }
        #endregion Constructors
    }
}
