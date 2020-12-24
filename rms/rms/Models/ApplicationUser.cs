using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace rms.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SignupDate { get; set; }
        public List<ApplicationSource> Sources { get; set; }
        public List<ApplicationActivity> Activities { get; set; }
    }
}
