using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Gcp.Web.Models;
using Newtonsoft.Json;

namespace Gcp.Web.Controllers
{
	[Authorize(Roles = "admin")]
	public class EgitimController : Controller
    {
		readonly HttpClient _client;
		//string _url = "http://localhost:53723/api/Egitim";
		string _url = "http://garbgabe.azurewebsites.net/api/Egitim";
	    
		public EgitimController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		[Authorize(Roles = "admin")]
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
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Create(v.EgitimAd+" eğitimi oluşturuldu", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		public async Task<ActionResult> Edit(int id)
		{
			var responseMessage = await _client.GetAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return View($"Error");
			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var egitim = JsonConvert.DeserializeObject<Egitim>(responseData);
			return Json(egitim, JsonRequestBehavior.AllowGet);

		}
		//PUT (Update) Method
		[HttpPost]
		public async Task<ActionResult> Edit(Egitim v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{v.EgitimID}", content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Update(v.EgitimAd + " eğitimi güncellendi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var responseMessage = await _client.DeleteAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Delete("Eğitim silindi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
	}
}