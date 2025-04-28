using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.Orders.Queries.Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>> // Fixed generic type
    {
        private readonly NorthwindDbContext _context;

        public GetAllOrdersQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken) // Fixed return type
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ToListAsync(cancellationToken); // Fixed method call

            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate ?? DateTime.MinValue, // Manejo de DateTime? a DateTime
                ShipAddress = order.ShipAddress ?? string.Empty, // Manejo de posibles valores nulos en cadenas
                EmployeeId = order.EmployeeId ?? 0, // Manejo de int? a int
                OrderItems = order.OrderDetails.Select(od => new OrderItemDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity ?? 0, // Conversión explícita de short? a int con manejo de valores nulos
                    UnitPrice = od.UnitPrice
                }).ToList()
            }).ToList();
        }
    }
}