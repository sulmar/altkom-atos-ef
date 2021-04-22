namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGetProductsByColor : DbMigration
    {
        public override void Up()
        {
            string sql = "select * from dbo.Items where Discriminator = 'Product' and Color = @Color";

            CreateStoredProcedure("GetProductsByColor", p => new
            {
                Color = p.String(),
            }, sql );
        }
        
        public override void Down()
        {
            DropStoredProcedure("GetProductsByColor");
        }
    }
}
