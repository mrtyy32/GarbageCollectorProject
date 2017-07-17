using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gcp.Web.Models;
using Newtonsoft.Json;

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
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetAllAsync()
        {
            var responseMessage = await _client.GetAsync(_url);

            if (!responseMessage.IsSuccessStatusCode) return Json("Error", JsonRequestBehavior.DenyGet);

            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            var personel = JsonConvert.DeserializeObject<PersonelModel>(responseData);
            return Json(personel, JsonRequestBehavior.AllowGet);
        }
    }
}