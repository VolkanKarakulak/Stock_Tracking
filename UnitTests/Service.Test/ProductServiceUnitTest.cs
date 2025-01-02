using AutoMapper;
using FluentAssertions;
using Moq;
using Service.DTOs.ProductDtos;
using Service.Services.ProductService;
using Xunit;
using Data.Repositories.ProductRepositories;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.UnitOfWorks;
using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Mapping;

namespace Service.Test
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IProductStockRepository> _productStockRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _productStockRepositoryMock = new Mock<IProductStockRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            // Gerçek Mapper Konfigürasyonu
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            _mapper = config.CreateMapper();

            _productService = new ProductService(
                _productRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _productStockRepositoryMock.Object,
                _mapper
            );
        }

        [Fact]
        public async Task CreateProductAsync_ShouldReturnCreatedProduct()
        {
            // Arrange
            var productAddDto = new ProductAddDto
            {
                Name = "Yamaha",
                Price = 100,
                Stock = 50,
                Color = "Black",
                Material = "Koton",
                CategoryIds = new List<int> { 1, 2 }
            };

            var product = new Product
            {
                Id = 1,
                Name = "Yamaha",
                Price = 100,
                Stock = 50,
                Color = "Black",
                Material = "Koton",
                IsActive = true,
                ProductCategories = new List<ProductCategory>
            {
                new ProductCategory { CategoryId = 1 },
                new ProductCategory { CategoryId = 2 }
            }
            };

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Appliances" }
            };

            _productRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<Product>())).ReturnsAsync(product);
            _categoryRepositoryMock.Setup(m => m.GetByIdsAsync(productAddDto.CategoryIds))
               .ReturnsAsync(categories);
            _unitOfWorkMock.Setup(m => m.CommitAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _productService.CreateProductAsync(productAddDto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Yamaha");
            result.Stock.Should().Be(50);
            result.Price.Should().Be(100);

            _productRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(m => m.CommitAsync(), Times.AtLeastOnce);
        }

        
    }

}
