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

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.Entries()
                .Where(entity => entity.State == EntityState.Added || entity.State == EntityState.Modified)
                .ToList().ForEach(entity =>
                {
                    if (entity.State == EntityState.Added)
                        entity.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                    entity.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
                });

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion
    }
}
