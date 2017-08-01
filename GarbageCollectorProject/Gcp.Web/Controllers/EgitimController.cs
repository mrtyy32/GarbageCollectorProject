using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;

namespace Gcp.Web.Controllers
{
    public class EgitimController : Controller
    {
		readonly HttpClient _client;
		string _url = "http://localhost:53723/api/Egitim";
		public EgitimController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		// GET: Egitim
		public ActionResult Index()
        {
            return View();
        }
		public async Task<ActionResult> forDropDown()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var egitim = JsonConvert.DeserializeObject<List<Egitim>>(responseData);
			return Json(egitim, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> GetEgitimHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var egitim = JsonConvert.DeserializeObject<List<Egitim>>(responseData);
			return View(egitim.ToList());
		}
		public ActionResult Create()
		{
			return View(new Egitim());
		}

		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Egitim v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PostAsync(_url, content);
			return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
		}
		public async Task<ActionResult> Edit(int id)
		{
			var responseMessage = await _client.GetAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return View("Error");

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;

			var vardiya = JsonConvert.DeserializeObject<Egitim>(responseData);
			return Json(vardiya, JsonRequestBehavior.AllowGet);

		}
		//PUT (Update) Method
		[HttpPost]
		public async Task<ActionResult> Edit(Egitim v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{v.EgitimID}", content);
			return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
		}
		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var responseMessage = await _client.DeleteAsync($"{_url}/{id}");
			return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
		}
	}
}