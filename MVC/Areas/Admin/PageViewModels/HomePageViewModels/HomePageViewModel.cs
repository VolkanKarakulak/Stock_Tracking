using Data.Entities;
using MVC.Models.OrderModels;

namespace MVC.Areas.Admin.PageViewModels.HomePageViewModels
{
    public class HomePageViewModel
    {

        public int PendingOrdersCount { get; set; }
        public int TodayOrdersCount { get; set; }
        public string DailyEarnings {  get; set; }
        public int TotalOrdersCount { get; set; }
        public List<Earning> MonthlyEarnings { get; set; }
        public IEnumerable<LastTenOrdersModel> LastTenOrders { get; set; }


    }
}
