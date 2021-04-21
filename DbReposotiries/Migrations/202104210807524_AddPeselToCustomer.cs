namespace DbReposotiries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPeselToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Pesel", c => c.String(maxLength: 11, fixedLength: true, unicode: false));
            CreateIndex("dbo.Customers", "Pesel", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "Pesel" });
            DropColumn("dbo.Customers", "Pesel");
        }
    }
}
