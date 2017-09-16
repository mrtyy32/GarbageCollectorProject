using System;
using System.Collections.Generic;

namespace Gcp.Web.Models
{
	public class AraclarGecmis
	{
		public int AracGecmisID { get; set; }
		public int AracID { get; set; }
		public int PersonelID { get; set; }
		public int? VardiyaID { get; set; }
		public DateTime TeslimTarihi { get; set; }

		public Araclar Araclar { get; set; }
		public Vardiya Vardiya { get; set; }
		public Personel Personel { get; set; }
	}
}