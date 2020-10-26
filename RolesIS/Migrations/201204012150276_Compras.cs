namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Compras : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        CompraID = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Cuenta = c.String(),
                        ProductoID = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CompraID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoID, cascadeDelete: true)
                .Index(t => t.ProductoID)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compras", "ProductoID", "dbo.Productoes");
            DropForeignKey("dbo.Compras", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Compras", new[] { "Id" });
            DropIndex("dbo.Compras", new[] { "ProductoID" });
            DropTable("dbo.Compras");
        }
    }
}
