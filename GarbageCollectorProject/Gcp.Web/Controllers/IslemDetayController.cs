using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;

namespace Gcp.Web.Controllers
{
    public class IslemDetayController : Controller
    {
		readonly HttpClient _client;
		string _islem = "http://garbgabe.azurewebsites.net/api/IslemDetay";
		public IslemDetayController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_islem) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<ActionResult> GetIslemDetayHtml()
		{
			var responseMessage = await _client.GetAsync(_islem);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var detay = JsonConvert.DeserializeObject<List<IslemDetay>>(responseData);
			return View(detay.ToList());
		}
		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var responseMessage = await _client.DeleteAsync($"{_islem}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");
			return RedirectToAction("Index","Home");
		}
	}
}