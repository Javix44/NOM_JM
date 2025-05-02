using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Northwind.OrderManagement.Domain.Entities;
using Northwind.OrderManagement.Application.Features.Employees.Queries;

namespace Northwind.OrderManagement.Tests
{
    public class GetAllEmployeesQueryHandlerTest
    {
        private NorthwindDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NorthwindDbContext(options);
        }

        [Fact]
        public async Task Handle_ReturnsEmployees_WhenEmployeesExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            dbContext.Employees.Add(new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe"
            });
            dbContext.Employees.Add(new Employee
            {
                EmployeeId = 2,
                FirstName = "Jane",
                LastName = "Smith"
            });
            await dbContext.SaveChangesAsync();

            var handler = new GetAllEmployeesQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetAllEmployeesQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.EmployeeId == 1 && e.FirstName == "John" && e.LastName == "Doe");
            Assert.Contains(result, e => e.EmployeeId == 2 && e.FirstName == "Jane" && e.LastName == "Smith");
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoEmployeesExist()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var handler = new GetAllEmployeesQueryHandler(dbContext);

            // Act
            var result = await handler.Handle(new GetAllEmployeesQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}