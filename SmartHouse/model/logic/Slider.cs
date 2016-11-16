using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace SmartHouse
{
    public class Slider
    {
        public string SliderName { get; set; }
        public int CurrentValue { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public Slider()
        {

        }

        public Slider(string sliderName, int currentValue, int minValue, int maxValue)
        {
            SliderName = sliderName;
            if (currentValue >= MinValue && currentValue <= maxValue)
            {
                CurrentValue = currentValue;
            }
            if (minValue > 0)
            {
                MinValue = minValue;
            }
            if (maxValue > minValue)
            {
                MaxValue = maxValue;
            }
        }

        public virtual void Previous()
        {
            if (CurrentValue > MinValue && CurrentValue <= MaxValue)
            {
                CurrentValue--;
            }
        }

        public virtual void Next()
        {
            if (CurrentValue >= MinValue && CurrentValue < MaxValue)
            {
                CurrentValue++;
            }
        }
    }
}