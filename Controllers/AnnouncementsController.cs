using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCoursework.Models;
using WebCoursework.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace WebCoursework.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnnouncementsController(DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Announcements.ToListAsync());
        }

        // GET: Announcements/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Announcement announcement = await _context.Announcements.SingleOrDefaultAsync(m => m.AnnouncementId == id);
          

            if (announcement == null)
            {
                return NotFound();
            }

            await ViewCounter(announcement);

            AnnouncementDetailsViewModel vm = new AnnouncementDetailsViewModel();

            vm.Announcement = announcement;

            List<Comment> comments = await _context.Comments
                .Where(x => x.MyAnnouncement == announcement).ToListAsync();


            vm.Comments = comments;
            
            return View(vm);
        }

        public async Task ViewCounter(Announcement announcement)
        {
           announcement.Views = announcement.Views + 1;
           await _context.SaveChangesAsync();
        }
        // post: Announcements/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("AnnouncementId", "Message")] AnnouncementDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();

                comment.Message = vm.Message;

                ApplicationUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;


                Announcement announcement = await _context.Announcements
                     .SingleOrDefaultAsync(m => m.AnnouncementId == vm.AnnouncementId);
                if (announcement == null)
                {
                    return NotFound();
                }

                comment.Email = user.Email;
                comment.FirstName = user.FirstName;
                comment.LastName = user.LastName;
                comment.MyAnnouncement = announcement;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                vm = await GetAnnouncementDetailsViewModelFromAnnouncement(announcement);
            }

            return View(vm);
        }
        private async Task<AnnouncementDetailsViewModel> GetAnnouncementDetailsViewModelFromAnnouncement(Announcement announcement)
        {
            AnnouncementDetailsViewModel vm = new AnnouncementDetailsViewModel();

            vm.Announcement = announcement;

            //get comment from database
            List<Comment> comments = await _context.Comments
                .Where(x => x.MyAnnouncement == announcement).ToListAsync();

            vm.Comments = comments;
            return vm;
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnnouncementId,Title,Message,Views")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                var time = DateTime.Now;
                announcement.Time = time;
                ApplicationUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

                announcement.FirstName = user.FirstName;
                announcement.Lastname = user.LastName;

                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements.SingleOrDefaultAsync(m => m.AnnouncementId == id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnnouncementId,Title,Message,Views,Time")] Announcement announcement)
        {
            if (id != announcement.AnnouncementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.AnnouncementId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .SingleOrDefaultAsync(m => m.AnnouncementId == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var announcement = await _context.Announcements.SingleOrDefaultAsync(m => m.AnnouncementId == id);
            _context.Announcements.Remove(announcement);   

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcements.Any(e => e.AnnouncementId == id);
        }
    }
}
