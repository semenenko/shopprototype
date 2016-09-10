namespace ShopPrototype.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Salon_data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salons", "AdministrativeArea", c => c.String());
            AddColumn("dbo.Salons", "District", c => c.String());
            AddColumn("dbo.Salons", "Address", c => c.String());
            AddColumn("dbo.Salons", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salons", "Phone");
            DropColumn("dbo.Salons", "Address");
            DropColumn("dbo.Salons", "District");
            DropColumn("dbo.Salons", "AdministrativeArea");
        }
    }
}
