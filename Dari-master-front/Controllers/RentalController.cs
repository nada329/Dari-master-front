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
    public class RentalController : Controller
    {
        private HttpClient httpClient;

        public RentalController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8081");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)Session["AccessToken"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("owner:password")));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ViewResult> ClientRentals()
        {
            var httpResponse1 = await httpClient.GetAsync("/rentals/client");
            IEnumerable<RentalModel> rentals = await httpResponse1.EnsureSuccessStatusCode().Content.ReadAsAsync<IEnumerable<RentalModel>>();
            return View(rentals);
        }

        public async Task<ViewResult> OwnerRentals()
        {
            var httpResponse1 = await httpClient.GetAsync("/rentals/owner");
            IEnumerable<RentalModel> rentals = await httpResponse1.EnsureSuccessStatusCode().Content.ReadAsAsync<IEnumerable<RentalModel>>();
            return View(rentals);
        }

        public ViewResult Rent(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Rent(string id, RentFormModel rentForm)
        {
            if (ModelState.IsValid)
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(new StringContent(id), "propertyId");
                content.Add(new StringContent(rentForm.ExpectedEndDate.ToString()), "expectedEndDate");
                content.Add(new StringContent(rentForm.CardNumber.ToString()), "cardNumber");
                content.Add(new StringContent(rentForm.ExpMonth.ToString()), "expMonth");
                content.Add(new StringContent(rentForm.ExpYear.ToString()), "expYear");
                content.Add(new StringContent(rentForm.CCV.ToString()), "ccv");
                var httpResponse = await httpClient.PostAsync("/rentals/rent", content);
                httpResponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ViewResult ExtendRent(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExtendRent(string id, RentFormModel rentForm)
        {
            if (ModelState.IsValid)
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(new StringContent(id), "rentalId");
                content.Add(new StringContent(rentForm.ExpectedEndDate.ToString()), "expectedEndDate");
                content.Add(new StringContent(rentForm.CardNumber.ToString()), "cardNumber");
                content.Add(new StringContent(rentForm.ExpMonth.ToString()), "expMonth");
                content.Add(new StringContent(rentForm.ExpYear.ToString()), "expYear");
                content.Add(new StringContent(rentForm.CCV.ToString()), "ccv");
                var httpResponse = await httpClient.PostAsync("/rentals/rent", content);
                httpResponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ViewResult Grant(string rentalId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Grant(string rentalId, RentalGrantModel rentalGrant)
        {
            if (ModelState.IsValid)
            {
                var httpResponse = await httpClient.PostAsJsonAsync<RentalGrantModel>("/rentals/deposit-grant/" + rentalId, rentalGrant);
                httpResponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Working (<> View)
        public async Task<ViewResult> Grants(string id) // id: Rental Id
        {
            var httpResponse = await httpClient.GetAsync("/rentals/grants/" + id);
            var grants = await httpResponse.EnsureSuccessStatusCode().Content.ReadAsAsync<IEnumerable<RentalGrantModel>>();
            return View(grants);
        }

        public async Task<FilePathResult> GrantFile(string grantId)
        {
            String path = await httpClient.GetStringAsync("/rentals/grant/" + grantId);
            // return File();
            return null;
        }

        public async Task<ViewResult> Metrics()
        {
            var httpResponse1 = await httpClient.GetAsync("/rentals/metrics/regions-by-demand");
            ViewBag.RegionsByDemand = await httpResponse1.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<string, long>>();
            var httpResponse2 = await httpClient.GetAsync("/rentals/metrics/regions-by-price");
            ViewBag.RegionsByAvgPrice = await httpResponse2.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<string, double>>();
            var httpResponse3 = await httpClient.GetAsync("/rentals/metrics/properties-by-acc-revenue");
            ViewBag.PropertiesByAccRevenue = await httpResponse3.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<PropertyModel, double>>();
            var httpResponse4 = await httpClient.GetAsync("/rentals/metrics/properties-by-acc-revenue");
            ViewBag.PropertiesByAccRevenue = await httpResponse3.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<PropertyModel, double>>();
            var httpResponse5 = await httpClient.GetAsync("/rentals/metrics/properties-by-score");
            ViewBag.PropertiesByScore = await httpResponse4.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<PropertyModel, double>>();
            var httpResponse6 = await httpClient.GetAsync("/rentals/metrics/period-by-acc-revenue");
            ViewBag.PeriodByAccRevenue = await httpResponse5.EnsureSuccessStatusCode().Content.ReadAsAsync<Dictionary<string, double>>();
            return View();
        }
    }
}