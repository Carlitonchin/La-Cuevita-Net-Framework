namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "Category_Clothing", c => c.String());
            AddColumn("dbo.Productoes", "Category_Accessories", c => c.String());
            AddColumn("dbo.Productoes", "Category_HomeAndDecor", c => c.String());
            AddColumn("dbo.Productoes", "Category_MoviesANdMusic", c => c.String());
            AddColumn("dbo.Productoes", "Category_Games", c => c.String());
            AddColumn("dbo.Productoes", "Category_BooksAndMagazines", c => c.String());
            AddColumn("dbo.Productoes", "Category_Handicraft", c => c.String());
            AddColumn("dbo.Productoes", "Category_Electronics", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "Category_Electronics");
            DropColumn("dbo.Productoes", "Category_Handicraft");
            DropColumn("dbo.Productoes", "Category_BooksAndMagazines");
            DropColumn("dbo.Productoes", "Category_Games");
            DropColumn("dbo.Productoes", "Category_MoviesANdMusic");
            DropColumn("dbo.Productoes", "Category_HomeAndDecor");
            DropColumn("dbo.Productoes", "Category_Accessories");
            DropColumn("dbo.Productoes", "Category_Clothing");
        }
    }
}
