using MediatR;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Commands.DeleteOrderDetail
{
    public class DeleteOrderDetailCommand : IRequest<DeleteOrderDetailResponse>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }

    public class DeleteOrderDetailResponse
    {
        public string Message { get; set; }
    }
}
