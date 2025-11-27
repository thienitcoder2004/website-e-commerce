const decreaseBtn = document.querySelector(".btn-decrease");
const increaseBtn = document.querySelector(".btn-increase");
const quantityInput = document.querySelector("#quantity");
const container = document.getElementById("products");

if (decreaseBtn && increaseBtn && quantityInput) {
    decreaseBtn.addEventListener("click", () => {
        const current = parseInt(quantityInput.value) || 1;
        if (current > 1) quantityInput.value = current - 1;
    });

    increaseBtn.addEventListener("click", () => {
        const current = parseInt(quantityInput.value) || 1;
        quantityInput.value = current + 1;
    });
}

function scrollProducts(direction) {
    const scrollAmount = 250;
    if (container) {
        container.scrollBy({ left: direction * scrollAmount, behavior: "smooth" });
    }
}

// --- SLIDER ---
var swiper = new Swiper(".flash-swiper", {
    slidesPerView: 6,
    spaceBetween: 10,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev"
    }
});


// --- COUNTDOWN ---
document.querySelectorAll(".fs-countdown").forEach(cd => {
    let end = new Date(cd.dataset.end).getTime();

    function tick() {
        let now = Date.now();
        let diff = end - now;

        if (diff <= 0) {
            cd.innerText = "Hết giờ";
            return;
        }
        let h = Math.floor(diff / 3600000);
        let m = Math.floor((diff % 3600000) / 60000);
        let s = Math.floor((diff % 60000) / 1000);

        cd.innerText =
            h.toString().padStart(2, '0') + ":" +
            m.toString().padStart(2, '0') + ":" +
            s.toString().padStart(2, '0');
    }
    tick();
    setInterval(tick, 1000);
});


// --- CHỌN KHUNG GIỜ ---
document.querySelectorAll(".slot-btn").forEach(slot => {
    slot.onclick = function () {

        let start = this.dataset.start;

        document.querySelectorAll(".slot-btn")
            .forEach(x => x.classList.remove("active"));
        this.classList.add("active");

        document.querySelectorAll(".flash-item").forEach(item => {
            if (item.dataset.start === start)
                item.style.display = "";
            else
                item.style.display = "none";
        });
    };
});