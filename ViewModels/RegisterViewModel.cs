using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoursework.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Required*")]
        [EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Required*")]
        [MinLength(6,ErrorMessage ="Must Contain at least 6 characters"), MaxLength(20), DataType(DataType.Password),Display(Name = "Password")]
        public String Password { get; set; }

        [Required(ErrorMessage = "Required*")]
        [MinLength(6, ErrorMessage = "Must Contain at least 6 characters"), MaxLength(20)]
        [DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required*")]
        [MinLength(2, ErrorMessage = "Must Contain at least 2 characters"), MaxLength(25, ErrorMessage = "Must not exceed 25 characters")] 
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Required*")]
        [MinLength(2, ErrorMessage = "Must Contain at least 2 characters"), MaxLength(25, ErrorMessage = "Must not exceed 25 characters")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }


    }
}
