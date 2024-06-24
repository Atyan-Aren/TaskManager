using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models.DBModels;

namespace TaskManager.Repositories.DbContexts
{
    public class TaskCategoryRepository : ILookupRepository<TaskCategoryModel>
    {
        #region Fields

        private ApplicationContext _applicationContext;

        #endregion

        #region Constructors

        public TaskCategoryRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        #endregion

        #region Methods: Public

        public async Task<TaskCategoryModel> CreateLookup(string name)
        {
            var category = new TaskCategoryModel()
            {
                Name = name,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };
            _applicationContext.TaskCategories.Add(category);
            var insertedCount = await _applicationContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<TaskCategoryModel>> GetAllLookups()
        {
            var categories = await _applicationContext.TaskCategories.ToListAsync();
            return categories;
        }

        public async Task<TaskCategoryModel> GetLookupById(Guid id)
        {
            var category = await _applicationContext.TaskCategories.FirstOrDefaultAsync(category => category.Id == id);
            return category;
        }

        public async Task<TaskCategoryModel> UpdateLookup(Guid id, string name)
        {
            var category = await _applicationContext.TaskCategories.FirstOrDefaultAsync(category => category.Id == id);
            category.Name = name;
            var insertedCount = await _applicationContext.SaveChangesAsync();
            return category;
        }

        #endregion
    }
}
