using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoursework.Models
{
    public class Announcement
    {
        [Key]
        public int AnnouncementId { get; set; }

        [Required, MinLength(3), MaxLength(140)]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        public int Views { get; set; }

        public String FirstName { get; set; }

        public String Lastname { get; set; }

        public DateTime Time { get; set; }
    }
}
