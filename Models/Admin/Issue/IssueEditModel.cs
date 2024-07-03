namespace GeoProf.Models.Admin.Issue
{
    public class IssueEditModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }
        public double Hours { get; set; }
        public bool IsCompleted { get; set; }
    }
}
