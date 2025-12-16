namespace proje.Models
{
    public class TrainerHizmet
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public int HizmetId { get; set; }
        public Hizmet Hizmet { get; set; }
    }

}
