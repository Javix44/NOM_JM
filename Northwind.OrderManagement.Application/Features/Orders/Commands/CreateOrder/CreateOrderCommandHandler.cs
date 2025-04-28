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

                //Campos no obligatorios
                RequiredDate = request.RequiredDate,
                ShippedDate = request.ShippedDate,
                ShipVia = request.ShipVia,
                Freight = request.Freight,
                ShipName = request.ShipName,
                ShipCity = request.ShipCity,
                ShipRegion = request.ShipRegion,
                ShipPostalCode = request.ShipPostalCode,
                ShipCountry = request.ShipCountry
            };

            // Agregar detalle
            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in request.OrderItems)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return order.OrderId;
        }
    }
}
