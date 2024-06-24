using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;
using TaskManager.Repositories.DbContexts;

namespace TaskManager.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {
        #region Fields

        private readonly ApplicationContext _applicationContext;

        #endregion

        #region Constructors

        public TaskController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        #endregion

        #region Methods: Public

        [HttpPost]
        [Authorize]
        [Route("CreateTask")]
        public async Task CreateTask([FromBody] ServiceResponse title)
        {
            var username = User.Claims.FirstOrDefault().Value;
            var user = await _applicationContext.Users.Where(user => user.Name == username).FirstOrDefaultAsync();
            var taskModel = new TaskModel<UserModel>()
            {
                Title = title.Message,
                Author = user
            };
            _applicationContext.Tasks.Add(taskModel);
            await _applicationContext.SaveChangesAsync();
        }

        [HttpGet]
        [Authorize]
        [Route("GetAll")]
        public async Task<List<string>> Login()
        {
            var tasks = await _applicationContext.Tasks.Where(task => task.Author.Name == User.Claims.FirstOrDefault().Value).ToListAsync();
            return tasks.Select(task => task.Title).ToList();
        }

        #endregion
    }
}
