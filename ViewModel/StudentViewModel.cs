using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWebApplicationMVC.ViewModel
{
    public class StudentViewModel
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name", Order = 1, Prompt = "Enter First Name", Description = "Student First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name", Order = 2, Prompt = "Enter Last Name", Description = "Student Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Address", Order = 4, Prompt = "Enter Last Name", Description = "Student Address")]
        public string Address { get; set; }

        [Display(Name = "Gender", Order = 3, Prompt = "Select Gender", Description = "Student's Gender")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Birth date", Order = 5, Prompt = "Select birth date", Description = "Student's date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string PhotoPath { get; set; }
        public int SubjectId { get; set; }
        public HttpPostedFileBase Photo { get; set; }



    }
}