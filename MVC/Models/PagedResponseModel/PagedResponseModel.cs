namespace MVC.Models.PagedResponseModel
{
	public class PagedResponseModel<TModel> where TModel : class
	{
		public int? Id { get; set; }
		public TModel PagedModel { get; set; }
		public int PageNumber { get; set; }
		public int TotalPages { get; set; }
		public int TotalCount { get; set; }
	}
}
