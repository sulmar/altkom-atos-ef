namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBarCodeToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "BarCode", c => c.String(maxLength: 13, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "BarCode");
        }
    }
}
