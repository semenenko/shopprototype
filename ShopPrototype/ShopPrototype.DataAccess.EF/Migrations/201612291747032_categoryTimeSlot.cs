namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryTimeSlot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalonCategoryTimeSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalonId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalonCategoryTimeSlots");
        }
    }
}
