using MediatR;

namespace Northwind.OrderManagement.Application.Features.Orders.Commands.DeteleOrder
{
    public class DeleteOrderCommand : IRequest<DeleteOrderResponse>
    {
        public int OrderId { get; set; }
    }
}

