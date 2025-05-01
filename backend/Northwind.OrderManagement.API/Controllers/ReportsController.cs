using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.OrderManagement.Application.Features.Reports.AllOrdersReport.Queries;
using Northwind.OrderManagement.Application.Features.Reports.OrderDetailsReportDtos;
using QuestPDF.Fluent;

namespace Northwind.OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/reports/all-orders
        [HttpGet("all-orders")]
        public async Task<IActionResult> GetAllOrdersReport()
        {
            var orders = await _mediator.Send(new GetAllOrdersReportQuery());
            var document = new AllOrdersReportDocument(orders);

            var stream = new MemoryStream();
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", "AllOrdersReport.pdf");
        }

        // GET: api/reports/order-details-report/{orderId}
        [HttpGet("order-details-report/{orderId}")]
        public async Task<IActionResult> GetOrderDetailsReport(int orderId)
        {
            var dto = await _mediator.Send(new GetOrderDetailsReportQuery(orderId));

            var document = new OrderDetailsReportDocument(dto);
            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", $"Order_{orderId}_DetailsReport.pdf");
        }

    }
}
