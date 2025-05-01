using MediatR;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly NorthwindDbContext _context;

        public CreateOrderCommandHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Crear Order
            var order = new Order
            {
                //Campos obligatorios
                CustomerId = request.CustomerId,
                OrderDate = request.OrderDate,
                ShipAddress = request.ShipAddress,
                EmployeeId = request.EmployeeId,

            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return order.OrderId;
        }
    }
}
