namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salonLocation_Id : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SalonLocations", "SalonId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalonLocations", "SalonId", c => c.Int(nullable: false));
        }
    }
}
