using MediatR;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>
    {
    }
}
