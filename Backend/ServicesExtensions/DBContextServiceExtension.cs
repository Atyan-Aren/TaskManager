using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DBModels;
using TaskManager.Repository.DbContexts;
using TaskManager.Services;

namespace TaskManager.ServicesExtensions
{
	public static class DBContextServiceExtension
	{
		public static void AddDBContextByConfig(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration.GetValue<string>("AuthorizationMethod") == "Identity")
			{
				services.AddDbContext<ApplicationContextWithIdentity>(options =>
					options.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));

				services.AddIdentity<IdentityUserModel, IdentityRole>().AddEntityFrameworkStores<ApplicationContextWithIdentity>();
				services.AddScoped<ILoginService, IdentityLoginService>();
			}
			else if (configuration.GetValue<string>("AuthorizationMethod") == "Custom")
			{
				services.AddDbContext<ApplicationContext>(options =>
					options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
				services.AddScoped<ILoginService, LoginService>();
				services.AddTransient<IPasswordService, PasswordService>();
			}
		}
	}
}
