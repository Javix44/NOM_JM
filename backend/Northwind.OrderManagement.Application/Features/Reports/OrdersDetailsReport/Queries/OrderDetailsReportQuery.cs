using MediatR;

namespace Northwind.OrderManagement.Application.Features.Reports.OrderDetailsReportDtos
{
    public class GetOrderDetailsReportQuery : IRequest<OrderDetailsReportDto>
    {
        public int OrderId { get; set; }

        public GetOrderDetailsReportQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
