using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    //[Authorize]
    public class StockHistoryController : Controller
    {
        private readonly IStockHistoryRepository _repo;

        public StockHistoryController(IStockHistoryRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public IActionResult Details(int id)
        {
            var item = _repo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var item = _repo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }
    }
}
