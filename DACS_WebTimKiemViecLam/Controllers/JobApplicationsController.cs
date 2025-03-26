using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DACS_WebTimKiemViecLam.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly IJobApplicationRepository _jobAppRepo;

        public JobApplicationsController(IJobApplicationRepository jobAppRepo)
        {
            _jobAppRepo = jobAppRepo;
        }

        public async Task<IActionResult> Index()
        {
            var apps = await _jobAppRepo.GetAllAsync();
            return View(apps);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(JobApplication app)
        {
            if (!ModelState.IsValid) return View(app);
            await _jobAppRepo.AddAsync(app);
            await _jobAppRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var app = await _jobAppRepo.GetByIdAsync(id);
            if (app == null) return NotFound();
            return View(app);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobApplication app)
        {
            if (!ModelState.IsValid) return View(app);
            _jobAppRepo.Update(app);
            await _jobAppRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var app = await _jobAppRepo.GetByIdAsync(id);
            if (app == null) return NotFound();
            return View(app);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var app = await _jobAppRepo.GetByIdAsync(id);
            if (app != null)
            {
                _jobAppRepo.Delete(app);
                await _jobAppRepo.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
