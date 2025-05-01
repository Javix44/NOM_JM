using MediatR;

namespace Northwind.OrderManagement.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
