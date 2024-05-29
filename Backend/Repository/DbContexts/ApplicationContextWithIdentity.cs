using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Repository.DbContexts
{
	public class ApplicationContextWithIdentity : DbContext
	{
		#region Properties: Public
		public DbSet<IdentityUser> Users { get; set; }

		#endregion

		#region Constructors: Public

		public ApplicationContextWithIdentity(DbContextOptions<ApplicationContextWithIdentity> options) : base(options)
		{
			Database.EnsureCreated();
		}

		#endregion

		#region Methods: Protected

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<BaseModel>().Property(model => model.CreatedDate).ValueGeneratedOnAdd();
		//	modelBuilder.Entity<BaseModel>().Property(model => model.UpdatedDate).ValueGeneratedOnAddOrUpdate();
		//}

		#endregion
	}
}
