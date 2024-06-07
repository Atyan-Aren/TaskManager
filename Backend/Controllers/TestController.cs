using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.DBModels;
using TaskManager.Repositories.DbContexts;

namespace TaskManager.Controllers
{
	[Route("Test")]
	public class TestController : Controller
	{
		private ApplicationContext _applicationContext;
		public TestController(ApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}

		#region Methods: Public

		[HttpGet]
		[Route("GetUsers")]
		public List<UserModel> GetUsers()
		{
			return _applicationContext.Users.ToList();
		}

		[HttpGet]
		[Authorize]
		[Route("TEST")]
		public string TEST()
		{
			return "Arenchik is so beautiful";
		}

		#endregion
	}
}
