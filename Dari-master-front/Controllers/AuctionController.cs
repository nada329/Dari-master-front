using Dari_master_front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dari_master_front.Controllers
{
    public class AuctionController : Controller
    {
        private HttpClient httpClient;
        private String BaseAddress;
        public AuctionController()
        {
            httpClient = new HttpClient();
            BaseAddress = "http://localhost:8081";
            httpClient.BaseAddress = new Uri(BaseAddress);
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)Session["AccessToken"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("user1:password")));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Auction
        public ActionResult Index()
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture").Result;

            if (tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<IEnumerable<FurnitureModel>>().Result;
                Console.WriteLine(props);
                return View(props);
            }
            return View(new List<FurnitureModel>());
        }
        //GET: Auction1
        public ActionResult Index1()
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/Auction").Result;

            if (tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<IEnumerable<AuctionModel>>().Result;
                Console.WriteLine(props);
                return View(props);
            }
            return View(new List<AuctionModel>());
        }
        // GET: Auction/Details/5
        public ActionResult Details(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/OneAuction/" + id).Result;
            if (tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<AuctionModel>().Result;
                return View(props);
            }
            return View(new List<AuctionModel>());
        }

        // GET: Auction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auction/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection, int id,AuctionModel auction)
        {
            try
            {
               
                var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture/" + id).Result;
                if (tokenRepsonse.IsSuccessStatusCode)
                {
                  var props = tokenRepsonse.Content.ReadAsAsync<FurnitureModel>().Result;
                    auction.startingprice = props.price;
                }
                
                var APIResponse = httpClient.PostAsJsonAsync<AuctionModel>("http://localhost:8081/api/Auction/"+id, auction).ContinueWith(postTask => postTask.Result);
                return RedirectToAction("Index1");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Auction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
