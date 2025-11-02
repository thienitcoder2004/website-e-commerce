document.addEventListener("DOMContentLoaded", function () {
    const categoryId = document.getElementById("categoryId")?.value || 0;

    const updateProducts = () => {
        const brands = Array.from(document.querySelectorAll(".filter-brand:checked")).map(e => e.value);
        const cpus = Array.from(document.querySelectorAll(".filter-cpu:checked")).map(e => e.value);
        const gpus = Array.from(document.querySelectorAll(".filter-gpu:checked")).map(e => e.value);
        const sizes = Array.from(document.querySelectorAll(".filter-size:checked")).map(e => e.value);

        fetch("/Product/FilterProducts", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ categoryId, brands, cpus, gpus, sizes })
        })
            .then(res => res.text())
            .then(html => {
                document.getElementById("productList").innerHTML = html;
            })
            .catch(err => console.error(err));
    };

    document.querySelectorAll(".filter-brand, .filter-cpu, .filter-gpu, .filter-size").forEach(cb => {
        cb.addEventListener("change", updateProducts);
    });
});
