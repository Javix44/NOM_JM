using Northwind.OrderManagement.Application.Features.Orders.DTOs;

namespace Northwind.OrderManagement.Domain.Interfaces
{
    public interface IGoogleMapsService
    {
        Task<ValidatedAddressDto> ValidateAddressAsync(string rawAddress);
    }
}