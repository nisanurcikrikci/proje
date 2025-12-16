namespace proje.Models
{
    public class TrainerUzmanlik
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public int UzmanlikId { get; set; }
        public Uzmanlik Uzmanlik { get; set; }
    }

}
