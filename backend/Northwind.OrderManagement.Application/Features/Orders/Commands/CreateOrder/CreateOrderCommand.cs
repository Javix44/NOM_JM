using MediatR;

namespace Northwind.OrderManagement.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>  
    {
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        // Para líneas del pedido (detalle)
        public List<orderDetails> orderDetails { get; set; }
    }

    public class orderDetails
    {
        public int ProductId { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
