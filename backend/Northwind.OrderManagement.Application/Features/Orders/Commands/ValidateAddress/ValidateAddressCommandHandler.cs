using MediatR;
using Northwind.OrderManagement.Application.Features.Orders.DTOs;
using Northwind.OrderManagement.Application.Features.Orders.Commands.ValidateAddress;
using Northwind.OrderManagement.Domain.Interfaces;

namespace Northwind.OrderManagement.Application.Features.Orders.Queries.Maps
{
    public class ValidateAddressCommandHandler : IRequestHandler<ValidateAddressCommand, ValidatedAddressDto>
    {
        private readonly IGoogleMapsService _mapsService;

        public ValidateAddressCommandHandler(IGoogleMapsService mapsService)
        {
            _mapsService = mapsService;
        }

        public async Task<ValidatedAddressDto> Handle(ValidateAddressCommand request, CancellationToken cancellationToken)
        {
            return await _mapsService.ValidateAddressAsync(request.RawAddress);
        }
    }
}