namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGetProductsByColor : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetProductsByColor", "select * from dbo.Items where Discriminator = 'Product' and Color = @Color", new { Color = "" } );
        }
        
        public override void Down()
        {
            DropStoredProcedure("GetProductsByColor");
        }
    }
}
