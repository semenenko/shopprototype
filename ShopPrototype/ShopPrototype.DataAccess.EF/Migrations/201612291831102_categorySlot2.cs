namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categorySlot2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonCategoryTimeSlots", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalonCategoryTimeSlots", "CategoryId");
        }
    }
}
