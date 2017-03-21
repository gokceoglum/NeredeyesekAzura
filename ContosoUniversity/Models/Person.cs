using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Point> GivenPoints { get; set; }


    }
}

















