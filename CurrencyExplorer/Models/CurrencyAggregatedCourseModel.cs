using System.ComponentModel.DataAnnotations;

namespace CurrencyExplorer.Models
{
    public class CurrencyAggregatedCourseModel
    {
        public int AggregateInterval { get; set; }
        public string ValuePairName { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double MaxValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double MinValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double FirstValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double LastValue { get; set; }
    }
}