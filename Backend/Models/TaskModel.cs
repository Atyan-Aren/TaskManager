namespace TaskManager.Models
{
	public class TaskModel : BaseModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public bool HasReminding { get; set; }
		public DateTime RemindingTime { get; set; }
		public DateTime DueDate { get; set; }
		public UserModel Author { get; set; }
		public TaskPriorityModel TaskPriority { get; set; }
		public TaskCategoryModel TaskCategory { get; set; }
		public TaskStatusModel TaskStatus { get; set; }
	}
}
