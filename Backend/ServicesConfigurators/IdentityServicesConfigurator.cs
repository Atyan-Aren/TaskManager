using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces.Services;
using TaskManager.Interfaces;
using TaskManager.Models.DBModels;
using TaskManager.Repositories.DbContexts;
using TaskManager.Services;

namespace TaskManager.ServicesConfigurators
{
	public class IdentityServicesConfigurator : IServicesConfigurator
	{
		#region Fields

		private IConfiguration _configuration;
		private IServiceCollection _services;

		#endregion

		#region Constructors

		public IdentityServicesConfigurator(IServiceCollection services, IConfiguration configuration)
		{
			_services = services;
			_configuration = configuration;
		}

		#endregion

		#region Methods: Public
		
		public IServicesConfigurator AddAuthorizationServices()
		{
			_services.AddIdentity<IdentityUserModel, IdentityRole>().AddEntityFrameworkStores<ApplicationContextWithIdentity>();
			_services.AddScoped<ILoginService, IdentityLoginService>();
			return this;
		}

		public IServicesConfigurator AddDBContext()
		{
			_services.AddDbContext<ApplicationContextWithIdentity>(options =>
				options.UseNpgsql(_configuration.GetConnectionString("IdentityConnection")));
			return this;
		}

		#endregion
	}
}
