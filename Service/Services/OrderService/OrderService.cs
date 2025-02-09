using AutoMapper;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.OrderRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.Repositories.TaxSettingRepositories;
using Data.UnitOfWorks;
using Service.DTOs.OrderDtos;
using Service.Helper;
using Service.Services.GenericService;

namespace Service.Services.OrderService
{
    public class OrderService : GenericService<Order, OrderDto>, IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITaxSettingRepository _taxSettingRepository;
        private readonly IProductStockRepository _productStockRepository;
        private readonly IProductRepository _productRepository;


        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository, ITaxSettingRepository taxSettingRepository, IProductStockRepository productStockRepository, IProductRepository productRepository) : base(repository, unitOfWork, mapper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _taxSettingRepository = taxSettingRepository;
            _productStockRepository = productStockRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Earning>> CalculateMonthlyEarningsAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            var earnings = orders
                .GroupBy(o => new { o.CreatedDate.Year, o.CreatedDate.Month })
                .Select(g => new Earning
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Amount = g.Sum(o => o.TotalAmount) // Örnek hesaplama
                })
                .ToList();

            return earnings;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderAddDto dto)
		{
            //var customer = await _customerRepository.GetByIdAsync(orderRequest.CustomerId);
            //if (customer == null)
            //    return ServiceResponse<OrderResponseDto>.Fail("Customer not found");

            var taxRate = await _taxSettingRepository.GetTaxRateAsync();

			var order = _mapper.Map<Order>(dto);

			if (order == null)
			{
				throw new InvalidOperationException();
			}

			// Sipariş numarasını oluşturuyoruz (örn. "ORD-20250107-001").
			order.TrackingNumber = GenerateOrderNumber.CreateOrderNumber();

			// 'TotalAmount' hesaplamasını burada yapıyoruz.
			var totalAmount = order.TotalAmount = dto.Items.Sum(item => item.Quantity * item.UnitPrice);
			order.TaxAmount = totalAmount * taxRate;

			// OrderDetails oluşturma
			var orderDetails = dto.Items.Select(item => _mapper.Map<OrderDetail>(item)).ToList();

            //// Sipariş objesini veritabanına kaydediyoruz, ödeme yapılmadı olarak işaretliyoruz
            //order.IsPaid = false;  // Ödeme yapılmamış olarak varsayıyoruz
            //order.PaymentDate = null;
            //order.PaymentMethod = null;

            var createdOrder = await _orderRepository.CreateAsync(order);
			await _unitOfWork.CommitAsync();

            //// Eğer ödeme yapılmışsa, ödeme işlemini gerçekleştiriyoruz
            //if (dto.IsPaid)
            //{
            //    await _paymentService.PayForOrderAsync(createdOrder.Id, dto.PaymentMethod ?? "Unknown");
            //}

            var result = _mapper.Map<OrderDto>(createdOrder);

			return result;

		}

        public async Task<decimal> GetDailyEarningsAsync()
        {
            return await _orderRepository.GetDailyEarningsAsync();
        }

        public async Task<IEnumerable<LastTenOrdersDto>> GetLastTenOrdersAsync()
        {
            // Repository'den siparişleri al
            var orders = await _orderRepository.GetLastTenOrdersAsync();

            // Siparişin güncellenmiş haliyle dönüş yapıyoruz
            var result = _mapper.Map<IEnumerable<LastTenOrdersDto>>(orders);

            return result;

        }

        public async Task<int> GetPendingOrdersCountAsync()
        {
            return await _orderRepository.GetPendingOrdersCountAsync();
        }

        public async Task<int> GetTodayOrdersCountAsync()
        {
            return await _orderRepository.GetTodayOrdersCountAsync();
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _orderRepository.GetTotalOrdersAsync();
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderUpdateDto dto)
        {
            var order = await _orderRepository.GetOrderWithDetailsAsync(dto.OrderId);

            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            var orderDetails = order.OrderDetails;

            // Sipariş onaylanıyorsa, stokları güncelliyoruz
            if (dto.IsApproved)
            {
                // Her bir sipariş detayı için stokları güncelle
                foreach (var orderDetail in orderDetails)
                {
                    var product = await _productRepository.GetByIdAsync(orderDetail.ProductId);

                    if (product == null)
                    {
                        throw new InvalidOperationException($"Product with ID {orderDetail.ProductId} not found.");
                    }

                    if (product.Stock < orderDetail.Quantity)
                    {
                        throw new InvalidOperationException($"Not enough stock for product {product.Name}. Available: {product.Stock}, Requested: {orderDetail.Quantity}");
                    }

                    var productStock = await _productStockRepository.GetByColumnAsync(ps => ps.ProductId == orderDetail.ProductId);

                    if (productStock == null)
                    {
                        throw new InvalidOperationException($"Stock information for product {product.Name} not found.");
                    }

                    // Stok miktarını düşür
                    product.Stock -= orderDetail.Quantity;
                    await _productRepository.UpdateAsync(product);


                    productStock.Quantity -= orderDetail.Quantity;
                    await _productStockRepository.UpdateAsync(productStock);

                    //  stok hareketi kaydetme işlemi
                    // await _stockMovementRepository.AddAsync(new StockMovement
                    // {
                    //     ProductId = product.Id,
                    //     Quantity = -orderDetail.Quantity,
                    //     MovementType = "Sale", // Stok düşüşü
                    //     Date = DateTime.UtcNow
                    // });
                }
            }

            // Siparişin durumunu güncelliyoruz
            order.IsApproved = dto.IsApproved;
            order.IsCancelled = dto.IsCancelled;
            order.Status = dto.Status;
            order.IsPaid = dto.IsPaid;
            order.CancellationReason = dto.CancellationReason;

            // Siparişin güncellenmesini bekleyin
            var updateResult = await _orderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            // Siparişin güncellenmiş haliyle dönüş yapıyoruz
            var result = _mapper.Map<OrderDto>(updateResult);

            return result; // Her durumda döndürülmeli.
        }
    }
}
