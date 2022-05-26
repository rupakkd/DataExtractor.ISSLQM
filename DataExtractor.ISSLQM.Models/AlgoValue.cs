namespace DataExtractor.ISSLQM.Models
{
    public class AlgoValue
    {
        #region Properties
        public string Field { get; set; }
        public string Value { get; set; }
        public string X { get; set; }
        #endregion Properties

        #region Methods
        public override string ToString()
        {
            return $"{Field}:{Value}";
        }
        #endregion Methods
    }
}
