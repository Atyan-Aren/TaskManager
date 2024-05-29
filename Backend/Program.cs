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

			//TODO: Вынести
			if (builder.Configuration.GetValue<string>("AuthorizationMethod") == "Identity")
			{
				builder.Services.AddDbContext<ApplicationContextWithIdentity>(options => 
					options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));

				builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContextWithIdentity>();
			}
			else if (builder.Configuration.GetValue<string>("AuthorizationMethod") == "Custom")
			{
				builder.Services.AddDbContext<ApplicationContext>(options =>
					options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));
			}


			builder.Services.AddControllers();

			var app = builder.Build();

			app.UseRouting();
			
			//TODO: Non unterstandable
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.MapGet("/", (ApplicationContextWithIdentity db) => db.Users.ToList());
			app.Run();
		}
	}
}