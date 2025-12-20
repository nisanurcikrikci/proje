using Microsoft.AspNetCore.Mvc;

namespace proje.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        public int TrainerId { get; set; }
        public int MusteriId { get; set; }

        public DateTime Tarih { get; set; }

        public int BaslangicSaat { get; set; } // 9, 10, 11
        public int BitisSaat { get; set; }     // 13, 14


        public decimal Ucret { get; set; }

        public string OdemeTipi { get; set; } // Nakit / Online

        public string Durum { get; set; } = "Beklemede";
        // Beklemede | Aktif | Iptal

        public Trainer Trainer { get; set; }
        public Musteri Musteri { get; set; }
    }


}
