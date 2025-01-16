namespace MVC.Models.PaginationModel
{
	public class PaginationModel
	{
		public int? Id { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
		public bool? IsActive { get; set; } = true;
		public bool WebStatus { get; set; } = true;
		public bool IsHandled { get; set; } = true;
	}
}
