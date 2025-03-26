using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DACS_WebTimKiemViecLam.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepo.GetAllAsync();
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid) return View(user);
            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid) return View(user);
            _userRepo.Update(user);
            await _userRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user != null)
            {
                _userRepo.Delete(user);
                await _userRepo.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
