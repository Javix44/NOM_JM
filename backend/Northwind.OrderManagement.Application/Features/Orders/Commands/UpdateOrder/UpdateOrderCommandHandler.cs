using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.OrderManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResponse>
    {
        private readonly NorthwindDbContext _dbContext;

        public UpdateOrderCommandHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UpdateOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null)
            {
                return new UpdateOrderResponse
                {
                    Message = $"Order with Id {request.OrderId} not found."
                };
            }
            //Campos obligatorios
            // Actualiza solo si viene un valor
            if (request.CustomerId != null) order.CustomerId = request.CustomerId;
            if (request.OrderDate.HasValue) order.OrderDate = request.OrderDate;
            if (request.EmployeeId.HasValue) order.EmployeeId = request.EmployeeId.Value;
            if (request.ShipAddress != null) order.ShipAddress = request.ShipAddress;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResponse
            {
                Message = "Order updated successfully."
            };
        }
    }
}
