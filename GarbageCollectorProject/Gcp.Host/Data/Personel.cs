using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("Personel")]
	//[JsonObject(IsReference = true)]
	public class Personel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PersonelID { get; set; }
		public string PersonelAd { get; set; }
		public string PersonelSoyad { get; set; }
		public DateTime? DogumTarihi { get; set; }
		public int? UnvanID { get; set; }
		public DateTime? GirisTarihi { get; set; }
		public DateTime? CikisTarihi { get; set; }
		public DateTime? izinTarihi { get; set; }
		public decimal? Maas { get; set; }
		public int? EgitimID { get; set; }
		public int? VardiyaID { get; set; }
		public bool CalismaDurumu { get; set; }
		public int? AmirID { get; set; }
		public bool AmirMi { get; set; }
		public int? AracID { get; set; }


		[ForeignKey("AracID")]
		public virtual Araclar Araclar { get; set; }
		[ForeignKey("EgitimID")]
		public virtual Egitim Egitim { get; set; }
		[ForeignKey("AmirID")]
		public virtual Personel Amir { get; set; }
		[ForeignKey("UnvanID")]
		public virtual Unvanlar Unvanlar { get; set; }
		[ForeignKey("VardiyaID")]
		public virtual Vardiya Vardiya { get; set; }
	}
}