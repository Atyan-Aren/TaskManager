using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Middlewares;
using TaskManager.Models.DBModels;
using TaskManager.Repositories.DbContexts;
using TaskManager.Services;
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

			builder.Services.AddScoped<Interfaces.Repositories.ILookupRepository<TaskCategoryModel>, TaskCategoryRepository>();
			builder.Services.AddScoped<Interfaces.Services.ILookupService<TaskCategoryModel>, TaskCategoriesService>();
			builder.Services.AddControllers();

			var app = builder.Build();

			app.UseMiddleware<GlobalExceptionMiddlware>();
			app.UseRouting();
			
			//TODO: Non unterstandable
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.Run();
		}
	}
}