namespace Northwind.OrderManagement.Application.Features.Reports
{
    public class AllOrdersReportDto
    {
        public int OrderId { get; set; }
        public string? CustomerCompanyName { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
        public decimal TotalAmount => OrderDetails.Sum(d => d.UnitPrice * d.Quantity);
    }

    public class OrderDetailDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
    }
}