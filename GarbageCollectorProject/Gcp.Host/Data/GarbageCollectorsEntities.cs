using System;
using System.Data.Entity.Migrations;

namespace Gcp.Host.Data
{
	using System.Data.Entity;

	public class GarbageCollectorsEntities : DbContext
	{
		public GarbageCollectorsEntities()
			: base("name=GarbageCollectorsEntities")
		{
		}

		static GarbageCollectorsEntities()
		{
			Database.SetInitializer(new DropCreateIfChangeInitializer());
			Database.SetInitializer(new CreateInitializer());
		}

		public virtual DbSet<Araclar> Araclar { get; set; }
		public virtual DbSet<AraclarDetay> AraclarDetay { get; set; }
		public virtual DbSet<Egitim> Egitim { get; set; }
		public virtual DbSet<Kurumlar> Kurumlar { get; set; }
		public virtual DbSet<Marka> Marka { get; set; }
		public virtual DbSet<Model> Model { get; set; }
		public virtual DbSet<Personel> Personel { get; set; }
		public virtual DbSet<Unvanlar> Unvanlar { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<Vardiya> Vardiya { get; set; }

		public void Seed(GarbageCollectorsEntities context)
		{
			context.Marka.AddOrUpdate(
			  p => new { p.MarkaAd, p.MarkaID },
			  new Marka { MarkaAd = "BMC" },
			  new Marka { MarkaAd = "Fatih" },
			  new Marka { MarkaAd = "Mercedes" }
			);

			context.Model.AddOrUpdate(
			  p => new { p.ModelAd, p.ModelID, p.MarkaID },
			  new Model { ModelAd = "BMCmodel", MarkaID = 1 },
			  new Model { ModelAd = "Fatihmodel", MarkaID = 2 },
			  new Model { ModelAd = "Mercedesmodel", MarkaID = 3 }
			);

			context.User.AddOrUpdate(
			  p => new { p.UserID, p.UserName, p.UserMail, p.UserPassword, p.Role },
			  new User { UserName = "admin", UserPassword = "admin", Role = "admin", UserMail = "admin@garbage.com" }
			);

			context.Egitim.AddOrUpdate(e => new { e.EgitimAd, e.EgitimID },
				new Egitim { EgitimAd = "Orta Öğretim" },
				new Egitim { EgitimAd = "Lise" },
				new Egitim { EgitimAd = "Ön Lisans" },
				new Egitim { EgitimAd = "Lisans" }
				);

			context.Unvanlar.AddOrUpdate(u => new { u.UnvanAd, u.UnvanID },
				new Unvanlar { UnvanAd = "Çavuş" },
				new Unvanlar { UnvanAd = "Muhasebe" },
				new Unvanlar { UnvanAd = "Müdür" }
				);

			context.Vardiya.AddOrUpdate(v => new { v.VardiyaAd, v.BaslamaSaati, v.BitirmeSaati, v.VardiyaID },
				new Vardiya { VardiyaAd = "Sabah", BaslamaSaati = "06:00", BitirmeSaati = "14:00" },
				new Vardiya { VardiyaAd = "Akşam", BaslamaSaati = "14:00", BitirmeSaati = "22:00" },
				new Vardiya { VardiyaAd = "Gece", BaslamaSaati = "22:00", BitirmeSaati = "06:00" }
				);

			//context.Araclar.AddOrUpdate(a => new { a.AracPlaka, a.AktifMi, a.MarkaID, a.ModelID, a.PersonelID, a.AracID },
			//	new Araclar { AracPlaka = "34asd1907", MarkaID = 1, ModelID = 1, PersonelID = 1, /*AktifMi = true as bool?*/ },
			//	new Araclar { AracPlaka = "34gf147", MarkaID = 2, ModelID = 1, PersonelID = 2, /*AktifMi = true as bool?*/ }
			//	);

			//context.Personel.AddOrUpdate(
			//	p =>
			//		new
			//		{
			//			p.PersonelAd,
			//			p.PersonelSoyad,
			//			p.EgitimID,
			//			p.UnvanID,
			//			p.VardiyaID,
			//			p.AmirID,
			//			p.AmirMi,
			//			p.PersonelID,
			//			p.DogumTarihi,
			//			p.CalismaDurumu,
			//			p.GirisTarihi,
			//			p.CikisTarihi,
			//			p.Maas,
			//			p.izinTarihi
			//		},
			//		new Personel
			//		{
			//			PersonelAd = "Murat",
			//			PersonelSoyad = "Savran",
			//			EgitimID = 3,
			//			VardiyaID = 1,
			//			UnvanID = 2,
			//			AmirMi = false,
			//			CalismaDurumu = true,
			//			Maas = 2500,
			//			GirisTarihi = DateTime.Parse("2014-10-29 00:00:00:0000"),
			//			DogumTarihi = DateTime.Parse("1992-06-29 00:00:00:0000"),
			//			AmirID = 0
			//		},
			//		new Personel
			//		{
			//			PersonelAd = "Özlem",
			//			PersonelSoyad = "Sakallı",
			//			EgitimID = 4,
			//			VardiyaID = 1,
			//			UnvanID = 3,
			//			AmirMi = true,
			//			CalismaDurumu = true,
			//			Maas = 4500,
			//			GirisTarihi = DateTime.Parse("2014-10-29 00:00:00:0000"),
			//			DogumTarihi = DateTime.Parse("1989-02-15 00:00:00:0000"),
			//			AmirID = 0
			//		}
			//	);

			//context.Kurumlar.AddOrUpdate(
			//	k =>
			//		new
			//		{
			//			k.KurumID,
			//			k.KurumIsmi,
			//			k.KurumAdresi,
			//			k.TemsilciKisi,
			//			k.TemsilciKisiNo,
			//			k.TemsilciKisiEmail,
			//			k.CalismaDurumu,
			//			k.VergiDairesi,
			//			k.VergiDairesiAdresi,
			//			k.VergiNo
			//		},
			//		new Kurumlar
			//		{
			//			KurumIsmi = "Örnek Kurum",
			//			KurumAdresi = "Kurum Adresi",
			//			TemsilciKisi = "Temsilci Kişi",
			//			TemsilciKisiNo = "05417412145",
			//			TemsilciKisiEmail = "temsilciyim@fena.com",
			//			CalismaDurumu = true as bool?,
			//			VergiDairesi = "Kadıköy",
			//			VergiDairesiAdresi = "Osmanağa Mh.",
			//			VergiNo = int.Parse(12378612387.ToString())
			//		}
			//		);

			context.SaveChanges();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Araclar>()
				.HasMany(d => d.AraclarDetay)
				.WithRequired(d => d.Araclar)
				.HasForeignKey(d => d.AracID);

			modelBuilder.Entity<Araclar>()
				.HasMany(d => d.Personel)
				.WithRequired(d => d.Araclar)
				.HasForeignKey(d => d.AracID);

			modelBuilder.Entity<Egitim>()
				.HasMany(d => d.Personel)
				.WithRequired(d => d.Egitim)
				.HasForeignKey(d => d.EgitimID);

			modelBuilder.Entity<Unvanlar>()
				.HasMany(d => d.Personel)
				.WithRequired(d => d.Unvanlar)
				.HasForeignKey(d => d.UnvanID);

			modelBuilder.Entity<Vardiya>()
				.HasMany(d => d.Personel)
				.WithRequired(d => d.Vardiya)
				.HasForeignKey(d => d.VardiyaID);

			modelBuilder.Entity<Marka>()
				.HasMany(d => d.Model)
				.WithRequired(d => d.Marka)
				.HasForeignKey(d => d.MarkaID);

			base.OnModelCreating(modelBuilder);
		}

		public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<GarbageCollectorsEntities>
		{
			protected override void Seed(GarbageCollectorsEntities context)
			{
				context.Seed(context);

				base.Seed(context);
			}
		}

		public class CreateInitializer : CreateDatabaseIfNotExists<GarbageCollectorsEntities>
		{
			protected override void Seed(GarbageCollectorsEntities context)
			{
				context.Seed(context);

				base.Seed(context);
			}
		}
	}
}