using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.UserList = new SelectList(await _context.Users.ToListAsync(), "UserID", "FullName");
            ViewBag.JobList = new SelectList(await _context.JobPositions.ToListAsync(), "JobID", "Title");

            return View(app);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobApplication app)
        {
            if (!ModelState.IsValid)
            {
                app.User = await _context.Users.FindAsync(app.UserID);
                app.JobPosition = await _context.JobPositions.FindAsync(app.JobID);
                return View(app);
            }

            try
            {
                // Update entity trực tiếp (nếu app là object đầy đủ và không thiếu trường)
                _context.JobApplications.Update(app);
                await _context.SaveChangesAsync();

                // Chuyển hướng về Index nếu thành công
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi khi cập nhật: " + ex.Message);
                ModelState.AddModelError("", "Không thể cập nhật dữ liệu.");
                app.User = await _context.Users.FindAsync(app.UserID);
                app.JobPosition = await _context.JobPositions.FindAsync(app.JobID);
                return View(app);
            }
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

        //Apply (ỨNG TUYỂN)
        public IActionResult Apply(int jobId)
        {
            ViewBag.JobID = jobId; // truyền JobID qua View để hidden field
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(int JobID, string FullName, string Email, string Phone, IFormFile CVFile)
        {
            if (CVFile != null && CVFile.Length > 0)
            {
                var fileName = Path.GetFileName(CVFile.FileName);
                var folderPath = Path.Combine("wwwroot", "uploads", "cv");
                Directory.CreateDirectory(folderPath); // Đảm bảo thư mục tồn tại

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CVFile.CopyToAsync(stream);
                }

                // Tạo mới user (nếu chưa có đăng nhập)
                var user = new User
                {
                    FullName = FullName,
                    Email = Email,
                    Phone = Phone,
                    Address = "Không xác định"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

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
    }
}
