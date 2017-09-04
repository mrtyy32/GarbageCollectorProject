using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Araclar
    {
        public string AracID { get; set; }
        public int MarkaID { get; set; }
        //public int? PersonelID { get; set; }
		public bool AktifMi { get; set; }
		public string AracPlaka { get; set; }
		public int? ModelID { get; set; }

		public Marka Marka { get; set; }
        public List<AraclarDetay> AraclarDetay { get; set; }
        public List<Personel> Personel { get; set; }
        public Model Model { get; set; }
	}
}