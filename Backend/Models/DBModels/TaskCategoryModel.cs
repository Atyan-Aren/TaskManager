using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.DBModels
{
	[Table("TaskCategory")]
	public class TaskCategoryModel : BaseModel
    {
        public string Name { get; set; }
    }
}
