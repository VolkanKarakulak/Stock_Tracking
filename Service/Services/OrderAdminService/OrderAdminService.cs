using AutoMapper;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.OrderRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.UnitOfWorks;
using Service.DTOs.OrderAdminDtos;
using Service.DTOs.OrderDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.OrderAdminService
{
	public class OrderAdminService : GenericService<Order, OrderDto>, IOrderAdminService
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;
		private readonly IProductRepository _productRepository;
		private readonly IProductStockRepository _productStockRepository;
		private readonly IUnitOfWork _unitOfWork;
		public OrderAdminService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository, IProductStockRepository productStockRepository) : base(repository, unitOfWork, mapper)
		{
			_mapper = mapper;
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
			_productRepository = productRepository;
			_productStockRepository = productStockRepository;
			_productStockRepository = productStockRepository;
		}

		public async Task<OrderDto> UpdateOrderAsync(OrderUpdateByAdminDto dto)
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

