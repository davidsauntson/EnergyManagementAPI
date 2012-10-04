using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnergyManagerWebApp.Models
{
    public class UserViewModel
    {

        public string OldPassword { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Length must be between 7 and 100 characters")]
        [Required(ErrorMessage="Required")]
        public string ConfirmNewPassword { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Length must be between 7 and 100 characters")]
        [Required(ErrorMessage="Required")]
        public string NewPassword { get; set; }

        
        public string Username { get; set; }
        public bool CreateAPropertyNow { get; set; }

        public User User { get; set; }
    }
}