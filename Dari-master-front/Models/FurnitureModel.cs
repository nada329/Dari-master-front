using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dari_master_front.Models
{
    public class FurnitureModel
    {
        [Key]
        public int id { get; set; }
        [StringLength(100)]
        [Required]
        public String libelle { get; set; }
        [StringLength(100)]
        [Required]
        public String description { get; set; }
        [DataType(DataType.Currency)]
        public float price { get; set; }
        [DataType(DataType.Date)]
        public DateTime postDate { get; set; }
    }
}