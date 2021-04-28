using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using dari_frontend.Models;
namespace Dari_master_front.Controllers
{
    public class PropertyController : Controller
    {
        private HttpClient httpClient;
        private String BaseAddress;
        public PropertyController()
        {
            httpClient = new HttpClient();
            BaseAddress = "http://localhost:8081";
            httpClient.BaseAddress = new Uri(BaseAddress);
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)Session["AccessToken"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("user1:password")));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Property1
        public ActionResult Index()
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/prop").Result;
           
            if(tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<IEnumerable<PropertyModel>>().Result;
                Console.WriteLine(props);
                return View(props);
            }
            return View(new List<PropertyModel>());
        }
        public ActionResult Index1()
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/prop").Result;

            if (tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<IEnumerable<PropertyModel>>().Result;
                Console.WriteLine(props);
                return View(props);
            }
            return View(new List<PropertyModel>());
        }

        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/prop/"+id).Result;
            if (tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<PropertyModel>().Result;
                return View(props);
            }
            return View(new List<PropertyModel>());
        }
        public ActionResult Details1(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/prop/" + id).Result;
            if (tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<PropertyModel>().Result;
                return View(props);
            }
            return View(new List<PropertyModel>());
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        public async Task<ActionResult> Create(PropertyModel p)
        {
            try
            {
                var APIResponse = httpClient.PostAsJsonAsync<PropertyModel>("http://localhost:8081/api/prop", p).ContinueWith(postTask => postTask.Result);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Create1()
        {
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        public async Task<ActionResult> Create1(PropertyModel p)
        {
            try
            {
                var APIResponse = httpClient.PostAsJsonAsync<PropertyModel>("http://localhost:8081/api/prop", p).ContinueWith(postTask => postTask.Result);
                return RedirectToAction("Index1");
            }
            catch
            {
                return View();
            }
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/prop/" + id).Result;
            
                var props = tokenRepsonse.Content.ReadAsAsync<PropertyModel>().Result;
                return View(props);
            
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, PropertyModel p)
        {
            try
            {
                var APIResponse = httpClient.PutAsJsonAsync("http://localhost:8081/api/prop/" + id,
                                                            p).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index1");
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Edit1(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/prop/" + id).Result;

            var props = tokenRepsonse.Content.ReadAsAsync<PropertyModel>().Result;
            return View(props);

        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit1(int id, FormCollection collection, PropertyModel p)
        {
            try
            {
                var APIResponse = httpClient.PutAsJsonAsync("http://localhost:8081/api/prop/" + id,
                                                            p).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Property/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var APIResponse = httpClient.DeleteAsync("http://localhost:8081/api/prop/"+id).ContinueWith(postTask => postTask.Result);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete1(int id)
        {
            return View();
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete1(int id, FormCollection collection)
        {
            try
            {
                var APIResponse = httpClient.DeleteAsync("http://localhost:8081/api/prop/" + id).ContinueWith(postTask => postTask.Result);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
