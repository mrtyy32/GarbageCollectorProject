using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;

namespace Gcp.Web.Controllers
{
    public class VardiyaController : Controller
    {
		readonly HttpClient _client;
		string _url = "http://localhost:53723/api/Vardiya";
		public VardiyaController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		// GET: Vardiya
		public ActionResult Index()
        {
            return View();
        }
		public async Task<ActionResult> forDropDown()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var vardiya = JsonConvert.DeserializeObject<List<Vardiya>>(responseData);
			return Json(vardiya, JsonRequestBehavior.AllowGet);
		}
	}
}