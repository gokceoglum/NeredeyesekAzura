using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    public class ProjectInitializer : System.Data.Entity.DropCreateDatabaseAlways<ProjectContext>
    {
        protected override void Seed(ProjectContext context)
        {

            var persons = new List<Person>
            {
            new Person{ FirstName = "Hanifi",LastName = "Demirel", Email = "hdemirel16@gmail.com" },
            new Person{ FirstName = "Alper",LastName = "Akyıldız", Email = "alperakyldz@gmail.com" },
            new Person{ FirstName = "Mustafa",LastName = "Gökçeoğlu", Email = "mustafa.gokceoglu14@gmail.com"}
            };
            persons.ForEach(s => context.Persons.Add(s));
            context.SaveChanges();

            var restaurants = new List<Restaurant>
            {
            new Restaurant{ Name="Köfteci Yusuf", TransType=TransType.ByCar, WeatherSensitivity = WeatherSens.Susceptible },
            new Restaurant{ Name="Nusret", TransType=TransType.OnFoot, WeatherSensitivity = WeatherSens.Nonsusceptible},
            new Restaurant{ Name="Aslı Börek", TransType=TransType.ByCar, WeatherSensitivity = WeatherSens.Susceptible}
            };
            restaurants.ForEach(s => context.Restaurants.Add(s));
            context.SaveChanges();

            var points = new List<Point>
            {
            new Point{PersonID = 1, RestaurantID = 1, GivenPoint = 80 },
            new Point{PersonID = 1, RestaurantID = 2, GivenPoint = 30 },
            new Point{PersonID = 2, RestaurantID = 1, GivenPoint = 60},
            new Point{PersonID = 3, RestaurantID = 3, GivenPoint = 90}
            };
            points.ForEach(s => context.Points.Add(s));
            context.SaveChanges();

            var statistics = new List<Statistic>
            {
            new Statistic{ RestaurantID = 3, DaysToGo = 5, DaysLeft = 8 },
            new Statistic{ RestaurantID = 2, DaysToGo = 2, DaysLeft = 3  },
            new Statistic{ RestaurantID = 1, DaysToGo = 4, DaysLeft = 6 }
            };
            statistics.ForEach(s => context.Statistics.Add(s));
            context.SaveChanges();
        }
    }
}
