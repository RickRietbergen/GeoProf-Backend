namespace GeoProf.Models
{
    public class VerlofTableModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string VerlofReden { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public string Beschrijving { get; set; }
        public bool IsPending { get; set; }
        public bool IsDenied { get; set; }
        public bool IsApproved { get; set; }
        public int Vakantie { get; set; }
        public int Persoonlijk{ get; set; }
        public int Ziek { get; set; }
    }
}
