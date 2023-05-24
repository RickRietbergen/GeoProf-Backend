namespace GeoProf.Models
{
    public class VerlofCreateModel
    {
        public string VerlofReden { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Beschrijving { get; set; }
        public bool isPending { get; set; }
        public bool isApproved { get; private set; }
    }
}
