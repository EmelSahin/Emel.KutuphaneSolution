namespace TB.Kutuphane.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDatabae : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kitap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 50),
                        SiraNo = c.String(nullable: false, maxLength: 20),
                        Adet = c.Int(nullable: false),
                        EklemeTarihi = c.DateTime(nullable: false),
                        YazarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Yazar", t => t.YazarId, cascadeDelete: true)
                .Index(t => t.YazarId);
            
            CreateTable(
                "dbo.Yazar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 50),
                        Soyad = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OduncKitap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlisTarih = c.DateTime(nullable: false),
                        GetirecegiTarih = c.DateTime(nullable: false),
                        GetirdigiTarih = c.DateTime(),
                        GetirdiMi = c.Boolean(nullable: false),
                        KitapId = c.Int(nullable: false),
                        UyeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kitap", t => t.KitapId, cascadeDelete: true)
                .ForeignKey("dbo.Uye", t => t.UyeId, cascadeDelete: true)
                .Index(t => t.KitapId)
                .Index(t => t.UyeId);
            
            CreateTable(
                "dbo.Uye",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 50),
                        Soyad = c.String(nullable: false, maxLength: 50),
                        TC = c.String(maxLength: 11, fixedLength: true, unicode: false),
                        Telefon = c.String(maxLength: 11, fixedLength: true, unicode: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        Mail = c.String(maxLength: 100),
                        Sifre = c.String(maxLength: 32, fixedLength: true, unicode: false),
                        Ceza = c.Int(nullable: false),
                        Yetki = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitapKategori",
                c => new
                    {
                        Kitap_Id = c.Int(nullable: false),
                        Kategori_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Kitap_Id, t.Kategori_Id })
                .ForeignKey("dbo.Kitap", t => t.Kitap_Id, cascadeDelete: true)
                .ForeignKey("dbo.Kategori", t => t.Kategori_Id, cascadeDelete: true)
                .Index(t => t.Kitap_Id)
                .Index(t => t.Kategori_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OduncKitap", "UyeId", "dbo.Uye");
            DropForeignKey("dbo.OduncKitap", "KitapId", "dbo.Kitap");
            DropForeignKey("dbo.Kitap", "YazarId", "dbo.Yazar");
            DropForeignKey("dbo.KitapKategori", "Kategori_Id", "dbo.Kategori");
            DropForeignKey("dbo.KitapKategori", "Kitap_Id", "dbo.Kitap");
            DropIndex("dbo.KitapKategori", new[] { "Kategori_Id" });
            DropIndex("dbo.KitapKategori", new[] { "Kitap_Id" });
            DropIndex("dbo.OduncKitap", new[] { "UyeId" });
            DropIndex("dbo.OduncKitap", new[] { "KitapId" });
            DropIndex("dbo.Kitap", new[] { "YazarId" });
            DropTable("dbo.KitapKategori");
            DropTable("dbo.Uye");
            DropTable("dbo.OduncKitap");
            DropTable("dbo.Yazar");
            DropTable("dbo.Kitap");
            DropTable("dbo.Kategori");
        }
    }
}
