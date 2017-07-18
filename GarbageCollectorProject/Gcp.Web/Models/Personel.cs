using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Personel
    {
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

        public IEnumerable<Araclar> Arac { get; set; }
        public Egitim Egitim { get; set; }
        public Unvanlar Unvan { get; set; }
        public Vardiya Vardiya { get; set; }
    }
}