using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyExchangeService.Models
{
    public class RateHistory
    {
        [Key]
        public int ID { get; set; }

        public int CurrencyPairID { get; set; }
        [ForeignKey("CurrencyPairID")]
        public virtual CurrencyPair CurrencyPair { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime UpdateDateTime { get; set; }

        public RateHistory()
        {
            UpdateDateTime = DateTime.Now;
        }

        public RateHistory(int p_currencyPairID, double p_price)
        {
            CurrencyPairID = p_currencyPairID;
            //CurrencyPair = p_currencyPair;
            Price = p_price;
            UpdateDateTime = DateTime.Now;
        }

        public int TimeDifference
        {
            get { return (int)(DateTime.Now - this.UpdateDateTime).TotalMinutes; }
        }
    }
}
