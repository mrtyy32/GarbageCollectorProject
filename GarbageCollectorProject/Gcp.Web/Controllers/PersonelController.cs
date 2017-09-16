using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace Gcp.Web.Controllers
{
	[Authorize(Roles = "admin")]
	public class PersonelController : Controller
	{
		readonly HttpClient _client;

		//string _url = "http://localhost:53723/api/Personel";
		string _url = "http://garbgabe.azurewebsites.net/api/Personel";
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
			return Json(personel.Count, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> aylikGider()
		{
			var responseMessage = await _client.GetAsync(_url);
			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
			var maasToplam = (from item in personel where item.Maas != null select (decimal)(item.Maas) into pmaas select (int)pmaas).Sum();

			return Json((decimal)maasToplam, JsonRequestBehavior.AllowGet);
		}
		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Personel p)
		{
			var jsonString = JsonConvert.SerializeObject(p);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PostAsync(_url, content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");
			if (p.AracID != null)
			{
				var responseData = responseMessage.Content.ReadAsStringAsync().Result;
				var personel = JsonConvert.DeserializeObject<Personel>(responseData);
				await new GecmisOlustur().Create((int)p.AracID, (int)p.VardiyaID, personel.PersonelID);
			}
			await new IslemOlustur().Create(p.PersonelAd + " " + p.PersonelSoyad + " personeli oluşturuldu", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
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
		public async Task<ActionResult> Edit(Personel p, int? oldAracId)
		{
			var jsonString = JsonConvert.SerializeObject(p);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{p.PersonelID}", content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			if (oldAracId != p.AracID)
			{
				await new GecmisOlustur().Create((int)p.AracID, (int)p.VardiyaID, p.PersonelID);
			}

			await new IslemOlustur().Update(p.PersonelAd + " " + p.PersonelSoyad + " personeli güncellendi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
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
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Delete("Personel silindi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		public async Task<ActionResult> forDropDown()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
			return Json(personel, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> amirDropDown()
		{
			var responseMessage = await _client.GetAsync($"{_url}/amirDropDown");

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
			return Json(personel, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> GetPersonelIslemHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
			return View(personel.ToList());
		}
		public async Task<ActionResult> giderDagilim()
		{
			var responseMessage = await _client.GetAsync($"{_url}/personelGiderTablosu");

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
			var dagilim = personel.Select(x => new
			{
				pAdSoyad = x.PersonelAd + " " + x.PersonelSoyad,
				gider = x.Maas
			});
			return Json(dagilim, JsonRequestBehavior.AllowGet);
		}
	}
}