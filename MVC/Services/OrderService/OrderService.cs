using Microsoft.Extensions.Options;
using MVC.Configuration;
using MVC.Models.OrderModels;
using MVC.Models.PagedResponseModel;
using MVC.Models.PaginationModel;
using MVC.Models.ResponseModels;
using System.Text.Json;

namespace MVC.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlConfiguration _urlConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(HttpClient httpClient, IOptions<UrlConfiguration> urlConfiguration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _urlConfiguration = urlConfiguration.Value;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri(_urlConfiguration.BaseUrl + _urlConfiguration.OrderUrl);
        }

        public async Task<ResponseModel<NoDataModel>> DeleteAsync(int id)
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

        public async Task<ResponseModel<IEnumerable<OrderModel>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/all");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var order = JsonSerializer.Deserialize<ResponseModel<IEnumerable<OrderModel>>>(content, options);
            return order;
        }

        public async Task<ResponseModel<OrderModel>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/get/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var order = JsonSerializer.Deserialize<ResponseModel<OrderModel>>(content, options);
            return order;
        }

        public async Task<ResponseModel<OrderModel>> UpdateAsync(OrderUpdateModel orderUpdateModel)
        {
            var response = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress, orderUpdateModel);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var order = JsonSerializer.Deserialize<ResponseModel<OrderModel>>(content, options);
            return order;
        }

        public async Task<ResponseModel<PagedResponseModel<IEnumerable<OrderModel>>>> GetPagedAsync(PaginationModel paginationModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/GetPaged", paginationModel);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var order = JsonSerializer.Deserialize<ResponseModel<PagedResponseModel<IEnumerable<OrderModel>>>>(content, options);
            order.Data.PageNumber = paginationModel.PageNumber;
            return order;
        }

        public async Task<int> GetPendingOrdersCountAsync()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/pending-orders-count");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Eğer JSON yanıtı bir sayı içeriyorsa, onu direkt integer olarak döndürüyoruz
            if (int.TryParse(content, out var pendingOrdersCount))
            {
                return pendingOrdersCount;
            }

            return 0;
        }

        public async Task<int> GetTodayOrdersCountAsync()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/today-orders-count");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Eğer JSON yanıtı bir sayı içeriyorsa, onu direkt integer olarak döndürüyoruz
            if (int.TryParse(content, out var todayOrdersCount))
            {
                return todayOrdersCount;
            }

            return 0;
        }

        public async Task<decimal> GetDailyEarningsAsync()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/daily-earnings");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Eğer JSON yanıtı decimal içeriyorsa, onu decimal olarak döndürüyoruz
            if (decimal.TryParse(content, out var dailyEarnings))
            {
                return dailyEarnings;
            }

            return 0;
        }
    }
}
