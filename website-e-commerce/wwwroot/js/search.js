const searchBox = document.querySelector(".search-auto");
const suggestionBox = document.createElement("div");

suggestionBox.className = "suggestion-box shadow-lg";
Object.assign(suggestionBox.style, {
    position: "absolute",
    top: "100%",
    left: "0",
    width: "100%",
    zIndex: "9999",
    display: "none"
});

const parent = searchBox.parentElement;
parent.style.position = "relative";
parent.appendChild(suggestionBox);

searchBox.addEventListener("keyup", async () => {
    const term = searchBox.value.trim();

    if (term.length < 1) {
        suggestionBox.style.display = "none";
        return;
    }

    try {
        const res = await fetch(`/Product/SearchSuggestions?term=${encodeURIComponent(term)}`);
        const data = await res.json();

        suggestionBox.innerHTML = "";

        if (data.length > 0) {
            data.forEach(item => {
                const html = `
                        <a href="/Product/Details/${item.id}"
                           class="suggestion-item d-flex justify-content-between align-items-center">

                            <div class="suggestion-info">
                                <div class="product-name">${item.name}</div>

                                <div class="product-prices">
                                    <span class="new-price">${item.newPrice.toLocaleString()}đ</span>
                                    ${item.oldPrice > 0
                        ? `<span class="old-price">${item.oldPrice.toLocaleString()}đ</span>`
                        : ""}
                                </div>
                            </div>

                            <img src="${item.imageUrl}" alt="${item.name}" class="suggestion-img">
                        </a>
                    `;
                suggestionBox.insertAdjacentHTML("beforeend", html);
            });

            suggestionBox.style.display = "block";
        } else {
            suggestionBox.style.display = "none";
        }

    } catch (error) {
        console.error("Search error:", error);
        suggestionBox.style.display = "none";
    }
});

document.addEventListener("click", (e) => {
    if (!searchBox.contains(e.target) &&
        !suggestionBox.contains(e.target)) {
        suggestionBox.style.display = "none";
    }
});