using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;
using Northwind.OrderManagement.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Queries.GetAllOrderDetails
{
    public class GetAllOrderDetailsQueryHandler : IRequestHandler<GetAllOrderDetailsQuery, List<OrderDetailDto>>
    {
        private readonly NorthwindDbContext _dbContext;

        public GetAllOrderDetailsQueryHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDetailDto>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _dbContext.OrderDetails.ToListAsync(cancellationToken);

            var orderDetailsDto = orderDetails.Select(od => new OrderDetailDto
            {
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                UnitPrice = od.UnitPrice,
                Quantity = od.Quantity ?? 0, // Conversión explícita de short? a int con manejo de valores nulos
                Discount = od.Discount
            }).ToList();

            return orderDetailsDto;

        }
    }
}
