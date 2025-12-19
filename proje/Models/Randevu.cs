using Microsoft.AspNetCore.Mvc;

namespace proje.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        public int TrainerId { get; set; }
        public int MusteriId { get; set; }   // Üye

        public DateTime Tarih { get; set; }
        public string Saat { get; set; }

        public bool AktifMi { get; set; } = true;

        // navigation (opsiyonel ama güzel)
        public Trainer Trainer { get; set; }
        public Musteri Musteri { get; set; }
    }

}
