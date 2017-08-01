using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;

namespace Gcp.Web.Controllers
{
    public class UnvanController : Controller
    {
		readonly HttpClient _client;
		string _url = "http://localhost:53723/api/Unvanlar";
		public UnvanController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		// GET: Unvan
		public ActionResult Index()
        {
            return View();
        }
		public async Task<ActionResult> forDropDown()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var unvan = JsonConvert.DeserializeObject<List<Unvanlar>>(responseData);
			return Json(unvan, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> GetUnvanHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var unvan = JsonConvert.DeserializeObject<List<Personel>>(responseData);
			return View(unvan.ToList());
		}
	}
}