namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomersByGenderView : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE VIEW dbo.CustomersByGender as select Gender, count(*) as Quantity from dbo.Customers group by Gender");
        }
        
        public override void Down()
        {
            Sql("DROP VIEW dbo.CustomersByGender");
        }
    }
}
