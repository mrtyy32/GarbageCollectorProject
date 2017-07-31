using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Vardiya
    {
        public int VardiyaID { get; set; }
        public string Aciklama { get; set; }
		public string BaslamaSaati { get; set; }
		public string BitirmeSaati { get; set; }

        public IEnumerable<Personel> Personel { get; set; }
    }
}