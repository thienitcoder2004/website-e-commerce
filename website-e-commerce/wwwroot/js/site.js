function scrollProducts(direction)
{
    const container = document.getElementById('products');
    const scrollAmount = 250;
    container.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
}