namespace MVC.Models.CategoryModels
{
	public class CategoryUpdateModel
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
