using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCoursework.Models;
using WebCoursework.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCoursework.Controllers
{
    
    public class UserManagementController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagementController(DatabaseContext dbContext, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        // GET: /<controller>/
        [Authorize(Roles = "Staff")]
        public IActionResult Index()
        {
            var vm = new UserManagementIndexViewModel
            {
                Users = _dbContext.Users.OrderBy(u => u.Email)
                                         .Include(u => u.Roles)
                                         .ToList()
            };

            return View(vm);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            var user = await GetUserById(id);

            var vm = new UserManagementAddRoleViewModel
            {
                Roles = GetAllRoles(),
                UserId = id,
                Email = user.Email
            };
            return View(vm);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public async Task<IActionResult> AddRole(UserManagementAddRoleViewModel rvm)
        {
            var user = await GetUserById(rvm.UserId);

            if (ModelState.IsValid)
            {
                var result = await _userManager.AddToRoleAsync(user, rvm.NewRole);
                if (result.Succeeded) {
                    return RedirectToAction("Index", "UserManagement");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            rvm.Roles = GetAllRoles();
            rvm.Email = user.Email;

            return View(rvm);
        }
        private async Task<ApplicationUser> GetUserById(String id) { 
            return await _userManager.FindByIdAsync(id);
        }

        private SelectList GetAllRoles() {
            return new SelectList(_roleManager.Roles.OrderBy(r => r.Name));
        }
    }
}
