using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentWebApplication.ViewModel
{
    public class PaymentInfoViewModel
    {
        [Required]
        [Display(Name = "Function Code")]
        public int? FunctionCode { get; set; }
        [Required]
        [Display(Name = "Card No")]
        [MinLength(16)]
        public string CardNo { get; set; }
        [Required]
        [Display(Name = "Card Holder")]
        public string CardHolder { get; set; }
        [Required]
        [Display(Name = "Amount of Transaction")]
        [Range(1,999999999)]
        public double AmountTrxn { get; set; }
        [Required]
        [Display(Name = "Curreny Code")]
        public int? CurrencyCode { get; set; }
    }
}
