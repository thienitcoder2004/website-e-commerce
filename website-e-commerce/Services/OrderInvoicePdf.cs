using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using website_e_commerce.Models;

public class OrderInvoicePdf : IDocument
{
    private readonly Order _order;

    public OrderInvoicePdf(Order order)
    {
        _order = order;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Content().Column(col =>
            {
                col.Item().Text($"HÓA ĐƠN ĐƠN HÀNG #{_order.Id}")
                    .FontSize(20).Bold();

                col.Item().PaddingVertical(10);

                col.Item().Text($"Khách hàng: {_order.ApplicationUser?.HoTen}");
                col.Item().Text($"Email: {_order.ApplicationUser?.Email}");
                col.Item().Text($"Ngày đặt: {_order.OrderDate:dd/MM/yyyy HH:mm}");
                col.Item().Text($"Địa chỉ: {_order.ShippingAddress}");

                col.Item().PaddingVertical(15);

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.ConstantColumn(60);
                        columns.ConstantColumn(80);
                        columns.ConstantColumn(100);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Sản phẩm").Bold();
                        header.Cell().Text("SL").Bold();
                        header.Cell().Text("Giá").Bold();
                        header.Cell().Text("Tổng").Bold();
                    });

                    foreach (var item in _order.OrderDetails)
                    {
                        table.Cell().Text(item.Product.Name);
                        table.Cell().Text(item.Quantity.ToString());
                        table.Cell().Text($"{item.Price:N0} đ");
                        table.Cell().Text($"{item.Price * item.Quantity:N0} đ");
                    }
                });

                col.Item().PaddingTop(15);
                col.Item().AlignRight().Text($"TỔNG: {_order.TotalPrice:N0} đ")
                    .FontSize(16).Bold();
            });
        });
    }
}
