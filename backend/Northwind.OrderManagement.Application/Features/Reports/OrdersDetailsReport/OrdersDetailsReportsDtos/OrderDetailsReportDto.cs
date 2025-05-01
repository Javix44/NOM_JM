namespace Northwind.OrderManagement.Application.Features.Reports.OrderDetailsReportDtos
{
    public class OrderDetailsReportDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public List<orderDetails> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.Total);
    }

    public class orderDetails
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total => UnitPrice * Quantity * (1 - (decimal)Discount);
    }
}