using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Exceptions;
using Northwind.OrderManagement.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.OrderManagement.Application.Features.Orders.Commands.DeteleOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderResponse>
    {
        private readonly NorthwindDbContext _dbContext;

        public DeleteOrderCommandHandler(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            // Buscar la orden incluyendo los detalles de la orden
            var order = await _dbContext.Orders
                .Include(o => o.OrderDetails)  // Incluir detalles de la orden
                .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null)
            {
                // Lanzar excepción si no se encuentra la orden
                throw new NotFoundException($"Order with Id {request.OrderId} not found.");
            }

            // Eliminar primero los detalles de la orden
            _dbContext.OrderDetails.RemoveRange(order.OrderDetails);

            // Eliminar la orden
            _dbContext.Orders.Remove(order);

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Devolver la respuesta con un mensaje de confirmación
            return new DeleteOrderResponse
            {
                Message = "Order deleted successfully."
            };
        }
    }

    public class DeleteOrderResponse
    {
        public string Message { get; set; }
    }
}
