using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dari_frontend.Models
{
    public class PropertyModel
    {
        [Key]
        public int id { get; set; }
        [StringLength(100)]
        [Required]
        public string title { get; set; }
        [StringLength(100)]
        [Required]
        public string description { get; set; }
        [Required]
        public int rooms { get; set; }
        [Required]
        public double area { get; set; }
        [DataType(DataType.Date)]
        public DateTime postDate { get; set; }
        [Required]
        public Boolean is_for_sale { get; set; }
        [Required]
        public Boolean is_for_rental { get; set; }
        [Range(0, 9999999)]
        public double salePrice { get; set; }
        [Range(0, 9999999)]
        public double rentalPrice { get; set; }
    
        public UserModel owner { get; set; }
    }
    public struct Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}