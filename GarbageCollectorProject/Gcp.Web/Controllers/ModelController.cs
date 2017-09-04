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
	public class ModelController : Controller
    {
		readonly HttpClient _client;
		
		//string _url = "http://localhost:53723/api/Model";
		string _url = "http://garbgabe.azurewebsites.net/api/Model";
		public ModelController()
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
		public async Task<ActionResult> forDropDown(int markaId)
		{
			var responseMessage = await _client.GetAsync($"{_url}/forDropDown/{markaId}");

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var Model = JsonConvert.DeserializeObject<List<Model>>(responseData);
			return Json(Model, JsonRequestBehavior.AllowGet);
		}
		public async Task<ActionResult> GetModelHtml()
		{
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var Model = JsonConvert.DeserializeObject<List<Model>>(responseData);
			return View(Model.ToList());
		}
		public ActionResult Create()
		{
			return View(new Model());
		}

		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Model v)
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

			var Model = JsonConvert.DeserializeObject<Model>(responseData);
			return Json(Model, JsonRequestBehavior.AllowGet);

		}
		//PUT (Update) Method
		[HttpPost]
		public async Task<ActionResult> Edit(Model v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{v.ModelID}", content);
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