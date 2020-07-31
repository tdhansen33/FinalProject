using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ReservationFinalProject.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*** Name is Required ***")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*** Email is Required ***")]
        [EmailAddress(ErrorMessage = "*** Please provide a valid email ***")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*** Subject is Required ***")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "*** Message is Required ***")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}