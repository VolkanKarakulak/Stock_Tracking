using Data.Entities;
using MVC.Models.OrderModels;
using MVC.Models.PagedResponseModel;
using MVC.Models.PaginationModel;
using MVC.Models.ResponseModels;

namespace MVC.Services.OrderService
{
    public interface IOrderService
    {
        Task<ResponseModel<OrderModel>> GetByIdAsync(int id);
        Task<ResponseModel<IEnumerable<OrderModel>>> GetAllAsync();
        Task<ResponseModel<OrderModel>> UpdateAsync(OrderUpdateModel model);
        Task<ResponseModel<NoDataModel>> DeleteAsync(int id);
        Task<ResponseModel<PagedResponseModel<IEnumerable<OrderModel>>>> GetPagedAsync(PaginationModel paginationModel);
        Task<int> GetPendingOrdersCountAsync();
        Task<int> GetTodayOrdersCountAsync();
        Task<decimal> GetDailyEarningsAsync();
        Task<List<Earning>> CalculateMonthlyEarningsAsync();
        Task<int> GetTotalOrdersAsync();
        Task<IEnumerable<LastTenOrdersModel>> GetLastTenOrdersAsync();
    }
}
