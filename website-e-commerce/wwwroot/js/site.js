function scrollProducts(direction) {
    const container = document.getElementById("products");
    const scrollAmount = 250;
    if (container) {
        container.scrollBy({ left: direction * scrollAmount, behavior: "smooth" });
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const decreaseBtn = document.querySelector(".btn-decrease");
    const increaseBtn = document.querySelector(".btn-increase");
    const quantityInput = document.querySelector("#quantity");

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
});
