using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace DACS_WebTimKiemViecLam.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly IJobApplicationRepository _jobAppRepo;
        private readonly JobDbContext _context;

        public JobApplicationsController(IJobApplicationRepository jobAppRepo, JobDbContext context)
        {
            _jobAppRepo = jobAppRepo;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var apps = await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.JobPosition)
                    .ThenInclude(j => j.Company)
                .ToListAsync();
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
            var app = await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.JobPosition)
                .FirstOrDefaultAsync(a => a.ApplicationID == id);

            if (app == null) return NotFound();

            return View(app);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobApplication app)
        {
            var existing = await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.JobPosition)
                .FirstOrDefaultAsync(a => a.ApplicationID == app.ApplicationID);

            if (existing == null) return NotFound();

            // Chỉ cập nhật trạng thái, giữ nguyên các trường còn lại
            existing.Status = app.Status;

            try
            {
                _context.JobApplications.Update(existing);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Cập nhật trạng thái thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi khi cập nhật: " + ex.Message);
                ModelState.AddModelError("", "Đã xảy ra lỗi. Vui lòng thử lại.");
                return View(existing);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var app = await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.JobPosition)
                .FirstOrDefaultAsync(a => a.ApplicationID == id);

            if (app == null) return NotFound();
            return View(app);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var app = await _context.JobApplications.FindAsync(id);
            if (app != null)
            {
                _context.JobApplications.Remove(app);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        //Apply (ỨNG TUYỂN)
        public IActionResult Apply(int jobId)
        {
            ViewBag.JobID = jobId; // truyền JobID qua View để hidden field
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(int JobID, string FullName, string Email, string Phone, string Address, IFormFile CVFile)
        {
            if (CVFile != null && CVFile.Length > 0)
            {
                var fileName = Path.GetFileName(CVFile.FileName);
                var folderPath = Path.Combine("wwwroot", "uploads", "cv");
                Directory.CreateDirectory(folderPath); // Tạo thư mục nếu chưa có

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CVFile.CopyToAsync(stream);
                }

                // Kiểm tra xem user đã tồn tại chưa (theo Email và Phone)
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == Email && u.Phone == Phone);

                User user;

                if (existingUser != null)
                {
                    user = existingUser;
                }
                else
                {
                    user = new User
                    {
                        FullName = FullName,
                        Email = Email,
                        Phone = Phone,
                        Address = Address 
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }

                // Tạo đơn ứng tuyển
                var app = new JobApplication
                {
                    JobID = JobID,
                    UserID = user.UserID,
                    CVFilePath = "/uploads/cv/" + fileName,
                    Status = "Pending",
                    ApplicationDate = DateTime.Now
                };

                _context.JobApplications.Add(app);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "JobPositions");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var app = await _context.JobApplications.FindAsync(id);
            if (app != null)
            {
                app.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
