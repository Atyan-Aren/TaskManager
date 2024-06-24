namespace TaskManager.Interfaces.Repositories
{
    public interface ILookupRepository<TLookupModel> where TLookupModel : class
    {
        Task<IEnumerable<TLookupModel>> GetAllLookups();
        Task<TLookupModel> GetLookupById(Guid id);
        Task<TLookupModel> CreateLookup(string name);
        Task<TLookupModel> UpdateLookup(Guid id, string name);
    }
}
