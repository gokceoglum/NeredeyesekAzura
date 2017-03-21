using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models

{
    public enum TransType
    {
        OnFoot=1,
        ByCar=2
    }
    public enum WeatherSens
    {
        Susceptible=1,
        Nonsusceptible
    }
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TransType TransType { get; set; }
        public WeatherSens WeatherSensitivity { get; set; }


    }
}