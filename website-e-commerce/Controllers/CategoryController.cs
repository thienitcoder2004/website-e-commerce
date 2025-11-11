using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: /Category
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var categories = _categoryRepository.GetAllCategories().ToList();

            int totalItems = categories.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            categories = categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(categories);
        }


        // GET: /Category/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Category/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: /Category/Edit/5
        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: /Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: /Category/Delete/5
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: /Category/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Details/5
        public IActionResult Details(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }
    }
}
