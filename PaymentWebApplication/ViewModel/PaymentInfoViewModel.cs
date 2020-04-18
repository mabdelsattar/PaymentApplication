using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentWebApplication.ViewModel
{
    public class PaymentInfoViewModel
    {
        public int? FunctionCode { get; set; }
        public string CardNo { get; set; }
        public string CardHolder { get; set; }
        public double AmountTrxn { get; set; }
        public int? CurrencyCode { get; set; }
    }
}
