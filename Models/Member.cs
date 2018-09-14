using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoursework.Models
{
    public class Member
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must be between 2 - 25 characters."),
         MaxLength(25, ErrorMessage = "Name must be between 2 - 25 characters.")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Surname must be between 2 - 25 characters."), 
         MaxLength(25, ErrorMessage = "Surname must be between 2 - 25 characters.")]

        public string LastName { get; set; }
        
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool Admin { get; set; }
    }
}
