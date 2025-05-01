using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly NorthwindDbContext _context;

        public GetAllOrdersQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails) // Asegúrate de que sea OrderItems si así se llama en tu DbContext
                .Include(o => o.Customer) // Incluyendo Customer
                .Include(o => o.Employee) // Incluyendo Employee
                .ToListAsync(cancellationToken);

            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate ?? DateTime.MinValue,
                ShipAddress = order.ShipAddress ?? string.Empty,
                EmployeeId = order.EmployeeId ?? 0,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry,

                // Campos extra (Customer y Employee)
                CustomerCompanyName = order.Customer?.CompanyName ?? "N/A",
                EmployeeFullName = $"{order.Employee?.FirstName} {order.Employee?.LastName}".Trim(),

                orderDetails = order.OrderDetails.Select(oi => new OrderDetail
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity ?? 0,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            }).ToList();
        }

    }

}