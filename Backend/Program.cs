using TaskManager.ServicesExtensions;

namespace TaskManager
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			builder.Services.GetServicesConfigurator(builder.Configuration)
				.AddDBContext()
				.AddAuthorizationServices();

			builder.Services.AddControllers();

			var app = builder.Build();

			app.UseRouting();
			
			//TODO: Non unterstandable
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.Run();
		}
	}
}