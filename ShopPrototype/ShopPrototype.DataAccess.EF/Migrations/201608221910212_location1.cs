namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalonLocations",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SalonId = c.Int(nullable: false),
                        Location = c.Geography(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Salons", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalonLocations", "Id", "dbo.Salons");
            DropIndex("dbo.SalonLocations", new[] { "Id" });
            DropTable("dbo.SalonLocations");
        }
    }
}
