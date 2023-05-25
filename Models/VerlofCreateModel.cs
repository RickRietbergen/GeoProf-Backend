﻿namespace GeoProf.Models
{
    public class VerlofCreateModel
    {
        public string VerlofReden { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Beschrijving { get; set; }
        public bool IsPending { get; set; }
        public bool IsApproved { get; private set; }
    }
}
