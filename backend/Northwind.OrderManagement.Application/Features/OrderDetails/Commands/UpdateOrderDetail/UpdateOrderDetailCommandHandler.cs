using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.Orders.Commands.UpdateOrder;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Commands.UpdateOrderDetail
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, UpdateOrderDetailResponse>
    {
        private readonly NorthwindDbContext _dbContext;

        public UpdateOrderDetailCommandHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UpdateOrderDetailResponse> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await _dbContext.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == request.OrderId && od.ProductId == request.ProductId, cancellationToken);

            if (orderDetail == null)
            {
                return new UpdateOrderDetailResponse
                {
                    Message = $"OrderDetail with OrderId {request.OrderId} and ProductId {request.ProductId} not found."
                };
            }

            // Actualizar solo si vienen valores
            if (request.UnitPrice.HasValue)
                orderDetail.UnitPrice = request.UnitPrice.Value;

            if (request.Quantity.HasValue)
                orderDetail.Quantity = request.Quantity.Value;

            if (request.Discount.HasValue)
                orderDetail.Discount = request.Discount.Value;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderDetailResponse
            {
                Message = "OrderDetail updated successfully."
            };

        }
    }
}
