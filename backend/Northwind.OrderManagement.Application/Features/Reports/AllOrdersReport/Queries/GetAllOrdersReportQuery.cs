using MediatR;

namespace Northwind.OrderManagement.Application.Features.Reports.AllOrdersReport.Queries
{
    public class GetAllOrdersReportQuery : IRequest<List<AllOrdersReportDto>>
    {
    }
}