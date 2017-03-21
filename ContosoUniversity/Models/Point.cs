using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Point
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int RestaurantID { get; set; }
        public int GivenPoint { get; set; }
        public virtual Person person { get; set; }
        public virtual Restaurant restaurant { get; set; }


    }
}