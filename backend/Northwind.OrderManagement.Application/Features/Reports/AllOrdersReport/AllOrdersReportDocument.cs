using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Northwind.OrderManagement.Application.Features.Reports.AllOrdersReport.Queries
{
    public class AllOrdersReportDocument : IDocument
    {
        private readonly List<AllOrdersReportDto> _orders;

        public AllOrdersReportDocument(List<AllOrdersReportDto> orders)
        {
            _orders = orders;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Header().Text("All Orders Report").FontSize(20).SemiBold().AlignCenter();

                page.Content().Column(col =>
                {
                    foreach (var order in _orders)
                    {
                        col.Item().Text($"Order ID: {order.OrderId} | Customer: {order.CustomerCompanyName} | Date: {order.OrderDate:yyyy-MM-dd}")
                            .Bold().FontSize(12);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(100);
                                columns.ConstantColumn(80);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Product");
                                header.Cell().Text("Unit Price");
                                header.Cell().Text("Quantity");
                            });

                            foreach (var detail in order.OrderDetails)
                            {
                                table.Cell().Text(detail.ProductName);
                                table.Cell().Text($"{detail.UnitPrice:C}");
                                table.Cell().Text(detail.Quantity.ToString());
                            }
                        });

                        col.Item().Text($"Total: {order.TotalAmount:C}").AlignRight().Bold();
                        col.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten2);
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Generated on ");
                    x.Span(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                });
            });
        }
    }
}