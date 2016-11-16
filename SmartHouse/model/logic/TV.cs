using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace SmartHouse
{
    public class TV : Device
    {
        public Slider Channel { get; set; }
        public Slider Sound { set; get; }

        public TV()
        {

        }

        public TV(string deviceName, bool power, int currentChannel, int minChannelNumber, int maxChannelNumber, int currentVolume, int minVolume, int maxVolume)
        {
            Name = deviceName;
            Power = power;

            if (currentChannel > minChannelNumber && currentChannel < maxChannelNumber)
            {
                Channel.CurrentValue = currentChannel;
            }
            if (minChannelNumber > 0)
            {
                Channel.MinValue = minChannelNumber;
            }
            if (maxChannelNumber > minChannelNumber)
            {
                Channel.MaxValue = maxChannelNumber;
            }

            if (currentVolume > minVolume && currentVolume < maxVolume)
            {
                Sound.CurrentValue = currentVolume;
            }
            if (minVolume > 0)
            {
                Sound.MinValue = minVolume;
            }
            if (maxVolume > minVolume)
            {
                Sound.MaxValue = maxVolume;
            }
        }

        public virtual void NextChannel()
        {
            Channel.Next();
        }

        public virtual void PreviousChannel()
        {
            Channel.Previous();
        }

        public virtual void IncreaseVolume()
        {
            Sound.Next();
        }

        public virtual void DecreaseVolume()
        {
            Sound.Previous();
        }
    }
}