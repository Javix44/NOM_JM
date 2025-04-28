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

            // Actualiza solo si viene un valor
            if (request.CustomerId != null) order.CustomerId = request.CustomerId;
            if (request.EmployeeId.HasValue) order.EmployeeId = request.EmployeeId.Value;
            if (request.OrderDate.HasValue) order.OrderDate = request.OrderDate;
            if (request.RequiredDate.HasValue) order.RequiredDate = request.RequiredDate;
            if (request.ShippedDate.HasValue) order.ShippedDate = request.ShippedDate;
            if (request.ShipVia.HasValue) order.ShipVia = request.ShipVia.Value;
            if (request.Freight.HasValue) order.Freight = request.Freight.Value;
            if (request.ShipName != null) order.ShipName = request.ShipName;
            if (request.ShipAddress != null) order.ShipAddress = request.ShipAddress;
            if (request.ShipCity != null) order.ShipCity = request.ShipCity;
            if (request.ShipRegion != null) order.ShipRegion = request.ShipRegion;
            if (request.ShipPostalCode != null) order.ShipPostalCode = request.ShipPostalCode;
            if (request.ShipCountry != null) order.ShipCountry = request.ShipCountry;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResponse
            {
                Message = "Order updated successfully."
            };
        }
    }
}
