namespace GeoProf.Models
{
    public class IssueCreationModel
    {
        public int UserId { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }
        public double Hours { get; set; }
    }
}
