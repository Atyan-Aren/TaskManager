using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
	[Route("Test")]
	public class TestController : Controller
	{
		#region Methods: Public

		[HttpGet]
		[Authorize]
		[Route("Test")]
		public string Test()
		{
			return "Arenchik is very beautiful";
		}

		#endregion
	}
}
