using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentWebApplication.Models
{
    public class PaymentInfoCommand
    {
        public string EncyptedBody { get; set; }
        public string Key { get; set; }

    }
}
