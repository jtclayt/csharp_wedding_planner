namespace WeddingPlanner.Models
{
    public class WeddingsWrapper {
        public User User { get; set; }
        public Wedding[] AllWeddings { get; set; }
    }
}
