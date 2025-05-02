using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById;

namespace Northwind.OrderManagement.Tests
{
    public class GetOrderByIdQueryHandlerTest
    {
        private NorthwindDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NorthwindDbContext(options);
        }

        [Fact]
        public async Task Handle_ReturnsOrder_WhenOrderExists()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            dbContext.Orders.Add(new Order
            {
                OrderId = 1,
                CustomerId = "CUST1",
                OrderDate = DateTime.Now,
                ShipAddress = "123 Street",
                EmployeeId = 1
            });
            await dbContext.SaveChangesAsync();

            var handler = new GetOrderByIdQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetOrderByIdQuery(1), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
            Assert.Equal("CUST1", result.CustomerId);
            Assert.Equal("123 Street", result.ShipAddress);
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new GetOrderByIdQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetOrderByIdQuery(999), CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}