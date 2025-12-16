using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace proje.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AdSoyad { get; set; }

        public int Boy { get; set; }      // cm
        public int Kilo { get; set; }     // kg

        public string OdakAlan { get; set; }
    }
}
