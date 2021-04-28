using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dari_frontend.Models
{
    public class BankModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Interest Rate")]
        [Range(0, 99)]
        public double InterestRate { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Loan Fees")]
        [Range(0, 9999)]
        public double LoanFees { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Max Loan Amount")]
        [Range(0, 9999999)]
        public double MaxLoanAmount { get; set; }
    }

    public class CalculateFormModel
    {
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Loan Amount")]
        [Range(0, 9999999)]
        public double LoanAmount { get; set; }
        [Required]
        [Range(0, 99)]
        public int Period { get; set; }
        public string Result { get; set; }
    }

    public class MessageModel
    {
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; }
        public UserModel Sender { get; }
        public UserModel Recipient { get; }
        public string Content { get; }
    }
}