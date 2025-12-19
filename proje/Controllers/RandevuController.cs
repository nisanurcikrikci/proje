using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;
using System.Security.Claims;

namespace proje.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var hizmetler = _context.Hizmetler
                .OrderBy(h => h.Ad)
                .ToList();

            ViewBag.Hizmetler = hizmetler;
            return View();
        }

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
        [HttpGet]
        public IActionResult GetUygunTarihler(int trainerId)
        {
            var bugun = DateTime.Today;
            var gunler = new List<string>();

            for (int i = 0; i < 14; i++)
            {
                var tarih = bugun.AddDays(i);
                gunler.Add(tarih.ToString("yyyy-MM-dd"));
            }

            return Json(gunler);
        }




        [HttpGet]
        public IActionResult GetUygunSaatler(int trainerId, DateTime tarih)
        {
            var calismaSaatleri = new List<string>
    {
        "09:00","10:00","11:00","12:00","13:00",
        "14:00","15:00","16:00","17:00"
    };

            var doluSaatler = _context.Randevular
                .Where(r => r.TrainerId == trainerId && r.Tarih == tarih)
                .Select(r => r.Saat)
                .ToList();

            var bosSaatler = calismaSaatleri
                .Except(doluSaatler)
                .ToList();

            return Json(bosSaatler);
        }

        public IActionResult Test()
        {
            return Content("Randevu Controller Çalışıyor");
        }


        [HttpGet]
        public IActionResult RandevuAl()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteri = _context.Musteriler
                .FirstOrDefault(m => m.IdentityUserId == userId);


            if (musteri.Boy == null || musteri.Kilo == null)
            {
                TempData["Hata"] = "Randevu almak için boy ve kilo bilgilerinizi girmeniz gerekir.";
                return RedirectToAction("Profilim", "Musteri");
            }

            return View();
        }

        [HttpPost]
        public IActionResult RandevuAl(int trainerId, DateTime tarih, string saat)
        {
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteri = _context.Musteriler
                .FirstOrDefault(m => m.IdentityUserId == identityUserId);

            if (musteri == null)
            {
                return BadRequest("Müşteri bulunamadı");
            }
            // Aynı saat dolu mu kontrol (çok önemli ⭐)
            bool doluMu = _context.Randevular.Any(r =>
                r.TrainerId == trainerId &&
                r.Tarih == tarih &&
                r.Saat == saat
            );

            if (doluMu)
            {
                TempData["Hata"] = "Bu saat dolu, lütfen başka bir saat seçiniz.";
                return RedirectToAction("RandevuAl");
            }

            var randevu = new Randevu
            {
                TrainerId = trainerId,
                MusteriId = musteri.Id,
                Tarih = tarih,
                Saat = saat,
                AktifMi = true
            };

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            TempData["Basarili"] = "Randevu başarıyla alındı.";
            return RedirectToAction("Index");
        }


    }
}
