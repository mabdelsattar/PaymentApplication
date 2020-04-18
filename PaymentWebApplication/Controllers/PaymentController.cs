using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment.Business.Model;
using PaymentWebApplication.Models;
using PaymentWebApplication.ViewModel;
using AutoMapper;
using Payment.Business.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PaymentWebApplication.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private IConfiguration _configuration;
        private IEncryptionService _encryptionService;
        private IPaymentService _paymentService;
        public PaymentController(IConfiguration configuration,IEncryptionService encryptionService, IPaymentService paymentService)
        {
            _configuration = configuration;
            _encryptionService = encryptionService;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            List<SelectListItem> currencyList = new List<SelectListItem>();

            currencyList.Add(new SelectListItem("EGP", "1"));
            currencyList.Add(new SelectListItem("SAR", "2"));
            currencyList.Add(new SelectListItem("USD", "3"));            
            ViewBag.currenyList = currencyList;

            return View(new PaymentInfoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind] PaymentInfoViewModel paymentInfo)
        {
            if (ModelState.IsValid)
            {
                PaymentInfoRequest paymentInfoRequest =new PaymentInfoRequest() { 
                AmountTrxn = paymentInfo.AmountTrxn,
                CardHolder = paymentInfo.CardHolder,
                CurrencyCode = paymentInfo.CurrencyCode,
                CardNo = paymentInfo.CardNo,
                FunctionCode = paymentInfo.FunctionCode
                };
                PaymentInfoResponse paymentInfoResponse = await _paymentService.DoTransaction(_configuration["PaymentServiceBaseUrl"].ToString(),paymentInfoRequest);
                return RedirectToAction("SuccessPayment", new { code = paymentInfoResponse.ApprovalCode });
            }
            else 
            {
                //TODO add view error message
                return View();
            }
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
