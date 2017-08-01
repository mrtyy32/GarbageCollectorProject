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
        public ActionResult Create()
        {
            return View(new Vardiya());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(Vardiya v)
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

            var vardiya = JsonConvert.DeserializeObject<Vardiya>(responseData);
            return Json(vardiya, JsonRequestBehavior.AllowGet);

        }
        //PUT (Update) Method
        [HttpPost]
        public async Task<ActionResult> Edit(Vardiya v)
        {
            var jsonString = JsonConvert.SerializeObject(v);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var responseMessage = await _client.PutAsync($"{_url}/{v.VardiyaID}", content);
            return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
        }
		public async Task<ActionResult> forDropDown()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var vardiya = JsonConvert.DeserializeObject<List<Vardiya>>(responseData);
			return Json(vardiya, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> GetVardiyaHtml()
        {
            var responseMessage = await _client.GetAsync(_url);

            if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            var vardiya = JsonConvert.DeserializeObject<List<Vardiya>>(responseData);
            return View(vardiya.ToList());
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var responseMessage = await _client.DeleteAsync($"{_url}/{id}");
            return RedirectToAction(responseMessage.IsSuccessStatusCode ? "Index" : "Error");
        }

	    public async Task<ActionResult> Hours()
	    {
		    string[] hours =
		    {
			    "00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00",
			    "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00",
			    "19:00", "20:00", "21:00", "22:00", "23:00"
			};
		    //var jsonString = @"{""hourList"": [
      //        {
      //          ""hours"": ""00:00"",
      //        },
		    return Json(hours, JsonRequestBehavior.AllowGet);
	    }
    }
}