
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gcp.Web.Models
{
	public class GecmisOlustur
	{
		readonly HttpClient _client;
		string _gecmis = "http://garbgabe.azurewebsites.net/api/AraclarGecmis";
		public GecmisOlustur()
		{
			_client = new HttpClient { BaseAddress = new Uri(_gecmis) };
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		public async Task Create(int aId, int vId, int pId)
		{
			var id = new AraclarGecmis
			{
				AracID = aId,
				PersonelID = pId,
				VardiyaID = vId,
				TeslimTarihi = DateTime.Now
			};
			
			var jsonString = JsonConvert.SerializeObject(id);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			await _client.PostAsync(_gecmis, content);
		}

		public async Task Delete(int id)
		{
			if (true)
			{
				await _client.DeleteAsync($"{_gecmis}/{id}");
			}

		}
	}
}