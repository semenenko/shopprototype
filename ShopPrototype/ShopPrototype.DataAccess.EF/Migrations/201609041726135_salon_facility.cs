namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salon_facility : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalonFacilities",
                c => new
                    {
                        SalonId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        DurationMin = c.Int(),
                    })
                .PrimaryKey(t => new { t.SalonId, t.FacilityId })
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .ForeignKey("dbo.Salons", t => t.SalonId, cascadeDelete: true)
                .Index(t => t.SalonId)
                .Index(t => t.FacilityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalonFacilities", "SalonId", "dbo.Salons");
            DropForeignKey("dbo.SalonFacilities", "FacilityId", "dbo.Facilities");
            DropIndex("dbo.SalonFacilities", new[] { "FacilityId" });
            DropIndex("dbo.SalonFacilities", new[] { "SalonId" });
            DropTable("dbo.SalonFacilities");
        }
    }
}
