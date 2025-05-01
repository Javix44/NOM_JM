using MediatR;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Queries.AllOrderDetailById
{
    public class GetAllOrderDetailByIdQuery : IRequest<List<OrderDetailDto>>
    {
        public int OrderId { get; set; }

        public GetAllOrderDetailByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
