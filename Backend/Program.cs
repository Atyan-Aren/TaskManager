using Microsoft.AspNetCore.Authentication.Cookies;
using TaskManager.ServicesExtensions;

namespace TaskManager
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			builder.Services.AddDBContextByConfig(builder.Configuration);
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => options.LoginPath = new PathString("/Login/Login"));
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