using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, CreateOrderDetailResponse>
    {
        private readonly NorthwindDbContext _dbContext;

        public CreateOrderDetailCommandHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateOrderDetailResponse> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = new Domain.Entities.OrderDetail
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity,
                Discount = request.Discount
            };

            _dbContext.OrderDetails.Add(orderDetail);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderDetailResponse
            {
                Message = "Order detail created successfully."
            };
        }
    }
}
