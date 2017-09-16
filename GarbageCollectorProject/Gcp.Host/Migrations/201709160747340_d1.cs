namespace Gcp.Host.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Araclar",
                c => new
                    {
                        AracID = c.Int(nullable: false, identity: true),
                        MarkaID = c.Int(nullable: false),
                        AktifMi = c.Boolean(),
                        AracPlaka = c.String(),
                        ModelID = c.Int(),
                    })
                .PrimaryKey(t => t.AracID)
                .ForeignKey("dbo.Marka", t => t.MarkaID, cascadeDelete: true)
                .ForeignKey("dbo.Model", t => t.ModelID)
                .Index(t => t.MarkaID)
                .Index(t => t.ModelID);
            
            CreateTable(
                "dbo.AraclarDetay",
                c => new
                    {
                        AracDetayID = c.Int(nullable: false, identity: true),
                        AlinisTarihi = c.DateTime(),
                        BakimTarihi = c.DateTime(),
                        AracID = c.Int(nullable: false),
                        MuayeneTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.AracDetayID)
                .ForeignKey("dbo.Araclar", t => t.AracID, cascadeDelete: true)
                .Index(t => t.AracID);
            
            CreateTable(
                "dbo.AraclarGecmis",
                c => new
                    {
                        AracGecmisID = c.Int(nullable: false, identity: true),
                        AracID = c.Int(nullable: false),
                        PersonelID = c.Int(nullable: false),
                        VardiyaID = c.Int(),
                        TeslimTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AracGecmisID)
                .ForeignKey("dbo.Personel", t => t.PersonelID, cascadeDelete: true)
                .ForeignKey("dbo.Vardiya", t => t.VardiyaID)
                .ForeignKey("dbo.Araclar", t => t.AracID)
                .Index(t => t.AracID)
                .Index(t => t.PersonelID)
                .Index(t => t.VardiyaID);
            
            CreateTable(
                "dbo.Personel",
                c => new
                    {
                        PersonelID = c.Int(nullable: false, identity: true),
                        PersonelAd = c.String(),
                        PersonelSoyad = c.String(),
                        DogumTarihi = c.DateTime(),
                        UnvanID = c.Int(nullable: false),
                        GirisTarihi = c.DateTime(),
                        CikisTarihi = c.DateTime(),
                        izinTarihi = c.DateTime(),
                        Maas = c.Decimal(precision: 18, scale: 2),
                        EgitimID = c.Int(nullable: false),
                        VardiyaID = c.Int(nullable: false),
                        CalismaDurumu = c.Boolean(nullable: false),
                        AmirID = c.Int(),
                        AmirMi = c.Boolean(nullable: false),
                        AracID = c.Int(),
                    })
                .PrimaryKey(t => t.PersonelID)
                .ForeignKey("dbo.Personel", t => t.AmirID)
                .ForeignKey("dbo.Egitim", t => t.EgitimID, cascadeDelete: true)
                .ForeignKey("dbo.Unvanlar", t => t.UnvanID, cascadeDelete: true)
                .ForeignKey("dbo.Vardiya", t => t.VardiyaID, cascadeDelete: true)
                .ForeignKey("dbo.Araclar", t => t.AracID, cascadeDelete: true)
                .Index(t => t.UnvanID)
                .Index(t => t.EgitimID)
                .Index(t => t.VardiyaID)
                .Index(t => t.AmirID)
                .Index(t => t.AracID);
            
            CreateTable(
                "dbo.Egitim",
                c => new
                    {
                        EgitimID = c.Int(nullable: false, identity: true),
                        EgitimAd = c.String(),
                    })
                .PrimaryKey(t => t.EgitimID);
            
            CreateTable(
                "dbo.Unvanlar",
                c => new
                    {
                        UnvanID = c.Int(nullable: false, identity: true),
                        UnvanAd = c.String(),
                    })
                .PrimaryKey(t => t.UnvanID);
            
            CreateTable(
                "dbo.Vardiya",
                c => new
                    {
                        VardiyaID = c.Int(nullable: false, identity: true),
                        VardiyaAd = c.String(),
                        BaslamaSaati = c.String(),
                        BitirmeSaati = c.String(),
                    })
                .PrimaryKey(t => t.VardiyaID);
            
            CreateTable(
                "dbo.Marka",
                c => new
                    {
                        MarkaID = c.Int(nullable: false, identity: true),
                        MarkaAd = c.String(),
                    })
                .PrimaryKey(t => t.MarkaID);
            
            CreateTable(
                "dbo.Model",
                c => new
                    {
                        ModelID = c.Int(nullable: false, identity: true),
                        ModelAd = c.String(),
                        MarkaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModelID)
                .ForeignKey("dbo.Marka", t => t.MarkaID, cascadeDelete: true)
                .Index(t => t.MarkaID);
            
            CreateTable(
                "dbo.Islem",
                c => new
                    {
                        IslemID = c.Int(nullable: false, identity: true),
                        IslemTuru = c.String(),
                    })
                .PrimaryKey(t => t.IslemID);
            
            CreateTable(
                "dbo.IslemDetay",
                c => new
                    {
                        IslemDetayID = c.Int(nullable: false, identity: true),
                        IslemID = c.Int(nullable: false),
                        IslemIcerigi = c.String(),
                        Kullanici = c.String(),
                        IslemTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IslemDetayID)
                .ForeignKey("dbo.Islem", t => t.IslemID, cascadeDelete: true)
                .Index(t => t.IslemID);
            
            CreateTable(
                "dbo.Kurumlar",
                c => new
                    {
                        KurumID = c.Int(nullable: false, identity: true),
                        KurumIsmi = c.String(),
                        KurumAdresi = c.String(),
                        TemsilciKisi = c.String(),
                        TemsilciKisiNo = c.String(),
                        TemsilciKisiEmail = c.String(),
                        CalismaDurumu = c.Boolean(),
                        VergiDairesi = c.String(),
                        VergiNo = c.Int(),
                        VergiDairesiAdresi = c.String(),
                    })
                .PrimaryKey(t => t.KurumID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPassword = c.String(),
                        UserMail = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IslemDetay", "IslemID", "dbo.Islem");
            DropForeignKey("dbo.Personel", "AracID", "dbo.Araclar");
            DropForeignKey("dbo.Araclar", "ModelID", "dbo.Model");
            DropForeignKey("dbo.Model", "MarkaID", "dbo.Marka");
            DropForeignKey("dbo.Araclar", "MarkaID", "dbo.Marka");
            DropForeignKey("dbo.AraclarGecmis", "AracID", "dbo.Araclar");
            DropForeignKey("dbo.AraclarGecmis", "VardiyaID", "dbo.Vardiya");
            DropForeignKey("dbo.AraclarGecmis", "PersonelID", "dbo.Personel");
            DropForeignKey("dbo.Personel", "VardiyaID", "dbo.Vardiya");
            DropForeignKey("dbo.Personel", "UnvanID", "dbo.Unvanlar");
            DropForeignKey("dbo.Personel", "EgitimID", "dbo.Egitim");
            DropForeignKey("dbo.Personel", "AmirID", "dbo.Personel");
            DropForeignKey("dbo.AraclarDetay", "AracID", "dbo.Araclar");
            DropIndex("dbo.IslemDetay", new[] { "IslemID" });
            DropIndex("dbo.Model", new[] { "MarkaID" });
            DropIndex("dbo.Personel", new[] { "AracID" });
            DropIndex("dbo.Personel", new[] { "AmirID" });
            DropIndex("dbo.Personel", new[] { "VardiyaID" });
            DropIndex("dbo.Personel", new[] { "EgitimID" });
            DropIndex("dbo.Personel", new[] { "UnvanID" });
            DropIndex("dbo.AraclarGecmis", new[] { "VardiyaID" });
            DropIndex("dbo.AraclarGecmis", new[] { "PersonelID" });
            DropIndex("dbo.AraclarGecmis", new[] { "AracID" });
            DropIndex("dbo.AraclarDetay", new[] { "AracID" });
            DropIndex("dbo.Araclar", new[] { "ModelID" });
            DropIndex("dbo.Araclar", new[] { "MarkaID" });
            DropTable("dbo.User");
            DropTable("dbo.Kurumlar");
            DropTable("dbo.IslemDetay");
            DropTable("dbo.Islem");
            DropTable("dbo.Model");
            DropTable("dbo.Marka");
            DropTable("dbo.Vardiya");
            DropTable("dbo.Unvanlar");
            DropTable("dbo.Egitim");
            DropTable("dbo.Personel");
            DropTable("dbo.AraclarGecmis");
            DropTable("dbo.AraclarDetay");
            DropTable("dbo.Araclar");
        }
    }
}
