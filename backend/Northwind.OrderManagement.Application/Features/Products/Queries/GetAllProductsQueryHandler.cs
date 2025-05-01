using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly NorthwindDbContext _context;

        public GetAllProductsQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            return products.Select(prod => new ProductDto
            {
                ProductId = prod.ProductId,
                ProductName = prod.ProductName ?? string.Empty, // Protección contra NULL
                UnitPrice = prod.UnitPrice,
                UnitsInStock = prod.UnitsInStock,
                Discontinued = prod.Discontinued
            }).ToList();
        }
    }
}
