using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.DBModels
{
	[Table("TaskPriority")]
	public class TaskPriorityModel : BaseModel
    {
        public string Name { get; set; }
        public int PriorityValue { get; set; }
    }
}
