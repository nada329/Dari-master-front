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
    public class PurchaseController : Controller
    {
        private HttpClient httpClient;

        public PurchaseController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8081");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String) Session["AccessToken"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("owner:password")));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ViewResult Purchase(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Purchase(string id, PurchaseFormModel purchaseForm)
        {
            if (ModelState.IsValid)
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(new StringContent(id), "propertyId");
                content.Add(new StringContent(purchaseForm.CardNumber.ToString()), "cardNumber");
                content.Add(new StringContent(purchaseForm.ExpMonth.ToString()), "expMonth");
                content.Add(new StringContent(purchaseForm.ExpYear.ToString()), "expYear");
                content.Add(new StringContent(purchaseForm.CCV.ToString()), "ccv");
                var httpResponse = await httpClient.PostAsync("/purchases/purchase", content);
                httpResponse.EnsureSuccessStatusCode();
                return View("PurchaseSuccessful");
            }
            return View();
        }

        public async Task<ViewResult> Metrics()
        {
            var httpResponse1 = await httpClient.GetAsync("/purchases/metrics/regions-by-demand");
            ViewBag.RegionsByDemand = await httpResponse1.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<string, long>>();
            var httpResponse2 = await httpClient.GetAsync("/purchases/metrics/regions-by-price");
            ViewBag.RegionsByAvgPrice = await httpResponse2.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<string, double>>();
            return View();
        }
    }
}