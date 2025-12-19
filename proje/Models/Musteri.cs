using System.ComponentModel.DataAnnotations;

namespace proje.Models
{
    public class Musteri
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100)]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon giriniz")]
        public string Telefon { get; set; }

        // Profil bilgileri
        public double? Boy { get; set; }   // cm
        public double? Kilo { get; set; }  // kg

        // Kullanıcının hedefi
        public string? OdaklanilanAlan { get; set; }

        // Navigation
        public string IdentityUserId { get; set; }
        public ICollection<Randevu> Randevular { get; set; }
    }
}
