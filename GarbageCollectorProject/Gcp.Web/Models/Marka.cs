using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Marka
    {
        public int MarkaID { get; set; }
        public string Aciklama { get; set; }

        public IEnumerable<Araclar> Araclar { get; set; }
        public IEnumerable<Model> Model { get; set; }
    }
}