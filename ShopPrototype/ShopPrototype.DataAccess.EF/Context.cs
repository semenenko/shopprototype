using ShopPrototype.DataAccess.EF.SpecificEntities;
using ShopPrototype.Modules.Entities;
using System.Data.Entity;

namespace ShopPrototype.DataAccess.EF
{
	public class Context : DbContext
	{
		public Context() : base("DefaultConnection")
		{ }

		public DbSet<Salon> Salons { get; set; }

		public DbSet<SalonLocation> Locations { get; set; }

		public DbSet<FacilityCategory> FacilityCategories { get; set; }

		public DbSet<Facility> Facilities { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<SalonLocation>()
				.HasRequired(x => x.Salon)
				.WithOptional();
		}
	}
}
