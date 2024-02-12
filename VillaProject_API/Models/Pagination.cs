namespace VillaProject_API.Models
{
	public record Pagination
	{
		public int PageNumber { get; set; }
		public int RecordsPerPage { get; set; }
	}
}
