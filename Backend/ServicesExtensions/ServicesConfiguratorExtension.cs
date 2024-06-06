using TaskManager.Interfaces;
using TaskManager.ServicesConfigurators;

namespace TaskManager.ServicesExtensions
{
	public static class ServicesConfiguratorExtension
	{
		public static IServicesConfigurator GetServicesConfigurator(this IServiceCollection services, IConfiguration configuration)
		{
			var authorizationMethod = configuration.GetValue<string>("AuthorizationMethod");
			if (authorizationMethod == "Identity")
				return new IdentityServicesConfigurator(services, configuration);
			else if (authorizationMethod == "Custom")
				return new CustomServicesConfigurator(services, configuration);
			throw new NotImplementedException();
		}
	}
}
