using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Controllers
{
    public class FieldsController : Controller
    {
        private readonly IFieldRepository _fieldRepo;
        private readonly JobDbContext _context;

        public FieldsController(IFieldRepository fieldRepo, JobDbContext context)
        {
            _fieldRepo = fieldRepo;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fields = await _fieldRepo.GetAllAsync();
            return View(fields);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Field field)
        {
            if (!ModelState.IsValid) return View(field);
            await _fieldRepo.AddAsync(field);
            await _fieldRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var field = await _fieldRepo.GetByIdAsync(id);
            if (field == null) return NotFound();
            return View(field);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Field field)
        {
            if (!ModelState.IsValid) return View(field);
            _fieldRepo.Update(field);
            await _fieldRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var field = await _fieldRepo.GetByIdAsync(id);
            if (field == null) return NotFound();
            return View(field);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var field = await _fieldRepo.GetByIdAsync(id);
            if (field == null) return NotFound();

            // ✅ Kiểm tra ràng buộc từ bảng Companies
            var usedInCompanies = await _context.Companies.AnyAsync(c => c.FieldID == id);
            // ✅ Có thể mở rộng thêm JobPositions nếu bạn muốn
            var usedInJobs = await _context.JobPositions.AnyAsync(j => j.FieldID == id);

            if (usedInCompanies || usedInJobs)
            {
                ModelState.AddModelError("", "Không thể xóa lĩnh vực vì đang được sử dụng trong hệ thống.");
                return View(field);
            }

            _fieldRepo.Delete(field);
            await _fieldRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
