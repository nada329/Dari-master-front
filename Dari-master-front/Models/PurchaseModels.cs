using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dari_frontend.Models
{
    public class PurchaseFormModel
    {
        [Required]
        [CreditCard]
        [Display(Name = "Card Number")]
        public int CardNumber;
        [Required]
        [Range(1, 12)]
        [Display(Name = "Expiration Month")]
        public int ExpMonth;
        [Required]
        [Range(21, 30)]
        [Display(Name = "Expiration Year")]
        public int ExpYear;
        [Required]
        [MinLength(3)]
        public int CCV;
    }
}