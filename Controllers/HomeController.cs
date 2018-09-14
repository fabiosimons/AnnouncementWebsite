using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCoursework.Models;

namespace WebCoursework.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult SignUp(Member member)
        {
            if(ModelState.IsValid)
            {
                member = new Member();
            }
            return View("SignUp");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
