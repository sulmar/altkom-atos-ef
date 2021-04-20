namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityConvention : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "ShipAddress_City", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "InvoiceAddress_City", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "InvoiceAddress_City", c => c.String());
            AlterColumn("dbo.Customers", "ShipAddress_City", c => c.String());
        }
    }
}
