using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Statistic
    {
        public int ID { get; set; }
        public int RestaurantID { get; set; }
        public int DaysToGo { get; set; }
        public int DaysLeft { get; set; }
        public virtual Restaurant restaurant { get; set; }


    }
}