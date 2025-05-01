using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Reports.AllOrdersReport.Queries
{
    public class GetAllOrdersReportQueryHandler : IRequestHandler<GetAllOrdersReportQuery, List<AllOrdersReportDto>>
    {
        private readonly NorthwindDbContext _context;

        public GetAllOrdersReportQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<List<AllOrdersReportDto>> Handle(GetAllOrdersReportQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Select(o => new AllOrdersReportDto
                {
                    OrderId = o.OrderId,
                    CustomerCompanyName = o.Customer.CompanyName,
                    OrderDate = o.OrderDate,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        ProductName = od.Product.ProductName,
                        UnitPrice = od.UnitPrice,
                        Quantity = od.Quantity ?? 0
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}
