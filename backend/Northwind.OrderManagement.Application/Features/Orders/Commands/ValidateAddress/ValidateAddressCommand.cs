using MediatR;
using Northwind.OrderManagement.Application.Features.Orders.DTOs;

namespace Northwind.OrderManagement.Application.Features.Orders.Commands.ValidateAddress
{
    public class ValidateAddressCommand : IRequest<ValidatedAddressDto>
    {
        public string RawAddress { get; set; } = string.Empty;
    }
}