const container = document.querySelector(".container");
const newsPopup   = document.getElementById("newsPopup");
const newsToggle  = document.getElementById("newsToggle");
const newsWrapper = document.getElementById("newsWrapper");
const newsCount   = document.getElementById("newsCount");
const newsList    = document.getElementById("newsList");
const bellIcon    = document.getElementById("bellIcon");

if (container) {
    container.style.opacity = "0";
    setTimeout(() => {
        container.style.transition = "opacity 0.6s ease";
        container.style.opacity = "1";
    }, 50);
}

// ----- 2. Xác nhận khi xóa bài viết -----
const deleteForms = document.querySelectorAll("form[action*='Delete']");
deleteForms.forEach(form => {
    form.addEventListener("submit", (e) => {
        const titleEl = document.querySelector(".card-body h5.fw-bold");
        const title = titleEl ? titleEl.textContent.trim() : "bài viết này";

        const confirmed = confirm(`Bạn có chắc muốn xóa ${title}?`);
        if (!confirmed) e.preventDefault();
    });
});

// ----- 3. Highlight bài viết mới thêm -----
const msg = document.querySelector(".alert-info");
if (msg && msg.textContent.includes("Thêm bài viết thành công")) {
    msg.classList.add("shadow-sm", "border", "border-success");

    const firstRow = document.querySelector("table tbody tr:first-child");
    if (firstRow) firstRow.classList.add("table-success");
}

// ----- 4. Tooltip cho nút hành động (không jQuery) -----
const tooltipElements = document.querySelectorAll("[data-bs-toggle='tooltip']");
tooltipElements.forEach(el => new bootstrap.Tooltip(el));

// ----- 5. Hiệu ứng hover nhẹ nhàng ở card tin liên quan -----
const relatedCards = document.querySelectorAll(".related-news .card");
relatedCards.forEach(card => {
    card.addEventListener("mouseenter", () => {
        card.style.transform = "translateY(-4px)";
    });
    card.addEventListener("mouseleave", () => {
        card.style.transform = "translateY(0)";
    });
});

// ----- 6. Tự động ẩn thông báo sau 3 giây -----
setTimeout(() => {
    const alertInfo = document.querySelector(".alert-info");
    if (alertInfo) {
        alertInfo.style.transition = "opacity 0.8s";
        alertInfo.style.opacity = "0";
        setTimeout(() => alertInfo.remove(), 900);
    }
}, 3000);


newsToggle.addEventListener("click", () => {
    const visible = newsPopup.style.display === "block";
    newsPopup.style.display = visible ? "none" : "block";
    if (!visible) loadLatestNews();
});

document.addEventListener("click", (e) => {
    if (!newsWrapper.contains(e.target)) {
        newsPopup.style.display = "none";
    }
});

// 🔄 Hàm load tin tức
function loadLatestNews() {
    fetch("/News/GetLatest")
        .then(res => res.json())
        .then(data => {
            newsList.innerHTML = "";
            if (data.length === 0) {
                newsList.innerHTML = `<li class="list-group-item text-center text-muted">Không có tin mới</li>`;
            } else {
                data.forEach(n => {
                    newsList.innerHTML += `
                                <li class="list-group-item">
                                    <a href="/News/Detail/${n.id}" class="text-decoration-none text-dark d-block">
                                        <div class="fw-semibold">${n.title}</div>
                                        <small class="text-muted">${n.publishedDate}</small>
                                    </a>
                                </li>`;
                });
            }
            newsCount.innerText = data.length;
        })
        .catch(() => {
            newsList.innerHTML = `<li class="list-group-item text-center text-danger">Lỗi tải dữ liệu</li>`;
        });
}

// 🔔 Kết nối SignalR
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/newsHub")
    .build();

// Khi nhận tin mới từ server
connection.on("ReceiveNews", (title) => {
    // Rung chuông
    bellIcon.classList.add("ring");
    setTimeout(() => bellIcon.classList.remove("ring"), 2000);

    // Cập nhật danh sách tin mới nhất
    loadLatestNews();

    // Hiển thị thông báo nhỏ
    const toast = document.createElement("div");
    toast.className = "position-fixed top-0 end-0 p-3";
    toast.innerHTML = `
                <div class="toast align-items-center text-bg-primary border-0 show" style="z-index:2000">
                    <div class="d-flex">
                        <div class="toast-body">
                            📰 Tin mới: <strong>${title}</strong>
                        </div>
                    </div>
                </div>`;
    document.body.appendChild(toast);
    setTimeout(() => toast.remove(), 4000);
});

connection.start().catch(err => console.error(err.toString()));

// Toggle mở popup
newsToggle.addEventListener("click", () => {
    const visible = newsPopup.style.display === "block";
    newsPopup.style.display = visible ? "none" : "block";

    if (!visible) loadLatestNews();
});

// Click ra ngoài → đóng popup
document.addEventListener("click", (e) => {
    if (!newsWrapper.contains(e.target)) {
        newsPopup.style.display = "none";
    }
});

// 🕒 Auto reload mỗi 30s
setInterval(loadLatestNews, 30000);
loadLatestNews();
