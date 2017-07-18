using System.Linq;
using Gcp.Host.Entity;

namespace Gcp.Host.Models
{
    public class ListPersonel
    {

        public dynamic List{ get; set; }

        public ListPersonel()
        {
            var db = new GarbageCollectorsEntities();
            db.Configuration.LazyLoadingEnabled = false;
            List = db.Personel.Select(x => new
                {
                    x.PersonelID,
                    x.PersonelAd,
                    x.PersonelSoyad,
                    x.DogumTarihi,
                    x.UnvanID,
                    x.Unvanlar.UnvanAd,
                    x.GirisTarihi,
                    x.CikisTarihi,
                    x.izinTarihi,
                    x.Maas,
                    x.EgitimID,
                    x.Egitim.EgitimAd,
                    x.VardiyaID,
                    x.Vardiya.Aciklama,
                    x.CalismaDurumu,
                    x.AmirMi,
                    x.AmirID
                })
                .ToList();

        }
    }
}