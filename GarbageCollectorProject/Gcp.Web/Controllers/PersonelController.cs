using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace Gcp.Web.Controllers
{
    public class PersonelController : Controller
    {
		readonly HttpClient _client;
        string _url = "http://localhost:53723/api/Personel";
        public PersonelController()
        {
            _client = new HttpClient { BaseAddress = new Uri(_url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        // GET: Personel
        public ActionResult Index()
        {
           return View();
        }

        public ActionResult Create()
        {
            return View(new Personel());
        }

	    public async Task<ActionResult> Count()
	    {
			var responseMessage = await _client.GetAsync(_url);
			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
		    return Json(personel.Count(), JsonRequestBehavior.AllowGet);
	    }
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(Personel p)
        {
            var jsonString = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync(_url,content);
            return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
        }
        //GET Method
        public async Task<ActionResult> Edit(int id)
        {
            var responseMessage = await _client.GetAsync($"{_url}/{id}");
            if (!responseMessage.IsSuccessStatusCode) return View("Error");

            var responseData = responseMessage.Content.ReadAsStringAsync().Result;

            var personel = JsonConvert.DeserializeObject<Personel>(responseData);
			return Json(personel, JsonRequestBehavior.AllowGet);

        }
        //PUT (Update) Method
        [HttpPost]
        public async Task<ActionResult> Edit(Personel p)
        {
			var jsonString = JsonConvert.SerializeObject(p);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{p.PersonelID}", content);
	        return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
        }
        public async Task<ActionResult> GetPersonelHtml()
        {
            var responseMessage = await _client.GetAsync(_url);

            if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
            return View(personel.ToList());
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var responseMessage = await _client.DeleteAsync(_url + "/" + id);
            return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
        }
    }
}