using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoursework.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public String FirstName { get; set; }
        public String LastName { get; set; }

    }
}
