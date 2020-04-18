using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment.Business.Model;

namespace Payment.Business.Services
{
    public class PaymentService: IPaymentService
    {
        public async Task<PaymentInfoResponse> DoTransaction(string baseUrl, PaymentInfoRequest paymentInfoRequest)
        {
            paymentInfoRequest.SystemTraceNr = RandomNumber(1000, 10000);
            paymentInfoRequest.ProcessingCode = RandomNumber(999,9999).ToString();

            PaymentInfoCommand paymentInfoCommand = new PaymentInfoCommand();
            using (var httpClient = new HttpClient())
            {
                using (var keyResponse = await httpClient.GetAsync(baseUrl + "/GetKey"))
                {
                    string apiGetKeyResponse = await keyResponse.Content.ReadAsStringAsync();
                    string key = JObject.Parse(apiGetKeyResponse)["GetKeyResult"].ToString();
                    paymentInfoCommand.Key = key;
                    paymentInfoCommand.EncyptedBody = new EncryptionService().Encrypt(JsonConvert.SerializeObject(paymentInfoRequest), key);

                    StringContent content = new StringContent(JsonConvert.SerializeObject(paymentInfoCommand), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(baseUrl + "/Pay", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        PaymentInfoResponse paymentInfoResponse = JsonConvert.DeserializeObject<PaymentInfoResponse>(apiResponse);
                        return paymentInfoResponse;
                    }
                }

            }
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
