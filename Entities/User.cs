using GeoProf.Enums;

namespace GeoProf.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Vakantie { get; set; }
        public int Persoonlijk { get; set; }
        public int Ziek { get; set; }
        public Role Role { get; set; }
    }
}
