using MediatR;

namespace Northwind.OrderManagement.Application.Features.Employees.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
    {
    }
}
