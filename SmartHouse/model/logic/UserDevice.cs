using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace SmartHouse
{
    public class UserDevice : Device
    {
        public Dictionary<int, Slider> Sliders { set; get; }

        public UserDevice()
        {
            
        }

        public UserDevice(string deviceName, bool power, Dictionary<int, Slider> sliders)
        {
            Name = deviceName;
            Power = power;
            Sliders = sliders;
        }

        public void AddSlider(int id, Slider newSlider)
        {
            Sliders.Add(id, newSlider);
        }

        public void DeleteSlider(string sliderName)
        {
            foreach (var x in Sliders)
            {
                if (Sliders[x.Key].SliderName == sliderName)
                {
                    Sliders.Remove(x.Key);
                }
            }
        }

        public void ChangeSlider(string sliderName, int newMinValue, int newMaxValue)
        {
            foreach (var x in Sliders)
            {
                if (Sliders[x.Key].SliderName == sliderName)
                {
                    x.Value.MinValue = newMinValue;
                    x.Value.MaxValue = newMaxValue;
                }
            }
        }
    }
}