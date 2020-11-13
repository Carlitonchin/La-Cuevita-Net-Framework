namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CantidadesComoEntidad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cantidades",
                c => new
                    {
                        CantidadesID = c.Int(nullable: false, identity: true),
                        valor = c.Int(nullable: false),
                        SubastaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CantidadesID)
                .ForeignKey("dbo.Subastas", t => t.SubastaID, cascadeDelete: true)
                .Index(t => t.SubastaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cantidades", "SubastaID", "dbo.Subastas");
            DropIndex("dbo.Cantidades", new[] { "SubastaID" });
            DropTable("dbo.Cantidades");
        }
    }
}
