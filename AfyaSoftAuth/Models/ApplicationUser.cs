using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AfyaSoftAuth.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name {get;set;} = string.Empty;
        public string ProfileImage {get;set;} = string.Empty;
        public string Phone {get;set;} = string.Empty;
        public string Address {get;set;} = string.Empty;
        public string Gender {get;set;} = string.Empty;
    }
}
