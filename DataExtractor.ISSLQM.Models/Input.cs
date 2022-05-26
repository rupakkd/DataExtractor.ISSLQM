using System;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace DataExtractor.ISSLQM.Models
{
    public class Input
    {
        #region Declarations
        public delegate List<T> AlgoReader<T>(string value, string newLineChar = null, string[] delimeters = null) where T : class, new();
        public static AlgoReader<AlgoValue> AlgoParser;
        #endregion Declarations

        #region Properties
        [BooleanTrueValues("TRUE")]
        [BooleanFalseValues("FALSE")]
        public bool? IsMultiFill { get; set; }
        public string ISIN { get; set; }
        public string Currency { get; set; }
        public string Venue { get; set; }
        public int? OrderRef { get; set; }
        [Name("PMID")]
        public string PMID { get; set; }
        [Name("CFICode")]
        public string CfiCode { get; set; }
        public string ParticipantCode { get; set; }
        [Name("PublicTradeID")]
        public string TraderId { get; set; }
        public string CounterPartyCode { get; set; }
        [Format("HH:mm:ss")]
        [DateTimeStyles(DateTimeStyles.AssumeLocal)]
        public DateTime? DecisionTime { get; set; }
        [Format("HH:mm:ss")]
        [DateTimeStyles(DateTimeStyles.AssumeLocal)]
        [Name("ArrivalTime_QuoteTime")]
        public DateTime? ArrivalTimeQuoteTime { get; set; }
        [Format("HH:mm:ss")]
        [DateTimeStyles(DateTimeStyles.AssumeLocal)]
        [Name("FirstFillTime_TradeTime")]
        public DateTime? FirstFillTimeTradeTime { get; set; }
        [Format("HH:mm:ss")]
        [DateTimeStyles(DateTimeStyles.AssumeLocal)]
        public DateTime? LastFillTime { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string Side { get; set; }
        public string TradeFlag { get; set; }
        public string SettlementDate { get; set; }
        [Name("PublicTradeID")]
        public string PublicTradeId { get; set; }
        public string UserDefinedFilter { get; set; }
        [Name("TradingNetworkID")]
        public string TradingNetworkId { get; set; }
        public string SettlementPeriod { get; set; }
        public string MarketOrderId { get; set; }
        public string ParticipationRate { get; set; }
        public string BenchmarkVenues { get; set; }
        public string BenchmarkType { get; set; }
        public string FlowType { get; set; }
        [Name("BasketID")]
        public string BasketId { get; set; }
        public string MessageType { get; set; }
        public string ParentOrderRef { get; set; }
        public string ExecutionType { get; set; }
        public double? LimitPrice { get; set; }
        public string Urgency { get; set; }
        public string AlgoName { get; set; }
        public string AlgoParams { get; set; }
        public string Index { get; set; }
        public string Sector { get; set; }

        private List<AlgoValue> _algoValues;
        public List<AlgoValue> AlgoValues { get { if (_algoValues == null) _algoValues = GetAlgoValues(AlgoParams); return _algoValues; } }
        #endregion Propertis

        #region Methods
        private List<AlgoValue> GetAlgoValues(string algoParams)
        {
            var header = $"{nameof(AlgoValue.Field)},{nameof(AlgoValue.Value)},{nameof(AlgoValue.X)}";
            var value = $"{header};{algoParams.Replace(":", ",").Replace("|", ",")}";

            return AlgoParser?.Invoke(value, ";");
        }
        #endregion Methods
    }
}
