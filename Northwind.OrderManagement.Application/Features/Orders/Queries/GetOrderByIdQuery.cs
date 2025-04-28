using MediatR;
using Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries
{
    namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
    {
        public class GetOrderByIdQuery : IRequest<OrderDto>
        {
            public int OrderId { get; set; }

            public GetOrderByIdQuery(int orderId)
            {
                OrderId = orderId;
            }
        }
    }
}
