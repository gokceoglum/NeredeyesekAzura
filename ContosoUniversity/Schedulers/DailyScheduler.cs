using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Schedulers
{
    public class DailyScheduler : IJob
    {
        private ProjectContext db = new ProjectContext();
        public void Execute(IJobExecutionContext context)
        {
            WeatherContext weath = new WeatherContext();
            
            //true: yürümeye uygun
            bool weather = HomeController.isWeatherFine((WeatherRootobject)weath.getWeatherForcast()); ;
            int counter = 0;
            Restaurant recommendedrestaurant = new Restaurant();
            Restaurant toGoRestaurant = new Restaurant();
            List<Statistic> statisticsList = db.Statistics.OrderByDescending(p => p.DaysLeft).ToList();
            db.SaveChanges();
            foreach (var i in statisticsList)
            {
                var restaurant = db.Restaurants.Find(i.RestaurantID);
                if (counter == 0)
                {
                    recommendedrestaurant = restaurant;
                }
                counter++;
                if (LastRestaurants.lastId == i.RestaurantID)
                {
                    continue;
                }
                if (weather == false && restaurant.WeatherSensitivity == WeatherSens.Susceptible && restaurant.TransType == TransType.OnFoot)
                {
                    continue;
                }
                var lastRestaurant = db.Restaurants.Find(LastRestaurants.lastId);
                var last2Restaurant = db.Restaurants.Find(LastRestaurants.last2Id);
                if (last2Restaurant == null && lastRestaurant != null)
                {
                    if (lastRestaurant.TransType == TransType.OnFoot && restaurant.TransType == TransType.ByCar)
                    {
                        continue;
                    }
                }
                if (last2Restaurant != null && lastRestaurant != null)
                {
                    if ((lastRestaurant.TransType == TransType.OnFoot || last2Restaurant.TransType == TransType.OnFoot) && restaurant.TransType == TransType.OnFoot)
                    {
                        continue;
                    }
                }
                if(db.Statistics.Single(x => x.RestaurantID == restaurant.ID).DaysLeft == 0)
                {
                    continue;
                }
                toGoRestaurant = restaurant;
                break;
            }
            db.SaveChanges();
            if (toGoRestaurant.Name == null)
            {
                toGoRestaurant = recommendedrestaurant;
            }
            LastRestaurants.last2Id = LastRestaurants.lastId;
            LastRestaurants.lastId = toGoRestaurant.ID;

            Mail mail = new Mail();
            foreach (var person in db.Persons)
            {
                //  mail.MailSender(recommendedRestaurant, person.Email);
            }
            updateTable(toGoRestaurant);
        }

        public void updateTable(Restaurant restaurant)
        {
            db.Statistics.Single(x => x.RestaurantID == restaurant.ID).DaysLeft -= 1;
            db.SaveChanges();

        }

    }
}