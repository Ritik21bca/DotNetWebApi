using Microsoft.AspNetCore.Identity;
using System;

namespace Assignment.Entites
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; }

        public DateTime ExpiredTime { get; set; }
        public Guid GuidId { get; set; }
        public string EmailId { get; set; }
    }
}
