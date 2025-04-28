using MediatR;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommand : IRequest<CreateOrderDetailResponse>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }

    public class CreateOrderDetailResponse
    {
        public string Message { get; set; }
    }
}
