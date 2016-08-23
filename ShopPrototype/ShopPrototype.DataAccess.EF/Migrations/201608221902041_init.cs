namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SortOrder = c.Int(nullable: false),
                        FacilityCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FacilityCategories", t => t.FacilityCategoryId, cascadeDelete: true)
                .Index(t => t.FacilityCategoryId);
            
            CreateTable(
                "dbo.FacilityCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facilities", "FacilityCategoryId", "dbo.FacilityCategories");
            DropIndex("dbo.Facilities", new[] { "FacilityCategoryId" });
            DropTable("dbo.Salons");
            DropTable("dbo.FacilityCategories");
            DropTable("dbo.Facilities");
        }
    }
}
