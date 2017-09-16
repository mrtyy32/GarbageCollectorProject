using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Egitim
    {
        public int EgitimID { get; set; }
        public string EgitimAd { get; set; }
		//public int Deger { get; set; }

		public List<Personel> Personel { get; set; }
    }
}