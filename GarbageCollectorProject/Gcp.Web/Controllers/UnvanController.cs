﻿using System;
using System.Collections.Generic;
using System.Linq;
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
	public class UnvanController : Controller
    {
		readonly HttpClient _client;
		
		//string _url = "http://localhost:53723/api/Unvanlar";
		string _url = "http://garbgabe.azurewebsites.net/api/Unvanlar";
		public UnvanController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		[Authorize(Roles = "admin")]
		// GET: Unvan
		public async Task<ActionResult> Index()
        {
			var responseMessage = await _client.GetAsync(_url);

			if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;
			var unvan = JsonConvert.DeserializeObject<List<Unvanlar>>(responseData);
			return View(unvan.ToList());
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
			var unvan = JsonConvert.DeserializeObject<List<Unvanlar>>(responseData);
			return View(unvan.ToList());
		}
		public ActionResult Create()
		{
			return View(new Unvanlar());
		}

		//The Post method
		[HttpPost]
		public async Task<ActionResult> Create(Unvanlar v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PostAsync(_url, content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Create(v.UnvanAd+ " ünvanı oluşturuldu", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		public async Task<ActionResult> Edit(int id)
		{
			var responseMessage = await _client.GetAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return View("Error");

			var responseData = responseMessage.Content.ReadAsStringAsync().Result;

			var unvan = JsonConvert.DeserializeObject<Unvanlar>(responseData);
			return Json(unvan, JsonRequestBehavior.AllowGet);

		}
		//PUT (Update) Method
		[HttpPost]
		public async Task<ActionResult> Edit(Unvanlar v)
		{
			var jsonString = JsonConvert.SerializeObject(v);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var responseMessage = await _client.PutAsync($"{_url}/{v.UnvanID}", content);
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Update(v.UnvanAd + " ünvanı güncellendi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var responseMessage = await _client.DeleteAsync($"{_url}/{id}");
			if (!responseMessage.IsSuccessStatusCode) return RedirectToAction($"Error");

			await new IslemOlustur().Delete("Ünvan silindi", HttpContext.User.Identity.Name);
			return RedirectToAction("Index");
		}
	}
}