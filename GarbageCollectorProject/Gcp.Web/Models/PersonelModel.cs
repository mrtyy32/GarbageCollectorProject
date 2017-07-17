using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class PersonelModel
    {
        public IEnumerable<Personel> ListPersonel { get; set; }
        public Personel Personel { get; set; }
    }
}