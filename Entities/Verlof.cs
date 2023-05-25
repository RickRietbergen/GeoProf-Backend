namespace GeoProf.Entities
{
    public class Verlof
    {
        public int Id { get; set; }
        public string VerlofReden { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public string Beschrijving { get; set; }
        public bool IsPending { get; set;}
        public bool IsApproved { get; set; }
    }
}
