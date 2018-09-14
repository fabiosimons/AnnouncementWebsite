using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoursework.Models;

namespace WebCoursework.ViewModels
{
    public class AnnouncementDetailsViewModel
    {
        public Announcement Announcement { get; set; }
        public List<Comment> Comments { get; set; }
        public int AnnouncementId { get; set; }
        public string Message { get; set; }
    }
}
