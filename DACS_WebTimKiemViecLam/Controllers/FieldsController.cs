using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DACS_WebTimKiemViecLam.Controllers
{
    public class FieldsController : Controller
    {
        private readonly IFieldRepository _fieldRepo;

        public FieldsController(IFieldRepository fieldRepo)
        {
            _fieldRepo = fieldRepo;
        }

        public async Task<IActionResult> Index()
        {
            var fields = await _fieldRepo.GetAllAsync();
            return View(fields);
        }

        public IActionResult Create() => View();

        [HttpPost]
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var field = await _fieldRepo.GetByIdAsync(id);
            if (field != null)
            {
                _fieldRepo.Delete(field);
                await _fieldRepo.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
