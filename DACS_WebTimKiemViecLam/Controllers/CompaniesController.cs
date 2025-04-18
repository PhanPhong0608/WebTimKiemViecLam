using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IFieldRepository _fieldRepo;
        private readonly JobDbContext _context;

        public CompaniesController(ICompanyRepository companyRepo, IFieldRepository fieldRepo, JobDbContext context)
        {
            _companyRepo = companyRepo;
            _fieldRepo = fieldRepo;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _companyRepo.GetAllAsync();
            return View(companies);
        }

        public async Task<IActionResult> Create()
        {
            var fields = await _fieldRepo.GetAllAsync(); // đảm bảo IFieldRepository đã được inject
            ViewBag.FieldList = new SelectList(fields, "FieldID", "FieldName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (!ModelState.IsValid)
            {
                var fields = await _fieldRepo.GetAllAsync();
                ViewBag.FieldList = new SelectList(fields, "FieldID", "FieldName");
                return View(company);
            }

            await _companyRepo.AddAsync(company);
            await _companyRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            var fields = await _fieldRepo.GetAllAsync();
            ViewBag.FieldList = new SelectList(fields, "FieldID", "FieldName", company.FieldID);

            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Company company)
        {
            if (!ModelState.IsValid) return View(company);
            _companyRepo.Update(company);
            await _companyRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Companies
                .Include(c => c.Field)
                .FirstOrDefaultAsync(c => c.CompanyID == id);
            if (company == null) return NotFound();
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
            if (company != null)
            {
                _companyRepo.Delete(company);
                await _companyRepo.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
