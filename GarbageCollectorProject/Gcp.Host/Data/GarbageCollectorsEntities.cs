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
		public virtual DbSet<Islem> Islem { get; set; }
		public virtual DbSet<IslemDetay> IslemDetay { get; set; }
		public virtual DbSet<AraclarGecmis> AraclarGecmis { get; set; }
		public virtual DbSet<AylikGider> AylikGider { get; set; }
		public virtual DbSet<Aylar> Aylar { get; set; }

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

			context.Islem.AddOrUpdate(i=> new{ i.IslemID, i.IslemTuru },
				new Islem {IslemTuru = "Kayıt"},
				new Islem {IslemTuru = "Güncelleme"},
				new Islem {IslemTuru = "Silme"}
			);

			context.Aylar.AddOrUpdate(ay=> new { ay.AyID, ay.AyAd },
				new Aylar { AyAd = "Ocak"},new Aylar { AyAd = "Şubat"},new Aylar { AyAd = "Mart"},new Aylar { AyAd = "Nisan"},new Aylar { AyAd = "Mayıs"},
				new Aylar { AyAd = "Haziran"},new Aylar { AyAd = "Temmuz"},new Aylar { AyAd = "Ağustos"},new Aylar { AyAd = "Eylül"},new Aylar { AyAd = "Ekim"},
				new Aylar { AyAd = "Kasım"},new Aylar { AyAd = "Aralık"}
				);

			
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

			modelBuilder.Entity<Araclar>()
				.HasMany(d => d.AraclarGecmis)
				.WithRequired(d => d.Araclar)
				.HasForeignKey(d => d.AracID).WillCascadeOnDelete(false); 

			modelBuilder.Entity<Islem>()
				.HasMany(i => i.IslemDetay)
				.WithRequired(i => i.Islem)
				.HasForeignKey(i => i.IslemID);

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