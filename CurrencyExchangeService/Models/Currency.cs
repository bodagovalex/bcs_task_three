using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchangeService.Models
{
    public class Currency
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public bool isDeleted { get; set; }

        public Currency()
        {
            isDeleted = false;
        }
    }
}
