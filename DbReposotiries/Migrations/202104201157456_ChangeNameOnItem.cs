namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameOnItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Name", c => c.String());
        }
    }
}
