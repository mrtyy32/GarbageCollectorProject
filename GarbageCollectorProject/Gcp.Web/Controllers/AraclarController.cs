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
	public class AraclarController : Controller
	{
		// GET: Araclar
		readonly HttpClient _client;
		string _url = "http://garbgabe.azurewebsites.net/api/Araclar";
		//string _url = "http://localhost:53723/api/Araclar";
		//string _urlDetay = "http://localhost:53723/api/AraclarDetay";
		string _urlDetay = "http://garbgabe.azurewebsites.net/api/AraclarDetay";
		string _urlGecmis = "http://garbgabe.azurewebsites.net/api/AraclarGecmis";

		public AraclarController()
		{
			_client = new HttpClient {BaseAddress = new Uri(_url)};
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
			var arac = JsonConvert.DeserializeObject<List<Araclar>>(responseData);
			return Json(arac, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> GetAraclarHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var arac = JsonConvert.DeserializeObject<List<Araclar>>(responseData);
			return View(arac);
		}

		public async Task<ActionResult> GetAraclarListe()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var arac = JsonConvert.DeserializeObject<List<Araclar>>(responseData);
			return View(arac);
		}

		public ActionResult Create()
		{
			return View(new Araclar());
		}

		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Araclar v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PostAsync(_url, content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Create(v.AracPlaka + " aracı oluşturuldu", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}

		public async Task<ActionResult> Edit(int id)
		{
			var responseMessage = await _client.GetAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return View($"Error");

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;

			var arac = JsonConvert.DeserializeObject<Araclar>(responseData);
			return Json(arac, JsonRequestBehavior.AllowGet);

		}

		//PUT (Update) Method
		[HttpPost]
		public async Task<ActionResult> Edit(Araclar v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{v.AracID}", content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Update(v.AracPlaka + " aracı güncellendi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var responseMessage = await _client.DeleteAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");
			await new IslemOlustur().Delete("Araç silindi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}

		public async Task<ActionResult> Count()
		{
			var responseMessage = await _client.GetAsync(_url);
			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var arac = JsonConvert.DeserializeObject<List<Araclar>>(responseData);
			return Json(arac.Count, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> DetayDokumHtml(int aracId)
		{
			var responseMessage = await _client.GetAsync($"{_urlDetay}/DetayDokum/{aracId}");
			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);
			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var detaydokum = JsonConvert.DeserializeObject<List<AraclarDetay>>(responseData);
			return View(detaydokum);
		}

		public async Task<ActionResult> GecmisDokumHtml(int aracId)
		{
			var responseMessage = await _client.GetAsync($"{_urlGecmis}/AracGecmis/{aracId}");
			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);
			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var gecmisdokum = JsonConvert.DeserializeObject<List<AraclarGecmis>>(responseData);
			return View(gecmisdokum);
		}

		[HttpPost]
		public async Task<ActionResult> DetayCreate(AraclarDetay ad)
		{
			var jsonString = JsonConvert.SerializeObject(ad);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PostAsync(_urlDetay,content);
			return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
		}
	}
}