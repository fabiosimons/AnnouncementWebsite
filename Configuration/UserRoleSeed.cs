using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoursework.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebCoursework.Configuration
{
    public class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
                _roleManager = roleManager;
          
        }
        public async void Seed()
        {
            // Create Generic User
            if((await _roleManager.FindByNameAsync("User")) == null)
            {
               await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }
            // Create Staff
            if ((await _roleManager.FindByNameAsync("Staff")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Staff" });
            }
        }
    }
}
