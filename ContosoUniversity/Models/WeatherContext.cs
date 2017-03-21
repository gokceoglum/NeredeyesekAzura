using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ContosoUniversity.Models
{
    public class WeatherContext
    {
        public Object getWeatherForcast()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?id=745042&APPID=4c4721a19c262f5dc3a45f5e7216bc74&units=metric";
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var serializer = new JavaScriptSerializer();
            var content = client.DownloadString(url);
            //var jsonContext = serializer.Deserialize<Object>(content);
            var jsonContext = JsonConvert.DeserializeObject<WeatherRootobject>(content);
            return jsonContext;
        }
    

    }
    public class WeatherRootobject
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float pressure { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public float message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

}