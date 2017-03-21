using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public  JsonResult GetWeather()
        {
            WeatherContext weath = new WeatherContext();
            return Json(weath.getWeatherForcast(), JsonRequestBehavior.AllowGet);
        }
        public static bool isWeatherFine(WeatherRootobject weather)
        {
            string description = weather.weather[0].main;
            if (description.Equals("Thunderstorm"))
            {
                return false;
            }
            else if (description.Equals("Drizzle")){
                return false;
            }
            else if (description.Equals("Rain")){
                return false;
            }
            else if (description.Equals("Snow")){
                return false;
            }
            else if (description.Equals("Atmosphere")){
                return false;
            }
            else if (description.Equals("Extreme")){
                return false;
            }
            else if (description.Equals("Additional")){
                return false;
            }
            else
                return true;

        }
    }
}