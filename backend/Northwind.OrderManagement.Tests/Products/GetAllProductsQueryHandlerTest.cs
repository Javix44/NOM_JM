using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Application.Features.Products.Queries;

namespace Northwind.OrderManagement.Tests
{
    public class GetAllProductsQueryHandlerTest
    {
        private NorthwindDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NorthwindDbContext(options);
        }

        [Fact]
        public async Task Handle_ReturnsProducts_WhenProductsExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            dbContext.Products.Add(new Product
            {
                ProductId = 1,
                ProductName = "Product 1",
                UnitPrice = 10.5m,
                UnitsInStock = 20,
                Discontinued = false,
                QuantityPerUnit = "10 boxes"
            });
            dbContext.Products.Add(new Product
            {
                ProductId = 2,
                ProductName = "Product 2",
                UnitPrice = 15.0m,
                UnitsInStock = 10,
                Discontinued = true,
                QuantityPerUnit = "5 botles"
            });
            await dbContext.SaveChangesAsync();

            var handler = new GetAllProductsQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.ProductId == 1 && p.ProductName == "Product 1" && p.UnitPrice == 10.5m && p.UnitsInStock == 20 && !p.Discontinued);
            Assert.Contains(result, p => p.ProductId == 2 && p.ProductName == "Product 2" && p.UnitPrice == 15.0m && p.UnitsInStock == 10 && p.Discontinued);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new GetAllProductsQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}