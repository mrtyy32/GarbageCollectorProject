using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Gcp.Web.Models;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Gcp.Web.Controllers
{
    public class PersonelController : Controller
    {
        readonly HttpClient _client;
        string _url = "http://localhost:53723/api/Personel";
        public PersonelController()
        {
            _client = new HttpClient { BaseAddress = new Uri(_url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        // GET: Personel
        public async Task<ActionResult> Index()
        {
            var responseMessage = await _client.GetAsync(_url);

            if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
            return View(personel.ToList());
        }

        //public async Task<ActionResult> GetAllAsync()
        //{
        //    var responseMessage = await _client.GetAsync(_url);

        //    if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

        //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //    //var serializer = new JavaScriptSerializer();
        //    //var personel = serializer.Serialize(responseData);
        //    var personel = JsonConvert.DeserializeObject<List<Personel>>(responseData);
        //    return Json(personel,JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Create()
        {
            return View(new Personel());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(Personel p)
        {
            HttpContent content = new StringContent(p.ToString()); //VERi TİPİ DUZELTİLECEK
            HttpResponseMessage responseMessage = await _client.PostAsync(_url, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");

        }

        //PUT (Update) Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Personel p)
        {
            var responseMessage = await _client.GetAsync($"{_url}/{id}");
            var a = responseMessage.Content.ReadAsStringAsync().Result;
            HttpContent _contentData = new StringContent(p.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> GetProductsHtml()
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

            HttpResponseMessage responseMessage = await _client.DeleteAsync(_url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}