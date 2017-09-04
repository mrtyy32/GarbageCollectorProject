using System.Linq;
using Gcp.Host.Data;

namespace Gcp.Host.Models
{
    public class PersonelModel
    {
        public IQueryable<Personel> ListPersonel { get; set; }
        public Personel Personel { get; set; }
        //public IEnumerable<PersonelDetay> ListPersonelDetay { get; set; }
        //public PersonelDetay PersonelDetay { get; set; }
       
    }
}
