namespace proje.Models
{
    public class Uzmanlik
    {
        public int Id { get; set; }
        public string Ad { get; set; }

        public ICollection<TrainerUzmanlik> TrainerUzmanlik { get; set; }
    }


}
