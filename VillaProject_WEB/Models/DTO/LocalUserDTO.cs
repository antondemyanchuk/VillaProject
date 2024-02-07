namespace VillaProject_WEB.Models.DTO
{
	public record LocalUserDTO
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
	}
}
