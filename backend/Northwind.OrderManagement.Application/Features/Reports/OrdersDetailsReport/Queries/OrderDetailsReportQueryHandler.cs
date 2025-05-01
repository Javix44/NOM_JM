using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Reports.OrderDetailsReportDtos
{
    public class GetOrderDetailsReportQueryHandler : IRequestHandler<GetOrderDetailsReportQuery, OrderDetailsReportDto?>
    {
        private readonly NorthwindDbContext _context;

        public GetOrderDetailsReportQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDetailsReportDto?> Handle(GetOrderDetailsReportQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null)
                return null;

            return new OrderDetailsReportDto
            {
                OrderId = order.OrderId,
                CustomerName = order.Customer?.CompanyName ?? "N/A",
                EmployeeName = order.Employee == null ? "N/A" : $"{order.Employee.FirstName} {order.Employee.LastName}",
                OrderDate = order.OrderDate,
                Items = order.OrderDetails.Select(od => new orderDetails
                {
                    ProductName = od.Product?.ProductName ?? "N/A",
                    UnitPrice = od.UnitPrice,
                    Quantity = (short)(od.Quantity ?? 0),
                }).ToList()
            };
        }
    }
}
