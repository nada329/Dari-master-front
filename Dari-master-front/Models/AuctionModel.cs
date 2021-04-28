using dari_frontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dari_master_front.Models
{
    public class AuctionModel
    {
        [Key]
        public  int id { get; set; }
        public String auction_name { get; set; }
        public float startingprice { get; set; }
        public float price { get; set; }
        public Boolean onGoing { get; set; }
        public DateTime postdate { get; set; }
        public FurnitureModel furniture;
        public UserModel highestbidder { get; set; }

    }
}