using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.DBModels
{
	[Table("TaskStatus")]
	public class TaskStatusModel : BaseModel
    {
        public string Name { get; set; }
    }
}
