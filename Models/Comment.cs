using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoursework.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    
        [ForeignKey("Announcement"),Required]
        public virtual Announcement MyAnnouncement { get; set; }

        [Required(ErrorMessage ="Required"), MinLength(3), MaxLength(350)]
        public string Message { get; set; }

    }
}
