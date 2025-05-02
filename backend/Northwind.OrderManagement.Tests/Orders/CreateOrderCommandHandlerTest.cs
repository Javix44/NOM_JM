using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Application.Features.Orders.Commands.CreateOrder;

namespace Northwind.OrderManagement.Tests
{
    public class CreateOrderCommandHandlerTest
    {
        private NorthwindDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NorthwindDbContext(options);
        }

        [Fact]
        public async Task Handle_CreatesOrder_WhenValidRequest()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new CreateOrderCommandHandler(dbContext);

            var command = new CreateOrderCommand
            {
                CustomerId = "CUST1",
                OrderDate = DateTime.Now,
                ShipAddress = "123 Street",
                EmployeeId = 1
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result > 0); // OrderId should be greater than 0
            var createdOrder = await dbContext.Orders.FindAsync(result);
            Assert.NotNull(createdOrder);
            Assert.Equal("CUST1", createdOrder.CustomerId);
            Assert.Equal("123 Street", createdOrder.ShipAddress);
            Assert.Equal(1, createdOrder.EmployeeId);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenRequiredFieldsMissing()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new CreateOrderCommandHandler(dbContext);

            var command = new CreateOrderCommand
            {
                // Missing required fields like CustomerId, OrderDate, etc.
                ShipAddress = "123 Street",
                EmployeeId = 1
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SavesOrderToDatabase()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new CreateOrderCommandHandler(dbContext);

            var command = new CreateOrderCommand
            {
                CustomerId = "CUST2",
                OrderDate = DateTime.Now,
                ShipAddress = "456 Avenue",
                EmployeeId = 2
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var savedOrder = await dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == result);
            Assert.NotNull(savedOrder);
            Assert.Equal("CUST2", savedOrder.CustomerId);
            Assert.Equal("456 Avenue", savedOrder.ShipAddress);
            Assert.Equal(2, savedOrder.EmployeeId);
        }
    }
}