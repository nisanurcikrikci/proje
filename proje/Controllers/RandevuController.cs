using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;
using System.Security.Claims;

namespace proje.Controllers
{
    [Authorize]
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 Kullanıcının randevuları
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteri = _context.Musteriler
                .Include(m => m.Randevular)
                    .ThenInclude(r => r.Trainer)
                .FirstOrDefault(m => m.IdentityUserId == userId);

            return View(musteri);
        }

        // 🔹 Hizmete göre trainer getir
        [HttpGet]
        public IActionResult GetTrainerByHizmet(int hizmetId)
        {
            var trainerlar = _context.TrainerHizmet
                .Where(th => th.HizmetId == hizmetId)
                .Include(th => th.Trainer)
                .Select(th => new
                {
                    th.Trainer.Id,
                    th.Trainer.AdSoyad
                })
                .Distinct()
                .ToList();

            return Json(trainerlar);
        }

        // 🔹 Randevu alma sayfası
        // 🔹 Randevu alma sayfası
        [HttpGet]
        public IActionResult RandevuAl()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteri = _context.Musteriler
                .FirstOrDefault(m => m.IdentityUserId == userId);

            if (musteri == null)
                return Unauthorized();

            if (musteri.Boy == null || musteri.Kilo == null)
            {
                TempData["Hata"] = "Randevu almak için boy ve kilo bilgilerinizi girmeniz gerekir.";
                return RedirectToAction("Profilim", "Musteri");
            }

            // 🔥 İŞTE BURAYA
            var hizmetler = _context.Hizmetler.ToList();
            ViewBag.Hizmetler = hizmetler;

            return View();
        }


        // 🔹 Randevu oluştur (POST)
        [HttpPost]
        public IActionResult RandevuAl(
     int trainerId,
     DateTime tarih,
     int baslangicSaat,
     int bitisSaat,
     decimal ucret,
     string odemeTipi
 )
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteri = _context.Musteriler
                .FirstOrDefault(m => m.IdentityUserId == userId);

            if (musteri == null)
                return BadRequest();

            if (bitisSaat <= baslangicSaat)
            {
                TempData["Hata"] = "Saat aralığı hatalı";
                return RedirectToAction("RandevuAl");
            }

            bool doluMu = _context.Randevular.Any(r =>
                r.TrainerId == trainerId &&
                r.Tarih == tarih &&
                baslangicSaat < r.BitisSaat &&
                bitisSaat > r.BaslangicSaat
            );

            if (doluMu)
            {
                TempData["Hata"] = "Bu saat dolu";
                return RedirectToAction("RandevuAl");
            }

            var randevu = new Randevu
            {
                TrainerId = trainerId,
                MusteriId = musteri.Id,
                Tarih = tarih,
                BaslangicSaat = baslangicSaat,
                BitisSaat = bitisSaat,
                Ucret = ucret,
                OdemeTipi = odemeTipi,
                Durum = "Beklemede"
            };

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
