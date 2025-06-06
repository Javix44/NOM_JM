namespace Northwind.OrderManagement.Application.Features.Orders.DTOs
{
    public class ValidatedAddressDto
    {
        public string FormattedAddress { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
