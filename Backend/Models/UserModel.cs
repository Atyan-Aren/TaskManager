namespace TaskManager.Models
{
	public class UserModel : BaseModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string TelegramNickname { get; set; }
	}
}
