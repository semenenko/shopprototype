namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salon_coord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salons", "Lat", c => c.Double(nullable: false));
            AddColumn("dbo.Salons", "Long", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salons", "Long");
            DropColumn("dbo.Salons", "Lat");
        }
    }
}
