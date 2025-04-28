using MediatR;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Commands.UpdateOrderDetail
{
    public class UpdateOrderDetailCommand : IRequest<UpdateOrderDetailResponse>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        // Campos opcionales
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float? Discount { get; set; }
    }

    public class UpdateOrderDetailResponse
    {
        public string Message { get; set; }
    }
}
