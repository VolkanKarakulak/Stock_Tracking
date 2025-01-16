namespace MVC.Models.CategoryModels
{
	public class CategoryModel
	{
		public int Id { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
	}
}
