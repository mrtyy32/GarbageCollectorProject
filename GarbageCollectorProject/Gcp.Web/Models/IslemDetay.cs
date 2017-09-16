using System;

namespace Gcp.Web.Models
{
	public class IslemDetay
	{
		public int IslemDetayID { get; set; }
		public int IslemID { get; set; }
		public string IslemIcerigi { get; set; }
		public string Kullanici { get; set; }
		public DateTime IslemTarihi { get; set; }

		public Islem Islem { get; set; }
	}
}