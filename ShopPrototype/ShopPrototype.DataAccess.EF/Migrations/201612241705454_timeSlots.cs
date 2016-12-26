namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeSlots : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalonFacilityTimeSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalonId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        SlotStartsAt = c.DateTime(nullable: false),
                        SlotEndsAt = c.DateTime(nullable: false),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalonFacilityTimeSlots");
        }
    }
}
