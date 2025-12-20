using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;

namespace proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        

        // 🔹 Admin Anasayfa
        public IActionResult Index()
        {
            return View();
        }

        // 🔹 Müşteriler
        public IActionResult Musteriler()
        {
            var musteriler = _context.Musteriler.ToList();
            return View(musteriler);
        }

        // 🔹 Antrenörler
        public IActionResult Trainerlar()
        {
            var trainerlar = _context.Trainers.ToList();
            return View(trainerlar);
        }

        // 🔹 Aktif Randevular
        public IActionResult Randevular()
        {
            var randevular = _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Trainer)
                .Where(r => r.Durum=="Aktif")
                .OrderBy(r => r.Tarih)
                .ToList();

            return View(randevular);
        }

        public IActionResult TrainerEkle()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> TrainerEkle(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (await _userManager.IsInRoleAsync(user, "Musteri"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Musteri");
            }

            // 🔥 2️⃣ Trainer rolünü EKLE
            if (!await _userManager.IsInRoleAsync(user, "Trainer"))
            {
                await _userManager.AddToRoleAsync(user, "Trainer");
            }


            bool trainerVarMi = _context.Trainers
    .Any(t => t.IdentityUserId == user.Id);

            if (!trainerVarMi)
            {
                var trainer = new Trainer
                {
                    AdSoyad = user.Email, // sonra düzenlersin
                    AktifMi = true,
                    IdentityUserId = user.Id
                };

                _context.Trainers.Add(trainer);
                await _context.SaveChangesAsync();
            }

            TempData["Basarili"] = "Kullanıcı antrenör yapıldı.";
            return RedirectToAction("Trainerlar");
        }


    }
}
