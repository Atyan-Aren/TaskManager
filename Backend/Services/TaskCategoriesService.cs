using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;

namespace TaskManager.Services
{
    public class TaskCategoriesService : ILookupService<TaskCategoryModel>
    {
        #region Fields

        private readonly ILookupRepository<TaskCategoryModel> _categoryRepository;

        #endregion

        #region Constructors

        public TaskCategoriesService(ILookupRepository<TaskCategoryModel> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion

        #region Methods: Public

        public async Task<ServiceResponse> CreateCategory(TaskCategoryModel lookupModel)
        {
            var result = new ServiceResponse<TaskCategoryModel>() { Success = true };
            var category = await _categoryRepository.CreateLookup(lookupModel.Name);
            result.Data = category;
            return result;
        }

        public async Task<ServiceResponse> GetAllLookups()
        {
            var result = new ServiceResponse<IEnumerable<TaskCategoryModel>>() { Success = true };
            var categories = await _categoryRepository.GetAllLookups();
            result.Data = categories;
            return result;
        }

        public async Task<ServiceResponse> GetTaskCategoryById(Guid lookupId)
        {
            var result = new ServiceResponse<TaskCategoryModel>() { Success = true };
            var category = await _categoryRepository.GetLookupById(lookupId);
            result.Data = category;
            return result;
        }

        public async Task<ServiceResponse> UpdateTaskCategory(TaskCategoryModel lookupModel)
        {
            var result = new ServiceResponse<TaskCategoryModel>() { Success = true };
            var category = await _categoryRepository.UpdateLookup(lookupModel.Id, lookupModel.Name);
            result.Data = category;
            return result;
        }

        #endregion
    }
}
