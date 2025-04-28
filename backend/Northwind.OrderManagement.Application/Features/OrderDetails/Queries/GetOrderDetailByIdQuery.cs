using MediatR;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdQuery : IRequest<OrderDetailDto>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
