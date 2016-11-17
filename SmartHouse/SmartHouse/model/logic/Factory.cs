using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.model.logic
{
    public class Factory
    {
        public Factory()
        {

        }

        public Dictionary<int, Slider> CreateDictionary(Slider[] sliders)
        {
            Dictionary<int, Slider> Sliders = new Dictionary<int, Slider>();
            if (sliders.Length != 0)
            {
                for (int i = 0; i < sliders.Length - 1; i++)
                {
                    Sliders.Add(i + 1, sliders[i]);
                }
            }
            return Sliders;
        }

        public Slider CreateSlider(string sliderName, int currentValue, int minValue, int maxValue)
        {
            Slider tempSlider = new Slider(sliderName, currentValue, minValue, maxValue);
            return tempSlider;
        }

        public UserDevice CreateDevice(string deviceName, bool power)
        {
            Dictionary<int, Slider> Sliders = new Dictionary<int, Slider>();
            UserDevice tempUserDevice = new UserDevice(deviceName, power, Sliders);
            return tempUserDevice;
        }

        public UserDevice CreateDevice(string deviceName, bool power, int sliderId, Slider slider)
        {
            Dictionary<int, Slider> Sliders = new Dictionary<int, Slider>();
            Sliders.Add(sliderId, slider);
            UserDevice tempUserDevice = new UserDevice(deviceName, power, Sliders);
            return tempUserDevice;
        }

        public UserDevice CreateDevice(string deviceName, bool power, Dictionary<int, Slider> Sliders)
        {
            UserDevice tempUserDevice = new UserDevice(deviceName, power, Sliders);
            return tempUserDevice;
        }

        public Conditioner CreateConditioner(string deviceName, bool power, Slider slider)
        {
            Conditioner tempConditioner = new Conditioner(deviceName, power, slider);
            return tempConditioner;

        }

        public Heater CreateHeater(string deviceName, bool power, Slider slider)
        {
            Heater tempConditioner = new Heater(deviceName, power, slider);
            return tempConditioner;

        }

        public TV CreateTV(string deviceName, bool power, int currentChannel, int minChannelNumber, int maxChannelNumber, int currentVolume, int minVolume, int maxVolume)
        {
            TV tempTV = new TV(deviceName, power, currentChannel, minChannelNumber, maxChannelNumber, currentVolume, minVolume, maxVolume);
            return tempTV;
        }
    }
}