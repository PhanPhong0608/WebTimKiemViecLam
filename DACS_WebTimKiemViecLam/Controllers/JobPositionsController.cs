using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Controllers
{
    public class JobPositionsController : Controller
    {
        private readonly IJobPositionRepository _jobPositionRepo;
        private readonly JobDbContext _context;

        public JobPositionsController(IJobPositionRepository jobPositionRepo, JobDbContext context)
        {
            _jobPositionRepo = jobPositionRepo;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _context.JobPositions
                .Include(j => j.Company)
                .Include(j => j.Field)
                .ToListAsync();
            return View(jobs);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(JobPosition job)
        {
            if (!ModelState.IsValid) return View(job);
            await _jobPositionRepo.AddAsync(job);
            await _jobPositionRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var job = await _jobPositionRepo.GetByIdAsync(id);
            if (job == null) return NotFound();
            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobPosition job)
        {
            if (!ModelState.IsValid) return View(job);
            _jobPositionRepo.Update(job);
            await _jobPositionRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var job = await _jobPositionRepo.GetByIdAsync(id);
            if (job == null) return NotFound();
            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _jobPositionRepo.GetByIdAsync(id);
            if (job != null)
            {
                _jobPositionRepo.Delete(job);
                await _jobPositionRepo.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
