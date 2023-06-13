using GeoProf.Enums;

namespace GeoProf.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Vakantie { get; set; }
        public int Persoonlijk { get; set; }
        public int Ziek { get; set; }
        public int AfdelingId { get; set; }
        public Role Role { get; set; }
        public Afdeling Afdeling { get; set; }
    }
}
