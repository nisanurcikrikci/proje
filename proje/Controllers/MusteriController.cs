using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;
using System;
using System.Security.Claims;

namespace proje.Controllers
{
    [Authorize]
    public class MusteriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusteriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // PROFİLİM - GET
        public IActionResult Profilim()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 🔥 O KULLANICIYA AİT MUSTERI
            var musteri = _context.Musteriler
                .Include(m => m.Randevular)
                    .ThenInclude(r => r.Trainer)
                .FirstOrDefault(m => m.IdentityUserId == userId);

            if (musteri == null)
            {
                return Content("Bu kullanıcıya ait müşteri bulunamadı.");
            }

            return View(musteri);

        }

        // PROFİLİM - POST
        [HttpPost]
        public IActionResult Profilim(Musteri model)
        {
            var musteri = _context.Musteriler.Find(model.Id);

            if (musteri == null)
                return NotFound();

            musteri.AdSoyad = model.AdSoyad;
            musteri.Email = model.Email;
            musteri.Telefon = model.Telefon;
            musteri.Boy = model.Boy;
            musteri.Kilo = model.Kilo;
            musteri.OdaklanilanAlan = model.OdaklanilanAlan;

            _context.SaveChanges();

            ViewBag.Mesaj = "Profil başarıyla güncellendi";
            return View(musteri);
        }
       
        [HttpPost]
        public async Task<IActionResult> RandevuIptal(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
                return NotFound();

            var userEmail = User.Identity.Name;

            var musteri = await _context.Musteriler
                .FirstOrDefaultAsync(x => x.Email == userEmail);

            if (musteri == null)
                return Unauthorized();

            if (randevu.MusteriId != musteri.Id)
                return Unauthorized();

            if (randevu.Durum == "Beklemede" || randevu.Durum == "Aktif")
            {
                randevu.Durum = "Iptal";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Randevu");

        }

    }
}
