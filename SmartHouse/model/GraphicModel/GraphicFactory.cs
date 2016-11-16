using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.model.CutomModel
{
    public class GraphicFactory
    {

       public GraphicFactory()
        {

        }

        public UserDevice CreateEmptyUserDevice()
        {
            UserDevice EmptyUserDevice = new UserDevice();
            return EmptyUserDevice;
        }
        public TV CreateEmptyTV()
        {
            TV EmptyTV = new TV();
            return EmptyTV;
        }
        public Heater CreateEmptyHeater()
        {
            Heater EmptyHeater = new Heater();
            return EmptyHeater;
        }
        public Conditioner CreateEmptyConditioner()
        {
            Conditioner EmptyConditioner = new Conditioner();
            return EmptyConditioner;
        }
        public Slider CreateEmptySlider()
        {
            Slider EmptySlider = new Slider();
            return EmptySlider;
        }
    }
}