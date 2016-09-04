namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class facility_duration_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facilities", "DurationMin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Facilities", "DurationMin");
        }
    }
}
