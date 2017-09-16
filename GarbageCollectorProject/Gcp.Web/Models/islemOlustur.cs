using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gcp.Web.Models
{
	public class IslemOlustur
	{
		readonly HttpClient _client;
		string _islem = "http://garbgabe.azurewebsites.net/api/IslemDetay";
		public IslemOlustur()
		{
			_client = new HttpClient { BaseAddress = new Uri(_islem) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		public async Task Create(string islemIcerigi, string kullanici)
		{
			var id = new IslemDetay
			{
				IslemID = 1,
				IslemIcerigi = islemIcerigi,
				IslemTarihi = DateTime.Now,
				Kullanici = kullanici
			};

			var jsonString = JsonConvert.SerializeObject(id);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			await _client.PostAsync(_islem, content);
		}
		public async Task Update(string islemIcerigi, string kullanici)
		{
			var id = new IslemDetay
			{
				IslemID = 2,
				IslemIcerigi = islemIcerigi,
				IslemTarihi = DateTime.Now,
				Kullanici = kullanici
			};

			var jsonString = JsonConvert.SerializeObject(id);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			await _client.PostAsync(_islem, content);
		}
		public async Task Delete(string islemIcerigi, string kullanici)
		{
			var id = new IslemDetay
			{
				IslemID = 3,
				IslemIcerigi = islemIcerigi,
				IslemTarihi = DateTime.Now,
				Kullanici = kullanici
			};

			var jsonString = JsonConvert.SerializeObject(id);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			await _client.PostAsync(_islem, content);
		}
	}
}