using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Business.Model
{
    public class PaymentInfoCommand
    {
        public string EncyptedBody { get; set; }
        public string Key { get; set; }

    }
}
