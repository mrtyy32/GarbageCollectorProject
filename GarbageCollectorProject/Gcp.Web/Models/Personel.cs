using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Web.Models
{
    public class Personel
    {
        public int PersonelID { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public int? UnvanID { get; set; }
		[DataType(DataType.Date)]
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

		[ForeignKey("AmirID")]
        public Personel Amir { get; set; }
        public Egitim Egitim { get; set; }
        public Unvanlar Unvanlar { get; set; }
		public Vardiya Vardiya { get; set; }
	    public Araclar Araclar { get; set; }
	}
}