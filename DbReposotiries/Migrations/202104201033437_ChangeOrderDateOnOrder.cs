namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderDateOnOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String(nullable: false, maxLength: 20, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String());
        }
    }
}
