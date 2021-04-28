using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dari_frontend.Models
{
    public class RentalModel
    {
        public PropertyModel Property { get; set; }
        public UserModel Tenant { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; }
        [DataType(DataType.Date)]
        [Display(Name = "Expected End Date")]
        public DateTime ExpectedEndDate { get; set; }
    }

    public class RentalGrantModel
    {
        [Key]
        [Url]
        public string Id { get; }
        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFile File { get; set; }
        [Required]
        public RentalGrantType Type { get; set; }
    }

    public enum RentalGrantType
    {
        PAYSLIP,
        ID,
        COMMITMENT_LETTER,
        AGREEMENT_LETTER
    }

    public class RentFormModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expected End Date")]
        [FutureDate]
        public DateTime ExpectedEndDate { get; set; }
        [Required]
        [CreditCard]
        [Display(Name = "Card Number")]
        public int CardNumber { get; set; }
        [Required]
        [Range(1, 12)]
        [Display(Name = "Expiration Month")]
        public int ExpMonth { get; set; }
        [Required]
        [Range(21, 30)]
        [Display(Name = "Expiration Year")]
        public int ExpYear { get; set; }
        [Required]
        [MinLength(3)]
        public int CCV { get; set; }
    }

    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return ((DateTime)value).CompareTo(DateTime.Now) > 0;
        }
    }
}