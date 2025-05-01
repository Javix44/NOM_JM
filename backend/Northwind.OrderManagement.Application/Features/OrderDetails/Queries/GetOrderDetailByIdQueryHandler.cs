using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdQueryHandler : IRequestHandler<GetOrderDetailByIdQuery, OrderDetailDto>
    {
        private readonly NorthwindDbContext _dbContext;

        public GetOrderDetailByIdQueryHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDetailDto> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetail = await _dbContext.OrderDetails
                .FirstOrDefaultAsync(x => x.OrderId == request.OrderId && x.ProductId == request.ProductId, cancellationToken);

            if (orderDetail == null)
                return null;

            var orderDetailDto = new OrderDetailDto
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                UnitPrice = orderDetail.UnitPrice,
                Quantity = orderDetail.Quantity ?? 0, // Conversión explícita de short? a int con manejo de valores nulos
                Discount = orderDetail.Discount
            };

            return orderDetailDto;

        }
    }
}
