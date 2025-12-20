using Microsoft.AspNetCore.Identity;

namespace proje.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        public string AdSoyad { get; set; }

        public int? Boy { get; set; }
        public int? Kilo { get; set; }

        public bool AktifMi { get; set; }

        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public ICollection<TrainerUzmanlik> TrainerUzmanliklar { get; set; }
        public ICollection<Randevu> Randevular { get; set; }
    }

}
