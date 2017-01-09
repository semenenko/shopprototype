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

		public DbSet<SalonFacilityTimeSlot> SalonFacilityTimeSlots { get; set; }

		public DbSet<SalonCategoryTimeSlot> SalonCategoryTimeSlots { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<SalonLocation>()
				.HasRequired(x => x.Salon)
				.WithOptional();

			modelBuilder.Entity<SalonFacility>()
				.HasKey(x => new { x.SalonId, x.FacilityId });

			modelBuilder.Entity<SalonFacility>()
				.HasRequired(x => x.Salon)
				.WithMany(x => x.Facilities)
				.HasForeignKey(x => x.SalonId);

			modelBuilder.Entity<SalonFacility>()
				.HasRequired(x => x.Facility)
				.WithMany()
				.HasForeignKey(x => x.FacilityId);
		}
	}
}
