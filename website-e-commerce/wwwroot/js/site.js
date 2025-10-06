// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function scrollProducts(direction)
{
    const container = document.getElementById('products');
    const scrollAmount = 250;
    container.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
}