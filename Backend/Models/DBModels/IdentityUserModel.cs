using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.DBModels
{
	[Table("User")]
	public class IdentityUserModel : IdentityUser
	{
		public string TelegramNickname { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
