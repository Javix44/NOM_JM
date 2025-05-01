using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Infrastructure.Persistence;

namespace Northwind.OrderManagement.Application.Features.Employees.Queries
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly NorthwindDbContext _context;

        public GetAllEmployeesQueryHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName?? string.Empty,
                    LastName = e.LastName?? string.Empty
                })
                .ToListAsync(cancellationToken);
        }
    }
}