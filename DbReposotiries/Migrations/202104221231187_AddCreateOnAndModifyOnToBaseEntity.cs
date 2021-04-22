namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateOnAndModifyOnToBaseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CreateOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Customers", "ModifyOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Orders", "CreateOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Orders", "ModifyOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OrderDetails", "CreateOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OrderDetails", "ModifyOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Items", "CreateOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Items", "ModifyOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ModifyOn");
            DropColumn("dbo.Items", "CreateOn");
            DropColumn("dbo.OrderDetails", "ModifyOn");
            DropColumn("dbo.OrderDetails", "CreateOn");
            DropColumn("dbo.Orders", "ModifyOn");
            DropColumn("dbo.Orders", "CreateOn");
            DropColumn("dbo.Customers", "ModifyOn");
            DropColumn("dbo.Customers", "CreateOn");
        }
    }
}
