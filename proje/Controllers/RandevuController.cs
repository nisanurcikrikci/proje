using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;

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
            var saatler = new List<string>
    {
        "09:00", "10:00", "11:00", "12:00",
        "13:00", "14:00", "15:00", "16:00", "17:00"
    };

            return Json(saatler);
        }

        public IActionResult Test()
        {
            return Content("Randevu Controller Çalışıyor");
        }


        [HttpPost]
        public IActionResult RandevuAl(int trainerId, DateTime tarih, string saat)
        {
            // ŞİMDİLİK TEST
            return Ok(new
            {
                Mesaj = "Randevu başarıyla alındı",
                TrainerId = trainerId,
                Tarih = tarih.ToShortDateString(),
                Saat = saat
            });
        }

    }
}
