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
                    x.PersonelDetay.UnvanID,
                    //UnvanAd = x.PersonelDetay.Unvan.Aciklama,
                    x.PersonelDetay.GirisTarihi,
                    x.PersonelDetay.CikisTarihi,
                    x.PersonelDetay.IzinTarihi,
                    x.PersonelDetay.Maas,
                    x.PersonelDetay.EgitimID,
                    EgitimAd = x.PersonelDetay.Egitim.Aciklama,
                    x.PersonelDetay.VardiyaID,
                    VardiyaAd = x.PersonelDetay.Vardiya.Aciklama,
                    x.PersonelDetay.CalismaDurumu,
                    x.PersonelDetay.AmirMi,
                    x.PersonelDetay.AmirID
                })
                .ToList();

        }
    }
}