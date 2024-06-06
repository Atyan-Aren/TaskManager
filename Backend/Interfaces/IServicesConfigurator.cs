namespace TaskManager.Interfaces
{
	public interface IServicesConfigurator
	{
		IServicesConfigurator AddDBContext();
		IServicesConfigurator AddAuthorizationServices();
	}
}
