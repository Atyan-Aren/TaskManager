namespace TaskManager.Models
{
	[Serializable]
	public class ServiceResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}

	[Serializable]
	public class ServiceResponse<T> : ServiceResponse where T : class
	{
		public T Data { get; set; }
	}
}
