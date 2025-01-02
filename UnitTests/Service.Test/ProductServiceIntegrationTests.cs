using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Service.Services.ProductService;
using Data.Repositories.ProductRepositories;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.UnitOfWorks;
using AutoMapper;
using Service.DTOs.ProductDtos;
using Data.Entities;
using Service.DTOs.PaginationDto;
using Data.Contexts;
using Data.Repositories.GenericRepositories;
using Moq;
using Service.Mapping;

public class ProductServiceIntegrationTests
{
    private readonly Stock_TrackingDbContext _context; 
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductServiceIntegrationTests()
    {
        // In-Memory Database oluþturma
        var options = new DbContextOptionsBuilder<Stock_TrackingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new Stock_TrackingDbContext(options);


       

        // Veritabanýna test verilerini ekleme
        _context.Categories.AddRange(new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Appliances" }
        });
        _context.SaveChanges();

        // Mock yerine gerçek bir GenericRepository kullanýmý
        var productRepository = new ProductRepository(new GenericRepository<Product>(_context), _context);


        // CategoryRepository'nin oluþturulmasý
        var categoryRepository = new CategoryRepository(new GenericRepository<Category>(_context), _context);

        // ProductStockRepository'nin oluþturulmasý
        var productStockRepository = new ProductStockRepository(new GenericRepository<ProductStock>(_context), _context);


        var unitOfWork = new UnitOfWork(_context);

       

        // Servisi oluþturma
        _productService = new ProductService(
            productRepository,
            categoryRepository,
            unitOfWork,
            productStockRepository,
            _mapper
        );
    }

    [Fact]
    public async Task CreateProductAsync_ShouldCreateProductWithCategoriesAndStock()
    {
        var category1 = await _context.Categories.FirstOrDefaultAsync(c => c.Id == 1);
        var category2 = await _context.Categories.FirstOrDefaultAsync(c => c.Id == 2);


        if (category1 == null || category2 == null)
        {
            throw new Exception("Kategori tablosunda gerekli veriler mevcut deðil.");
        }

 

        var productAddDto = new ProductAddDto
        {
            Name = "Smartphone",
            Description = "Latest model",
            Color = "Black",
            Material = "Koton",
            Price = 9990,
            Stock = 100,
            CategoryIds = new List<int> { 1, 2 }
        };

        // Act
        var result = await _productService.CreateProductAsync(productAddDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Smartphone", result.Name);
        Assert.Equal(100, result.Stock);
        Assert.Contains(1, result.CategoryIds);
        Assert.Contains(2, result.CategoryIds);

        var productInDb = await _context.Products
        .Include(p => p.ProductCategories)
        .ThenInclude(pc => pc.Category)
        .FirstOrDefaultAsync();

        Assert.NotNull(productInDb);
        Assert.Equal(2, productInDb.ProductCategories.Count);
        Assert.Contains(productInDb.ProductCategories, pc => pc.CategoryId == 1);
        Assert.Contains(productInDb.ProductCategories, pc => pc.CategoryId == 2);
    }

    //[Fact]
    //public async Task UpdateProductAsync_ShouldUpdateProductDetails()
    //{
    //    // Arrange
    //    var product = new Product { Id = 1, Name = "Laptop", Description = "Old model", Price = 899990, Stock = 50 };
    //    var category = new Category { Id = 1, Name = "Electronics" };
    //    _context.Products.Add(product);
    //    _context.Categories.Add(category);
    //    await _context.SaveChangesAsync();

    //    var productUpdateDto = new ProductUpdateDto
    //    {
    //        Id = 1,
    //        Name = "Gaming Laptop",
    //        Description = "High-performance model",
    //        Color = "Red",
    //        Material = "Hard",
    //        Price = 12990,
    //        Stock = 30,
    //        CategoryIds = new List<int> { 1 }
    //    };

    //    // Act
    //    var result = await _productService.UpdateProductAsync(productUpdateDto);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal("Gaming Laptop", result.Name);
    //    Assert.Equal(30, result.Stock);
    //}

    //[Fact]
    //public async Task GetProductsByCategoryIdPagedAsync_ShouldReturnPagedProducts()
    //{
    //    // Arrange
    //    var category = new Category { Id = 1, Name = "Electronics" };
    //    var products = new List<Product>
    //    {
    //        new Product { Id = 1, Name = "Phone", Description = "Latest phone", Price = 79990, Stock = 50 },
    //        new Product { Id = 2, Name = "TV", Description = "Smart TV", Price = 1199990, Stock = 20 }
    //    };

    //    _context.Categories.Add(category);
    //    _context.Products.AddRange(products);
    //    await _context.SaveChangesAsync();

    //    var paginationDto = new PaginationDto { PageNumber = 1, PageSize = 3 };

    //    // Act
    //    var result = await _productService.GetProductsByCategoryIdPagedAsync(1, paginationDto);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(2, result.PagedDto.Count());
    //}
}
