using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("AraclarGecmis")]
	//[JsonObject(IsReference = true)]
	public class AraclarGecmis
	{

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AracGecmisID { get; set; }
		public int AracID { get; set; }
		public int PersonelID { get; set; }
		public int? VardiyaID { get; set; }
		public DateTime TeslimTarihi { get; set; }

		[ForeignKey("AracID")]
		public virtual Araclar Araclar { get; set; }
		[ForeignKey("VardiyaID")]
		public virtual Vardiya Vardiya { get; set; }
		public virtual Personel Personel { get; set; }
	}
}