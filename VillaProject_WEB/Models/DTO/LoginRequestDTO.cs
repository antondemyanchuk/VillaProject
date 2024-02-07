namespace VillaProject_WEB.Models.DTO
{
	public record LoginRequestDTO
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
