namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompraUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "Estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "Estado");
        }
    }
}
