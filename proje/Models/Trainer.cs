namespace proje.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public bool AktifMi { get; set; }
        public string IdentityUserId { get; set; }

        public ICollection<Randevu> Randevular { get; set; }
    }

}
