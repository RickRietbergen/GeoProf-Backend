namespace GeoProf.Entities
{
    public class Verlof
    {
        public int Id { get; set; }
        public string VerlofReden { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public bool isPending { get; set;}
        public bool isApproved { get; private set; }
    }
}
