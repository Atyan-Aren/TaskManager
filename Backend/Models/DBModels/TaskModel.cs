using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.DBModels
{
	[Table("Task")]
	public class TaskModel<TUser> : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasReminding { get; set; }
        public DateTime RemindingTime { get; set; }
        public DateTime DueDate { get; set; }
        public TUser Author { get; set; }
        public TaskPriorityModel TaskPriority { get; set; }
        public TaskCategoryModel TaskCategory { get; set; }
        public TaskStatusModel TaskStatus { get; set; }
    }
}
