using Microsoft.EntityFrameworkCore;
using TaskManager.Models.DBModels;

namespace TaskManager.Repositories.DbContexts
{
    public class ApplicationContext : DbContext
	{
		#region Properties: Public

		public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel<UserModel>> Tasks { get; set; }
        public DbSet<TaskCategoryModel> TaskCategories { get; set; }
        public DbSet<TaskPriorityModel> TaskPriorities { get; set; }
        public DbSet<TaskStatusModel> TaskStatuses { get; set; }

        #endregion

        #region Constructors: Public

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
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
