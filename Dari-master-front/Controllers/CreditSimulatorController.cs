using dari_frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace dari_frontend.Controllers
{
    public class CreditSimulatorController : Controller
    {
        private HttpClient httpClient;

        public CreditSimulatorController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8081");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)Session["AccessToken"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("user1:password")));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ViewResult> Index()
        {
            var httpResponse = await httpClient.GetAsync("/mortgage/get-banks");
            IEnumerable<BankModel> banks = await httpResponse.EnsureSuccessStatusCode().Content.ReadAsAsync<IEnumerable<BankModel>>();
            return View(banks);
        }

        public async Task<ViewResult> Calculate(string id, CalculateFormModel calculateForm)
        {
            if (ModelState.IsValid)
            {
                calculateForm.Result = await httpClient.GetStringAsync("/mortgage/calculate-mortgage?bankId=" + id + "&loanAmount=" + calculateForm.LoanAmount + "&period=" + calculateForm.Period);
            }
            return View(calculateForm);
        }

        public ViewResult AddOffer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> AddOffer(BankModel bank)
        {
            var httpResponse = await httpClient.PostAsJsonAsync<BankModel>("/mortgage/add-bank", bank);
            httpResponse.EnsureSuccessStatusCode();
            return RedirectToRoute("/BackOfficeHome");
        }

        public async Task<ViewResult> ClientChat()
        {
            Session["ChatId"] = await httpClient.GetStringAsync("/mortgage/assign-session/expert");
            var httpResponse1 = await httpClient.GetAsync("/mortgage/messages/expert");
            ViewBag.Messages = await httpResponse1.EnsureSuccessStatusCode().Content.ReadAsAsync<IEnumerable<MessageModel>>();
            return View();
        }

        public async Task<ViewResult> ExpertChat()
        {
            Session["ChatId"] = await httpClient.GetStringAsync("/mortgage/assign-session/client");
            var httpResponse1 = await httpClient.GetAsync("/mortgage/messages/client");
            ViewBag.Messages = await httpResponse1.EnsureSuccessStatusCode().Content.ReadAsAsync<IEnumerable<MessageModel>>();
            return View();
        }
    }
}