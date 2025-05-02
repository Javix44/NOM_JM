using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Application.Features.Customers.Queries;

namespace Northwind.OrderManagement.Tests
{
    public class GetAllCustomersQueryHandlerTest
    {
        private NorthwindDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NorthwindDbContext(options);
        }

        [Fact]
        public async Task Handle_ReturnsCustomers_WhenCustomersExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            dbContext.Customers.Add(new Customer
            {
                CustomerId = "CUST1",
                CompanyName = "Customer 1",
                ContactName = "John Doe"
            });
            dbContext.Customers.Add(new Customer
            {
                CustomerId = "CUST2",
                CompanyName = "Customer 2",
                ContactName = "Jane Smith"
            });
            await dbContext.SaveChangesAsync();

            var handler = new GetAllCustomersQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetAllCustomersQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CustomerId == "CUST1" && c.CompanyName == "Customer 1" && c.ContactName == "John Doe");
            Assert.Contains(result, c => c.CustomerId == "CUST2" && c.CompanyName == "Customer 2" && c.ContactName == "Jane Smith");
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoCustomersExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new GetAllCustomersQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetAllCustomersQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}