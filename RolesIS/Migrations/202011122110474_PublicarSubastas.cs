namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublicarSubastas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subastas", "CuentaDelComprador", c => c.String());
            AddColumn("dbo.Subastas", "CuentaDelVendedor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subastas", "CuentaDelVendedor");
            DropColumn("dbo.Subastas", "CuentaDelComprador");
        }
    }
}
