using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Queries.AllOrderDetailById
{
    public class GetAllOrderDetailByIdQueryHandler : IRequestHandler<GetAllOrderDetailByIdQuery, List<OrderDetailDto>>
    {
        private readonly NorthwindDbContext _dbContext;

        public GetAllOrderDetailByIdQueryHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDetailDto>> Handle(GetAllOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.OrderDetails
                 .Where(od => od.OrderId == request.OrderId)
                 .Select(od => new OrderDetailDto
                 {
                     OrderId = od.OrderId,
                     ProductId = od.ProductId,
                     UnitPrice = od.UnitPrice,
                     Quantity = od.Quantity ?? 0,
                     Discount = od.Discount
                 })
                 .ToListAsync(cancellationToken);
        }
    }
}
