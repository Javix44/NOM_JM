using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace Northwind.OrderManagement.Application.Features.Reports.OrderDetailsReportDtos
{
    public class OrderDetailsReportDocument : IDocument
    {
        private readonly OrderDetailsReportDto _data;
        private readonly string _logoPath;

        public OrderDetailsReportDocument(OrderDetailsReportDto data)
        {
            _data = data;
            _logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo.png");
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);

                // Header con logo y texto
                page.Header().Height(60).Row(row =>
                {
                    if (File.Exists(_logoPath))
                    {
                        row.ConstantItem(60).Image(_logoPath);
                    }
                    else
                    {
                        row.ConstantItem(60).Placeholder(); // Fallback visual
                    }

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignRight().Text("Northwind Traders")
                            .FontSize(16).Bold().FontColor(Colors.Blue.Medium);
                        col.Item().AlignRight().Text("Order Details Report")
                            .FontSize(10).Italic();
                    });
                });

                // Contenido principal
                page.Content().Column(col =>
                {
                    col.Spacing(5);

                    col.Item().Text($"Order ID: {_data.OrderId}").Bold();
                    col.Item().Text($"Customer: {_data.CustomerName}");
                    col.Item().Text($"Employee: {_data.EmployeeName}");
                    col.Item().Text($"Order Date: {_data.OrderDate?.ToShortDateString() ?? "N/A"}");

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    // Tabla de detalles
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(80);
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Product").Bold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Unit Price").Bold();
                            header.Cell().Element(CellStyle).AlignCenter().Text("Qty").Bold();
                            header.Cell().Element(CellStyle).AlignCenter().Text("Discount").Bold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Total").Bold();
                        });

                        foreach (var item in _data.Items)
                        {
                            table.Cell().Element(CellStyle).Text(item.ProductName);
                            table.Cell().Element(CellStyle).AlignRight().Text($"${item.UnitPrice:F2}");
                            table.Cell().Element(CellStyle).AlignCenter().Text(item.Quantity.ToString());
                            table.Cell().Element(CellStyle).AlignCenter().Text($"{item.Discount:P0}");
                            table.Cell().Element(CellStyle).AlignRight().Text($"${item.Total:F2}");
                        }

                        static IContainer CellStyle(IContainer container)
                        {
                            return container.PaddingVertical(5).BorderBottom(0.5f).BorderColor(Colors.Grey.Darken4);
                        }
                    });

                    col.Item().PaddingTop(10).AlignRight().Text($"Total Amount: ${_data.TotalAmount:F2}").Bold();
                });

                page.Footer().AlignCenter().Text("Northwind Traders - Order Management System").FontSize(9).Italic();
            });
        }
    }
}
