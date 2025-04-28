using MediatR;
using Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries
{
    namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
    {
        public class GetAllOrdersQuery : IRequest<List<OrderDto>>
        {
        }
    }
}
