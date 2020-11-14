namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameInReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "UsuarioQueReporta", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "UsuarioQueReporta");
        }
    }
}
