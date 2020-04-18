using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentWebApplication.Models
{
    public class PaymentInfo
    {
        public string ProcessingCode { get; set; }
        public int? SystemTraceNr { get; set; }
        public int? FunctionCode { get; set; }
        public string CardNo { get; set; }
        public string CardHolder { get; set; }
        public double AmountTrxn { get; set; }
        public int? CurrencyCode { get; set; }
    }
}
