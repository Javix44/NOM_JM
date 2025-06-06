﻿namespace Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class OrderDto
    {
        public int? OrderId { get; set; } //Autogenerated
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
        public List<OrderDetail> orderDetails { get; set; }

        /// EXTRAS
        public string? CustomerCompanyName { get; set; } // Añadido para la consulta de CompanyName
        public string? EmployeeFullName { get; set; } // Añadido para la consulta de EmployeeFullName

    }

    public class OrderDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
