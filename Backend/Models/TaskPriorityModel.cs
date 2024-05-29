namespace TaskManager.Models
{
	public class TaskPriorityModel : BaseModel
	{
		public string Name { get; set; }
		public int PriorityValue { get; set; }
	}
}
