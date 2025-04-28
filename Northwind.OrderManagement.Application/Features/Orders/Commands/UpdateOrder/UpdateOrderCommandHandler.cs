using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Northwind.OrderManagement.Application.Features.Orders.Commands.UpdateOrder;
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
            // Buscar la orden por Id
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null)
            {
                return new UpdateOrderResponse
                {
                    Message = $"Order with Id {request.OrderId} not found."
                };
            }

            // Actualizar los valores de la orden
            order.CustomerId = request.CustomerId;
            order.EmployeeId = request.EmployeeId;
            order.OrderDate = request.OrderDate;
            order.RequiredDate = request.RequiredDate;
            order.ShippedDate = request.ShippedDate;
            order.ShipVia = request.ShipVia;
            order.Freight = request.Freight;
            order.ShipName = request.ShipName;
            order.ShipAddress = request.ShipAddress;
            order.ShipCity = request.ShipCity;
            order.ShipRegion = request.ShipRegion;
            order.ShipPostalCode = request.ShipPostalCode;
            order.ShipCountry = request.ShipCountry;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResponse
            {
                Message = "Order updated successfully."
            };
        }
    }
}
