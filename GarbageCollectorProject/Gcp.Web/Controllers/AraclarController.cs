using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;

namespace Gcp.Web.Controllers
{
    public class AraclarController : Controller
    {
		// GET: Araclar
		readonly HttpClient _client;
		string _url = "http://localhost:53723/api/Araclar";
		public AraclarController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public ActionResult Index()
		{
			return View();
		}
		public async Task<ActionResult> forDropDown()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var arac = JsonConvert.DeserializeObject<List<Egitim>>(responseData);
			return Json(arac, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> GetAraclarHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var arac = JsonConvert.DeserializeObject<List<Araclar>>(responseData);
			return View(arac.ToList());
		}
		public ActionResult Create()
		{
			return View(new Egitim());
		}

		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Araclar v)
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

			var arac = JsonConvert.DeserializeObject<Araclar>(responseData);
			return Json(arac, JsonRequestBehavior.AllowGet);

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