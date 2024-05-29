using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Repository.DbContexts;

namespace TaskManager
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<ApplicationContext>(options => 
				options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));

			builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

			builder.Services.AddControllers();

			var app = builder.Build();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.MapGet("/", (ApplicationContext db) => db.Users.ToList());
			app.Run();
		}
	}
}