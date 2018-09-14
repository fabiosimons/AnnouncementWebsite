using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCoursework.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebCoursework.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCoursework.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DatabaseContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DatabaseContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

            if (!userManager.Users.Any())
            {
                InitialiseDatabase();
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Login Attempt.");
                return View(vm);
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, FirstName = vm.FirstName, LastName = vm.LastName };
                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async void InitialiseDatabase()
        {
            ApplicationUser member1 = CreateUser("Member1@email.com", "Member1@email.com", "Member", "One");
            ApplicationUser customer1 = CreateUser("Customer1@email.com", "Customer1@email.com", "Customer", "One");
            ApplicationUser customer2 = CreateUser("Customer2@email.com", "Customer2@email.com", "Customer", "Two");
            ApplicationUser customer3 = CreateUser("Customer3@email.com", "Customer3@email.com", "Customer", "Three");
            ApplicationUser customer4 = CreateUser("Customer4@email.com", "Customer4@email.com", "Customer", "Four");
            Announcement testAnnouncement = CreateAnnouncement("TEST ANNOUNCEMENT", "Test message, this is the announcement content", DateTime.Now, 1);

            _context.Announcements.Add(testAnnouncement);
            await _context.SaveChangesAsync();

            //create users
            await _userManager.CreateAsync(member1, "password");
            await _userManager.CreateAsync(customer1, "password");
            await _userManager.CreateAsync(customer2, "password");
            await _userManager.CreateAsync(customer3, "password");
            await _userManager.CreateAsync(customer4, "password");

            //// Add Roles
            await _userManager.AddToRoleAsync(member1, "Staff");
            await _userManager.AddToRoleAsync(customer1, "User");
            await _userManager.AddToRoleAsync(customer2, "User");
            await _userManager.AddToRoleAsync(customer3, "User");
            await _userManager.AddToRoleAsync(customer4, "User");
        }
        public ApplicationUser CreateUser(String email, String username, String firstname, String lastname)
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
        public Announcement CreateAnnouncement(String title, String message, DateTime time, int views)
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
    }
}
