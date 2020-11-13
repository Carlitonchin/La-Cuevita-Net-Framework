namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubastasDateTimeNulleables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subastas", "tiempoPublicacion", c => c.DateTime());
            AlterColumn("dbo.Subastas", "tiempoUltimaPuja", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subastas", "tiempoUltimaPuja", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Subastas", "tiempoPublicacion", c => c.DateTime(nullable: false));
        }
    }
}
