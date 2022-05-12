using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
