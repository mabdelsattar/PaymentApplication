using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment.Business.Model;

namespace Payment.Business.Services
{
    public interface IPaymentService
    {
        Task<PaymentInfoResponse> DoTransaction(string baseUrl, PaymentInfoRequest paymentInfoRequest);
    }
}
