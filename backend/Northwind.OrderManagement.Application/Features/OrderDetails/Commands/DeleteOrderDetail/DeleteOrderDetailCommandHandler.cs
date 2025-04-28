using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Commands.DeleteOrderDetail
{
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, DeleteOrderDetailResponse>
    {
        private readonly NorthwindDbContext _dbContext;

        public DeleteOrderDetailCommandHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteOrderDetailResponse> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await _dbContext.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == request.OrderId && od.ProductId == request.ProductId, cancellationToken);

            if (orderDetail == null)
            {
                return new DeleteOrderDetailResponse { Message = "Order Detail not found." };
            }

            _dbContext.OrderDetails.Remove(orderDetail);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderDetailResponse { Message = "Order Detail deleted successfully." };
        }
    }
}
