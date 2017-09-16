using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gcp.Web.Models;
using System.Web.Security;

namespace Gcp.Web.Controllers
{
	public class LoginController : Controller
	{
		readonly HttpClient _client;
		//string _url = "http://localhost:53723/api/Users/Login";
		string _url = "http://garbgabe.azurewebsites.net/api/Users/Login";
		public LoginController()
		{
			_client = new HttpClient { BaseAddress = new Uri(_url) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		// GET: Login
		public ActionResult Index()
		{
			return View();
		}

		public async Task<ActionResult> Login(string name, string password)
		{
			var responseMessage = await _client.GetAsync($"{_url}/{name}/{password}");
			if (responseMessage.IsSuccessStatusCode)
			{
				FormsAuthentication.SetAuthCookie(name, false);
				var authTicket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddMinutes(20), false, name);
				var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
				var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
				HttpContext.Response.Cookies.Add(authCookie);
				return await Task.Run<ActionResult>(() => RedirectToAction("Index", "Home"));
			}

			FormsAuthentication.SetAuthCookie("nonLog", false);
			FormsAuthentication.SignOut();
			return await Task.Run<ActionResult>(() => RedirectToAction("Index", "Login"));
		}

		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Login");
		}
	}
}