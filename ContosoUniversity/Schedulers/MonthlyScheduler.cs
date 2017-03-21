using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using Quartz;
using System;
using System.Linq;

namespace ContosoUniversity.Schedulers
{
    public class MonthlyScheduler : IJob
    {
        private ProjectContext db = new ProjectContext();
        public void Execute(IJobExecutionContext context)
        {
            LastRestaurants.lastId = 0;
            LastRestaurants.last2Id = 0;
            foreach (var p in db.Statistics)
            {
                db.Statistics.Remove(p);
            }
            db.SaveChanges();
            int totalPoint = 0;
            foreach (var restaurant in db.Restaurants)
            {
                var drafts = db.Points.Where(d => d.RestaurantID == restaurant.ID).ToList();
                int point = 0;
                foreach (var resPoint in drafts)
                {
                    point += resPoint.GivenPoint; //Restoranın toplam puanı
                }
                totalPoint += point;
                Statistic statistic = new Statistic();
                statistic.RestaurantID = restaurant.ID;
                statistic.DaysLeft = point;
                statistic.DaysToGo = point;
                db.Statistics.Add(statistic);
            }
            db.SaveChanges();
            if (totalPoint == 0) return;
            foreach (var s in db.Statistics)
            {
                int exactDay = s.DaysToGo * 20 / totalPoint;
                s.DaysToGo = exactDay;
                s.DaysLeft = exactDay;
            }
            db.SaveChanges();
            int remaining_days = 20 - db.Statistics.Sum(d => d.DaysToGo); //Kalan günler ilk restorana atılır
            if (remaining_days != 0)
            {
                var stat = db.Statistics.ToArray();
                stat[0].DaysToGo += remaining_days;
                stat[0].DaysLeft += remaining_days;
            }
            db.SaveChanges();
        }
    }
}
