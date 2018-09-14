using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoursework.Models;

namespace WebCoursework
{
    public static class InitialiseDatabase
    {
        //initialise and seed the members, announcements and comments of the website if there are none.
        public static async Task InitialiseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {

                ApplicationUser member1 = CreateUser("Member1@email.com", "Member1@email.com", "Member", "One");
                ApplicationUser customer1 = CreateUser("Customer1@email.com", "Customer1@email.com", "Customer", "One");
                ApplicationUser customer2 = CreateUser("Customer2@email.com", "Customer2@email.com", "Customer", "Two");
                ApplicationUser customer3 = CreateUser("Customer3@email.com", "Customer3@email.com", "Customer", "Three");
                ApplicationUser customer4 = CreateUser("Customer4@email.com", "Customer4@email.com", "Customer", "Four");
                Announcement testAnnouncement = CreateAnnouncement("TEST ANNOUNCEMENT", "Test message, this is the announcement content", DateTime.Now, 1);

                context.Announcements.Add(testAnnouncement);
                await context.SaveChangesAsync();

                //create users
                await userManager.CreateAsync(member1, "password");
                await userManager.CreateAsync(customer1, "password");
                await userManager.CreateAsync(customer2, "password");
                await userManager.CreateAsync(customer3, "password");
                await userManager.CreateAsync(customer4, "password");
            }
            else
            {
            }
            //// Add Roles
            //await userManager.AddToRoleAsync(member1, "Staff");
            //await userManager.AddToRoleAsync(customer1, "User");
            //await userManager.AddToRoleAsync(customer2, "User");
            //await userManager.AddToRoleAsync(customer3, "User");
            //await userManager.AddToRoleAsync(customer4, "User");
        }

        //Create a User from the values input
        public static ApplicationUser CreateUser(String email, String username, String firstname, String lastname)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = email,
                UserName = username,
                FirstName = firstname,
                LastName = lastname
            };
            return user;
        }
        //Create an announcement from the values input
        public static Announcement CreateAnnouncement(String title, String message, DateTime time, int views)
        {
            Announcement announcement = new Announcement
            {
                Title = title,
                Message = message,
                Time = time,
                Views = views
            };

            return announcement;
        }

        //}
    }
}
