using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Egitim
    {
        public int EgitimID { get; set; }
        public string Aciklama { get; set; }


        public IEnumerable<PersonelDetay> PersonelDetay { get; set; }
    }
}