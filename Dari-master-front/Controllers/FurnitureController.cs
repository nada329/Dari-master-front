using Dari_master_front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Dari_master_front.Controllers
{
    public class FurnitureController : Controller
    {
        private HttpClient httpClient;
        private String BaseAddress;
        public FurnitureController()
        {
            httpClient = new HttpClient();
            BaseAddress = "http://localhost:8081";
            httpClient.BaseAddress = new Uri(BaseAddress);
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)Session["AccessToken"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("user1:password")));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Furniture
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

        // GET: Furniture/Details/5
        public ActionResult Details(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture/" + id).Result;
            if (tokenRepsonse.IsSuccessStatusCode)
            {
                var props = tokenRepsonse.Content.ReadAsAsync<FurnitureModel>().Result;
                return View(props);
            }
            return View(new List<FurnitureModel>());
        }

        // GET: Furniture/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Furniture/Create
        [HttpPost]
        public ActionResult Create(FurnitureModel p)
        {
            try
            {
                var APIResponse = httpClient.PostAsJsonAsync<FurnitureModel>("http://localhost:8081/api/furniture", p).ContinueWith(postTask => postTask.Result);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Furniture/Edit/5
        public ActionResult Edit(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture/" + id).Result;

            var props = tokenRepsonse.Content.ReadAsAsync<FurnitureModel>().Result;
            return View(props);
        }

        // POST: Furniture/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, FurnitureModel p)
        {
            try
            {
                var APIResponse = httpClient.PutAsJsonAsync("http://localhost:8081/api/furniture/" + id,
                                                            p).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Furniture/Delete/5
        public ActionResult Delete(int id)
        {
            var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture/" + id).Result;

            var props = tokenRepsonse.Content.ReadAsAsync<FurnitureModel>().Result;
            return View(props);
        }

        // POST: Furniture/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var APIResponse = httpClient.DeleteAsync("http://localhost:8081/api/furniture/" + id).ContinueWith(postTask => postTask.Result);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //////////////////////
        public ActionResult Index1()
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

// GET: Furniture/Details/5
public ActionResult Details1(int id)
{
    var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture/" + id).Result;
    if (tokenRepsonse.IsSuccessStatusCode)
    {
        var props = tokenRepsonse.Content.ReadAsAsync<FurnitureModel>().Result;
        return View(props);
    }
    return View(new List<FurnitureModel>());
}

// GET: Furniture/Create
public ActionResult Create1()
{
    return View();
}

// POST: Furniture/Create
[HttpPost]
public ActionResult Create1(FurnitureModel p)
{
    try
    {
        var APIResponse = httpClient.PostAsJsonAsync<FurnitureModel>("http://localhost:8081/api/furniture", p).ContinueWith(postTask => postTask.Result);
        return RedirectToAction("Index1");
    }
    catch
    {
        return View();
    }
}

// GET: Furniture/Edit/5
public ActionResult Edit1(int id)
{
    var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture/" + id).Result;

    var props = tokenRepsonse.Content.ReadAsAsync<FurnitureModel>().Result;
    return View(props);
}

// POST: Furniture/Edit/5
[HttpPost]
public ActionResult Edit1(int id, FormCollection collection, FurnitureModel p)
{
    try
    {
        var APIResponse = httpClient.PutAsJsonAsync("http://localhost:8081/api/furniture/" + id,
                                                    p).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
        return RedirectToAction("Index1");
    }
    catch
    {
        return View();
    }
}

// GET: Furniture/Delete/5
public ActionResult Delete1(int id)
{
    var tokenRepsonse = httpClient.GetAsync("http://localhost:8081/api/furniture/" + id).Result;

    var props = tokenRepsonse.Content.ReadAsAsync<FurnitureModel>().Result;
    return View(props);
}

// POST: Furniture/Delete/5
[HttpPost]
public ActionResult Delete1(int id, FormCollection collection)
{
    try
    {
        var APIResponse = httpClient.DeleteAsync("http://localhost:8081/api/furniture/" + id).ContinueWith(postTask => postTask.Result);
        return RedirectToAction("Index1");
    }
    catch
    {
        return View();
    }
}
    }
}
