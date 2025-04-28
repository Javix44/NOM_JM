using MediatR;
using System.Collections.Generic;
using Northwind.OrderManagement.Application.Features.OrderDetails.DTOs;

namespace Northwind.OrderManagement.Application.Features.OrderDetails.Queries.GetAllOrderDetails
{
    public class GetAllOrderDetailsQuery : IRequest<List<OrderDetailDto>>
    {
    }
}
