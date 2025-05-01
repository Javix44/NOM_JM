using MediatR;

namespace Northwind.OrderManagement.Application.Features.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
    {
    }
}
