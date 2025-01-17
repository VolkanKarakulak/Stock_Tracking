using Microsoft.Extensions.Options;
using MVC.Configuration;
using MVC.Models.CategoryModels;
using MVC.Models.PagedResponseModel;
using MVC.Models.PaginationModel;
using MVC.Models.ResponseModels;
using Service.DTOs.CategoryDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using System.Text.Json;

namespace MVC.Services.CategoryServices
{
	public class CategoryService : ICategoryService
	{
		private readonly HttpClient _httpClient;
		private readonly UrlConfiguration _urlConfiguration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CategoryService(HttpClient httpClient, IOptions<UrlConfiguration> urlConfiguration, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClient;
			_urlConfiguration = urlConfiguration.Value;
			_httpContextAccessor = httpContextAccessor;
			_httpClient.BaseAddress = new Uri(_urlConfiguration.BaseUrl + _urlConfiguration.CategoryUrl);
		}

		public async Task<ResponseModel<CategoryModel>> CreateAsync(CategoryAddModel categoryAddModel)
		{
			var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, categoryAddModel);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var category = JsonSerializer.Deserialize<ResponseModel<CategoryModel>>(content, options);
			return category;
		}

		public async  Task<ResponseModel<NoDataModel>> DeleteAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{id}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var responseModel = new ResponseModel<NoDataModel>();
			using (JsonDocument doc = JsonDocument.Parse(content))
			{
				int StatusCode = doc.RootElement.GetProperty("statusCode").GetInt32();
				responseModel.StatusCode = StatusCode;
				return responseModel;
			}
		}

		public async Task<ResponseModel<IEnumerable<CategoryModel>>> GetAllAsync()
		{
			var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/all");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var categories = JsonSerializer.Deserialize<ResponseModel<IEnumerable<CategoryModel>>>(content, options);
			return categories;
		}

		public async Task<ResponseModel<CategoryModel>> GetByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/get/{id}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var category = JsonSerializer.Deserialize<ResponseModel<CategoryModel>>(content, options);
			return category;
		}

		public async Task<ResponseModel<CategoryModel>> UpdateAsync(CategoryUpdateModel categoryUpdateModel)
		{
			var response = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress, categoryUpdateModel);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var category = JsonSerializer.Deserialize<ResponseModel<CategoryModel>>(content, options);
			return category;
		}

		public async Task<ResponseModel<PagedResponseModel<IEnumerable<CategoryModel>>>> GetPagedAsync(PaginationModel paginationModel)
		{
			var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/GetPaged", paginationModel);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var category = JsonSerializer.Deserialize<ResponseModel<PagedResponseModel<IEnumerable<CategoryModel>>>>(content, options);
			category.Data.PageNumber = paginationModel.PageNumber;
			return category;
		}
	}
}
