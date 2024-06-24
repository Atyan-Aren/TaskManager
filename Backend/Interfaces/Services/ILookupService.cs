using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
    public interface ILookupService<TLookupModel>
    {
        Task<ServiceResponse> GetAllLookups();
        Task<ServiceResponse> GetTaskCategoryById(Guid lookupId);
        Task<ServiceResponse> UpdateTaskCategory(TLookupModel lookupModel);
        Task<ServiceResponse> CreateCategory(TLookupModel lookupModel);
    }
}
