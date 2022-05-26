using System;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace DataExtractor.ISSLQM.CsvParser.Implementation.Tests.Models
{
    internal enum eCurrency
    {
        GBP,
        EUR,
        PLN
    }

    internal class TestModel
    {
        #region Properties
        [Optional]
        [BooleanTrueValues("TRUE")]
        [BooleanFalseValues("FALSE")]
        public bool? IsMultiFill { get; set; }
        [Optional]
        [BooleanTrueValues("Y")]
        [BooleanFalseValues("N")]
        public bool FlowType { get; set; }

        [Optional]
        [Format("HH:mm:ss")]
        [Name("FirstFillTime_TradeTime")]
        [DateTimeStyles(DateTimeStyles.AssumeLocal)]
        public DateTime? FirstFillTimeTradeTime { get; set; }
        public int? OrderRef { get; set; }
        public double? Price { get; set; }
        public eCurrency Currency { get; set; }
        #endregion Properties
    }
}
