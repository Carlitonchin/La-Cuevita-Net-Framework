namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComprasImporte : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "Importe", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "Importe");
        }
    }
}
