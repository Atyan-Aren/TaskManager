using Microsoft.EntityFrameworkCore;
using TaskManager.ServicesExtensions;

namespace TaskManager
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			builder.Services.AddDBContextByConfig(builder.Configuration);
			builder.Services.AddControllers();

			var app = builder.Build();

			app.UseRouting();
			
			//TODO: Non unterstandable
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			//app.MapGet("/", (ApplicationContextWithIdentity db) => db.Users.ToList());
			app.Run();
		}
	}
}