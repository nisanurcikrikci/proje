using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;
using System.Security.Claims;

namespace proje.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TrainerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Profilim()
        {
            var userId = _userManager.GetUserId(User);

            var trainer = _context.Trainers
                .Include(t => t.TrainerUzmanliklar)
                    .ThenInclude(tu => tu.Uzmanlik)
                .FirstOrDefault(t => t.IdentityUserId == userId);

            ViewBag.Uzmanliklar = _context.Uzmanlik.ToList();

            return View(trainer);
        }

        [HttpPost]
        public IActionResult Profilim(int boy, int kilo, List<int> uzmanliklar)
        {
            var userId = _userManager.GetUserId(User);

            var trainer = _context.Trainers
                .Include(t => t.TrainerUzmanliklar)
                .FirstOrDefault(t => t.IdentityUserId == userId);

            trainer.Boy = boy;
            trainer.Kilo = kilo;

            // Eski uzmanlıkları sil
            _context.TrainerUzmanlik.RemoveRange(trainer.TrainerUzmanliklar);

            // Yenileri ekle
            foreach (var uId in uzmanliklar)
            {
                _context.TrainerUzmanlik.Add(new TrainerUzmanlik
                {
                    TrainerId = trainer.Id,
                    UzmanlikId = uId
                });
            }

            _context.SaveChanges();

            TempData["Basarili"] = "Profil güncellendi";
            return RedirectToAction("Profilim");
        }
        public IActionResult Randevularim()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainer = _context.Trainers
                .FirstOrDefault(t => t.IdentityUserId == userId);

            if (trainer == null)
                return Unauthorized(); // veya özel bir hata sayfası

            var randevular = _context.Randevular
                .Where(r => r.TrainerId == trainer.Id && r.Durum!="Iptal")
            .Include(r => r.Musteri)
            .Where(r => r.TrainerId == trainer.Id)
            .OrderBy(r => r.Tarih)
            .ThenBy(r => r.BaslangicSaat)
            .ToList();
            return View(randevular);
        }

        [HttpPost]
        [Authorize(Roles = "Trainer")]
        public IActionResult Onayla(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
                return NotFound();

            randevu.Durum = "Aktif";
            _context.SaveChanges();

            return RedirectToAction("Randevularim");
        }

        [HttpPost]
        [Authorize(Roles = "Trainer")]
        public IActionResult Reddet(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
                return NotFound();

            randevu.Durum = "Iptal";
            _context.SaveChanges();

            return RedirectToAction("Randevularim");
        }

    }

}
