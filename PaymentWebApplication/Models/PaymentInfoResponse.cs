using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentWebApplication.Models
{
    public class PaymentInfoResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public string ApprovalCode { get; set; }
        public DateTime DateTime { get; set; }
    }
}
