namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStateToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "Estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "Estado");
        }
    }
}
