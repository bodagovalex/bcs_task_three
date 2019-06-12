using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyExchangeService.Models
{
    public class CurrencyPair
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [ForeignKey("BaseCurrency")]
        public int? BaseCurrencyID { get; set; }
        public virtual Currency BaseCurrency { get; set; }

        [ForeignKey("ChildCurrency")]
        public int? ChildCurrencyID { get; set; }
        public virtual Currency ChildCurrency { get; set; }

        public bool isDeleted { get; set; }

        public CurrencyPair()
        {
            isDeleted = false;
        }

        public  string PairCode
        {
            get { return $"{BaseCurrency.Code}_{ChildCurrency.Code}"; }
        }
    }
}
