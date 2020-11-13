namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubastasUpgrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subastas", "Nombre", c => c.String());
            AddColumn("dbo.Subastas", "Descripcion", c => c.String());
            AddColumn("dbo.Subastas", "PrecioActual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subastas", "PrecioActual");
            DropColumn("dbo.Subastas", "Descripcion");
            DropColumn("dbo.Subastas", "Nombre");
        }
    }
}
