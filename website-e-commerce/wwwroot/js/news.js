// ========== NEWS.JS ==========
// Script nhẹ nhàng dành cho phần News

$(document).ready(function () {
    // ----- 1. Hiệu ứng fade-in cho trang tin -----
    $(".container").css("opacity", "0").animate({ opacity: 1 }, 600);

    // ----- 2. Xác nhận khi xóa bài viết -----
    $("form[action*='Delete']").on("submit", function (e) {
        const title = $(".card-body h5.fw-bold").text().trim() || "bài viết này";
        const confirmed = confirm(`Bạn có chắc muốn xóa ${title}?`);
        if (!confirmed) {
            e.preventDefault();
        }
    });

    // ----- 3. Highlight bài viết mới thêm -----
    const message = $(".alert-info:contains('Thêm bài viết thành công')");
    if (message.length) {
        message.addClass("shadow-sm border border-success");
        $("table tbody tr:first").addClass("table-success");
    }

    // ----- 4. Tooltip cho nút hành động -----
    $("[data-bs-toggle='tooltip']").tooltip();

    // ----- 5. Hiệu ứng hover nhẹ nhàng ở card tin liên quan -----
    $(".related-news .card").hover(
        function () {
            $(this).css("transform", "translateY(-4px)");
        },
        function () {
            $(this).css("transform", "translateY(0)");
        }
    );

    // ----- 6. Tự động ẩn thông báo sau 3 giây -----
    setTimeout(() => {
        $(".alert-info").fadeOut("slow");
    }, 3000);
});
