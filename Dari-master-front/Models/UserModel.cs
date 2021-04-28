using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dari_frontend.Models
{
    public class UserModel
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public String Birthdate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        [Required]
        [Phone]
        public int Phone { get; set; }
        public string Picture { get; set; }
        public string Grade { get; set; }
    }
}