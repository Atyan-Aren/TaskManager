using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Models.DBModels;

namespace TaskManager.Repository.DbContexts
{
	public class ApplicationContextWithIdentity : IdentityDbContext<IdentityUser>
	{
		#region Properties: Public

		public DbSet<TaskModel<IdentityUser>> Tasks { get; set; }
		public DbSet<TaskCategoryModel> TaskCategories { get; set; }
		public DbSet<TaskPriorityModel> TaskPriorities { get; set; }
		public DbSet<TaskStatusModel> TaskStatuses { get; set; }

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
