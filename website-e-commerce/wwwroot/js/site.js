function scrollProducts(direction)
{
    const container = document.getElementById('products');
    const scrollAmount = 250;
    container.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
}

$(document).ready(function () {
    $(".increase-btn").click(function () {
        const row = $(this).closest("tr");
        const productId = row.data("id");

        $.post("/ShoppingCart/IncreaseQuantityAjax", { productId }, function (res) {
            if (res.success) {
                row.find(".quantity").text(res.quantity);
                row.find(".item-total").text(res.itemTotal.toLocaleString('vi-VN') + " đ");
                $("#cart-total").text(res.total.toLocaleString('vi-VN') + " đ");
            }
        });
    });

    $(".decrease-btn").click(function () {
        const row = $(this).closest("tr");
        const productId = row.data("id");

        $.post("/ShoppingCart/DecreaseQuantityAjax", { productId }, function (res) {
            if (res.success) {
                if (res.quantity > 0) {
                    row.find(".quantity").text(res.quantity);
                    row.find(".item-total").text(res.itemTotal.toLocaleString('vi-VN') + " đ");
                } else {
                    row.fadeOut(200, function () { $(this).remove(); });
                }
                $("#cart-total").text(res.total.toLocaleString('vi-VN') + " đ");
            }
        });
    });
});