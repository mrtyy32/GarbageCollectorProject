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
	[Authorize(Roles = "admin")]
	public class MarkaController : Controller
    {
		readonly HttpClient _client;
		
		//string _url = "http://localhost:53723/api/Marka";
		string _url = "http://garbgabe.azurewebsites.net/api/Marka";
		public MarkaController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		[Authorize(Roles = "admin")]
		public ActionResult Index()
		{
			return View();
		}
		public async Task<ActionResult> forDropDown()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var marka = JsonConvert.DeserializeObject<List<Marka>>(responseData);
			return Json(marka, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> GetMarkaHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var marka = JsonConvert.DeserializeObject<List<Marka>>(responseData);
			return View(marka.ToList());
		}

		public ActionResult Create()
		{
			return View(new Marka());
		}

		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Marka v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PostAsync(_url, content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Create(v.MarkaAd + " markası oluşturuldu", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		public async Task<ActionResult> Edit(int id)
		{
			var responseMessage = await _client.GetAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return View("Error");

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;

			var marka = JsonConvert.DeserializeObject<Marka>(responseData);
			return Json(marka, JsonRequestBehavior.AllowGet);

		}
		//PUT (Update) Method
		[HttpPost]
		public async Task<ActionResult> Edit(Marka v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{v.MarkaID}", content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Update(v.MarkaAd + " markası güncellendi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var responseMessage = await _client.DeleteAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Delete("Marka silindi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
	}
}