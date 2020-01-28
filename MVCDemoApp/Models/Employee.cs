using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemoApp.Models
{
    public class Employee
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter Name")]
        public string name { get; set; }

        [Display(Name = "Email_Id")]
        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Remote("VerifyEmail","Employee", HttpMethod ="POST", AdditionalFields = "id" , ErrorMessage ="email is alrready exist")]
        public string email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(8, ErrorMessage = "The Mobile must contains 6 characters", MinimumLength = 6)]
        public string password { get; set; }

        [Display(Name = "Mobile_Number")]
        [Required(ErrorMessage = "Please Enter Mobile No")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string mobile { get; set; }

        [Display(Name = "Date-Of-Birth")]
        [Required(ErrorMessage = "Please Enter Date-Of-Birth")]
        public string dob { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Enter Gender")]
        public string gender { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please Enter Department")]
        public string department { get; set; }

        
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter City")]
        public string city { get; set; }

        public string Actions { get; set; }

    }

    public class LoginViewModel

    {

            [Display(Name = "Email_Id")]
            [Required(ErrorMessage = "please Enter Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "please Enter Password")]
            public string Password { get; set; }
        

    }
}