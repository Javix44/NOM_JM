using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Application.Features.Orders.Commands.UpdateOrder;

namespace Northwind.OrderManagement.Tests
{
    public class UpdateOrderCommandHandlerTest
    {
        private NorthwindDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NorthwindDbContext(options);
        }

        [Fact]
        public async Task Handle_UpdatesOrder_WhenOrderExists()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            dbContext.Orders.Add(new Order
            {
                OrderId = 1,
                CustomerId = "CUST1",
                OrderDate = DateTime.Now.AddDays(-1),
                ShipAddress = "Old Address",
                EmployeeId = 1
            });
            await dbContext.SaveChangesAsync();

            var handler = new UpdateOrderCommandHandler(dbContext);
            var command = new UpdateOrderCommand
            {
                OrderId = 1,
                CustomerId = "CUST2",
                ShipAddress = "New Address"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Order updated successfully.", result.Message);
            var updatedOrder = await dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == 1);
            Assert.NotNull(updatedOrder);
            Assert.Equal("CUST2", updatedOrder.CustomerId);
            Assert.Equal("New Address", updatedOrder.ShipAddress);
            Assert.Equal(1, updatedOrder.EmployeeId); // Unchanged field
        }

        [Fact]
        public async Task Handle_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new UpdateOrderCommandHandler(dbContext);
            var command = new UpdateOrderCommand
            {
                OrderId = 999,
                CustomerId = "CUST1"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Order with Id 999 not found.", result.Message);
        }

        [Fact]
        public async Task Handle_UpdatesOnlyProvidedFields()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            dbContext.Orders.Add(new Order
            {
                OrderId = 2,
                CustomerId = "CUST1",
                OrderDate = DateTime.Now.AddDays(-1),
                ShipAddress = "Old Address",
                EmployeeId = 1
            });
            await dbContext.SaveChangesAsync();

            var handler = new UpdateOrderCommandHandler(dbContext);
            var command = new UpdateOrderCommand
            {
                OrderId = 2,
                ShipAddress = "Updated Address"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Order updated successfully.", result.Message);
            var updatedOrder = await dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == 2);
            Assert.NotNull(updatedOrder);
            Assert.Equal("CUST1", updatedOrder.CustomerId); // Unchanged field
            Assert.Equal("Updated Address", updatedOrder.ShipAddress);
            Assert.Equal(1, updatedOrder.EmployeeId); // Unchanged field
        }
    }
}