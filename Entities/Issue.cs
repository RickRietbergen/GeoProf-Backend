namespace GeoProf.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public bool IsCompleted { get; set; }
    }
}
