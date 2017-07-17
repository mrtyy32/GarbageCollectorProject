using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gcp.Host.Entity;

namespace Gcp.Host.Models
{
    public class PersonelModel
    {
        public IEnumerable<Personel> ListPersonel { get; set; }
        public Personel Personel { get; set; }
        //public IEnumerable<PersonelDetay> ListPersonelDetay { get; set; }
        //public PersonelDetay PersonelDetay { get; set; }
       
    }
}
