using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly NorthwindDbContext _context;

        public GetOrderByIdQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate ?? DateTime.MinValue, // Manejo de DateTime? a DateTime
                ShipAddress = order.ShipAddress ?? string.Empty, // Manejo de posibles valores nulos en cadenas
                EmployeeId = order.EmployeeId ?? 0, // Manejo de int? a int
                orderDetails = order.OrderDetails.Select(od => new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity ?? 0, // Conversión explícita de short? a int con manejo de valores nulos
                    UnitPrice = od.UnitPrice
                }).ToList()
            };
        }
    }
}