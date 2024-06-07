using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces.Services;
using TaskManager.Interfaces;
using TaskManager.Repositories.DbContexts;
using TaskManager.Services;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models.DBModels;
using TaskManager.Repositories;

namespace TaskManager.ServicesConfigurators
{
	public class CustomServicesConfigurator : IServicesConfigurator
	{
		#region Fields

		private IConfiguration _configuration;
		private IServiceCollection _services;

		#endregion

		#region Constructors

		public CustomServicesConfigurator(IServiceCollection services, IConfiguration configuration)
		{
			_services = services;
			_configuration = configuration;
		}

		#endregion

		#region Methods: Public
		
		public IServicesConfigurator AddAuthorizationServices()
		{
			_services.AddScoped<IUserRepository<UserModel>, CustomUserRepository>();
			_services.AddScoped<ILoginService, LoginService>();
			_services.AddTransient<IPasswordService, PasswordService>();
			_services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.Cookie.Name = "TaskManagerCookie";
				//options.LoginPath = ""
				options.ExpireTimeSpan = TimeSpan.FromDays(30);
			});
			return this;
		}

		public IServicesConfigurator AddDBContext()
		{
			_services.AddDbContext<ApplicationContext>(options =>
				options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
			return this;
		}

		#endregion
	}
}
