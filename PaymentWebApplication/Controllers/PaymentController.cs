using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentWebApplication.Models;

namespace PaymentWebApplication.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private IConfiguration _configuration;
        public PaymentController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new PaymentInfo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind] PaymentInfo paymentInfo)
        {
            PaymentInfoResponse paymentInfoResponse;
            PaymentInfoCommand paymentInfoCommand = new PaymentInfoCommand();
            using (var httpClient = new HttpClient())
            {
                using (var keyResponse = await httpClient.GetAsync(_configuration["PaymentServiceBaseUrl"] + "/GetKey"))
                {
                    string apiGetKeyResponse = await keyResponse.Content.ReadAsStringAsync();
                    string key = JObject.Parse(apiGetKeyResponse)["GetKeyResult"].ToString();
                    paymentInfoCommand.Key = key;
                    paymentInfoCommand.EncyptedBody = new AesOperation().Encrypt(JsonConvert.SerializeObject(paymentInfo),key);
                    
                    StringContent content = new StringContent(JsonConvert.SerializeObject(paymentInfoCommand), Encoding.UTF8, "application/json");
                    
                    using (var response = await httpClient.PostAsync(_configuration["PaymentServiceBaseUrl"] + "/Pay", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        paymentInfoResponse = JsonConvert.DeserializeObject<PaymentInfoResponse>(apiResponse);
                    }
                }
            }
            return RedirectToAction("SuccessPayment", new { code = paymentInfoResponse.ApprovalCode });
        }

        public IActionResult SuccessPayment(string code)
        {
            ViewData["Message"] = "Your Transaction done successfully with code: "+code;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
    }
}
