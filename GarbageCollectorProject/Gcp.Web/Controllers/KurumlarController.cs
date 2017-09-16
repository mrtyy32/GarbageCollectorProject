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
	public class KurumlarController : Controller
    {
		readonly HttpClient _client;
		//string _url = "http://localhost:53723/api/Kurumlar";
		string _url = "http://garbgabe.azurewebsites.net/api/Kurumlar";
		public KurumlarController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		[Authorize(Roles = "admin")]
		// GET: Kurumlar
		public ActionResult Index()
        {
            return View();
        }
		public async Task<ActionResult> GetKurumlarHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var kurum = JsonConvert.DeserializeObject<List<Kurumlar>>(responseData);
			return View(kurum.ToList());
		}
		public async Task<ActionResult> GetKurumlarListe()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var kurum = JsonConvert.DeserializeObject<List<Kurumlar>>(responseData);
			return View(kurum.ToList());
		}
		public ActionResult Create()
		{
			return View(new Kurumlar());
		}
		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Kurumlar k)
		{
			var jsonString = JsonConvert.SerializeObject(k);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PostAsync(_url, content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Create(k.KurumIsmi + " kurumu oluşturuldu", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		//GET Method
		public async Task<ActionResult> Edit(int id)
		{
			var responseMessage = await _client.GetAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return View($"Error");

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;

			var kurum = JsonConvert.DeserializeObject<Kurumlar>(responseData);
			return Json(kurum, JsonRequestBehavior.AllowGet);

		}
		//PUT (Update) Method
		[HttpPost]
		public async Task<ActionResult> Edit(Kurumlar k)
		{
			var jsonString = JsonConvert.SerializeObject(k);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{k.KurumID}", content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Update(k.KurumIsmi + " kurumu güncellendi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var responseMessage = await _client.DeleteAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Delete("Kurum silindi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		public async Task<ActionResult> Count()
		{
			var responseMessage = await _client.GetAsync(_url);
			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var kurum = JsonConvert.DeserializeObject<List<Kurumlar>>(responseData);
			return Json(kurum.Count(), JsonRequestBehavior.AllowGet);
		}
	}
}