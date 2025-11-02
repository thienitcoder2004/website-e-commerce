using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

namespace website_e_commerce.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 LIST / INDEX
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return View(contacts);
        }

        // 🔹 ADD (GET)
        public IActionResult Add()
        {
            return View();
        }

        // 🔹 ADD (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.CreatedAt = DateTime.Now;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm liên hệ thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // 🔹 EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // 🔹 EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cập nhật liên hệ thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // 🔹 DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Xóa liên hệ thành công!";
            return RedirectToAction(nameof(Index));
        }

    }
}
