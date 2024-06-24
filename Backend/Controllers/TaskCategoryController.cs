using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;

namespace TaskManager.Controllers
{
    [Route("TaskCategory")]
    public class TaskCategoryController : Controller
    {
        #region Fields

        private readonly ILookupService<TaskCategoryModel> _categoryService;

        #endregion

        #region Constructors

        public TaskCategoryController(ILookupService<TaskCategoryModel> categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Methods: Public

        [HttpGet]
        [Authorize]
        [Route("GetAll")]
        public async Task<ServiceResponse> GetAllTaskCategories()
        {
            return await _categoryService.GetAllLookups();   
        }

        [HttpGet]
        [Authorize]
        [Route("GetById")]
        public async Task<ServiceResponse> GetTaskCategoryById([FromQuery] Guid id)
        {
            return await GetTaskCategoryById(id);
        }

        [HttpPost]
        [Authorize]
        [Route("Update")]
        public async Task<ServiceResponse> UpdateTaskCategory([FromBody] TaskCategoryModel categoryModel)
        {
            return await UpdateTaskCategory(categoryModel);
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<ServiceResponse> CreateCategory([FromBody] TaskCategoryModel categoryModel)
        {
            return await _categoryService.CreateCategory(categoryModel);
        }

        #endregion
    }
}
