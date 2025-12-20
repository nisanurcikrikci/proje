using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using System.Security.Claims;

namespace proje.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public TrainerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Randevularim()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainer = _context.Trainers
                .FirstOrDefault(t => t.IdentityUserId == userId);

            if (trainer == null)
                return Unauthorized(); // veya özel bir hata sayfası

            var randevular = _context.Randevular
                .Where(r => r.TrainerId == trainer.Id && r.Durum=="Aktif")
                .Include(r => r.Musteri)
                .OrderBy(r => r.Tarih)
                .ToList();

            return View(randevular);
        }

    }

}
