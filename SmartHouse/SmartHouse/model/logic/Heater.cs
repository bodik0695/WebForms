using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace SmartHouse
{
    public class Heater : Device
    {
        public Slider Temperature { get; set; }

        public Heater()
        {

        }

        public Heater(string name, bool power, Slider temperature)
        {
            Name = name;
            Power = power;

            if (temperature.CurrentValue >= temperature.MinValue && temperature.CurrentValue < temperature.MaxValue && temperature.MinValue > 0 && temperature.MaxValue > temperature.MinValue)
            {
                Temperature = temperature;
            }
        }

        public virtual void DecreaseTemperature()
        {
            Temperature.Previous();
        }

        public virtual void IncreaseTemperature()
        {
            Temperature.Next();
        }



    }
}