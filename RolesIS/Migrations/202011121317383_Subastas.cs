namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subastas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subastas",
                c => new
                    {
                        SubastaID = c.Int(nullable: false, identity: true),
                        Id = c.String(),
                        tiempoPublicacion = c.DateTime(nullable: false),
                        PrecioInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Id2 = c.String(),
                        tiempoUltimaPuja = c.DateTime(nullable: false),
                        TiempoRestanteParaComenzar = c.Double(nullable: false),
                        SubastaTerminada = c.Boolean(nullable: false),
                        Anunciante_Id = c.String(maxLength: 128),
                        CompradorActual_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SubastaID)
                .ForeignKey("dbo.AspNetUsers", t => t.Anunciante_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CompradorActual_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.Anunciante_Id)
                .Index(t => t.CompradorActual_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
            CreateTable(
                "dbo.SubastaProductoes",
                c => new
                    {
                        Subasta_SubastaID = c.Int(nullable: false),
                        Producto_ProductoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subasta_SubastaID, t.Producto_ProductoID })
                .ForeignKey("dbo.Subastas", t => t.Subasta_SubastaID, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.Producto_ProductoID, cascadeDelete: true)
                .Index(t => t.Subasta_SubastaID)
                .Index(t => t.Producto_ProductoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subastas", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subastas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubastaProductoes", "Producto_ProductoID", "dbo.Productoes");
            DropForeignKey("dbo.SubastaProductoes", "Subasta_SubastaID", "dbo.Subastas");
            DropForeignKey("dbo.Subastas", "CompradorActual_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subastas", "Anunciante_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SubastaProductoes", new[] { "Producto_ProductoID" });
            DropIndex("dbo.SubastaProductoes", new[] { "Subasta_SubastaID" });
            DropIndex("dbo.Subastas", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Subastas", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Subastas", new[] { "CompradorActual_Id" });
            DropIndex("dbo.Subastas", new[] { "Anunciante_Id" });
            DropTable("dbo.SubastaProductoes");
            DropTable("dbo.Subastas");
        }
    }
}
