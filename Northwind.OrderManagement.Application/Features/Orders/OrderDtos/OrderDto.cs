using System;
using System.Collections.Generic;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.MinValue; // Default to MinValue
        public string ShipAddress { get; set; }
        public int EmployeeId { get; set; }

        public DateTime? RequiredDate { get; set; } // Optional
        public DateTime? ShippedDate { get; set; } // Optional
        public int? ShipVia { get; set; } // Optional
        public decimal? Freight { get; set; } // Optional
        public string? ShipName { get; set; } // Optional
        public string? ShipCity { get; set; }// Optional
        public string? ShipRegion { get; set; }// Optional
        public string? ShipPostalCode { get; set; }// Optional
        public string? ShipCountry { get; set; }// Optional
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
