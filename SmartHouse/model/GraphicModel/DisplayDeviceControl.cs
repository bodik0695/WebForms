using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SmartHouse.model.GraphicModel
{
    public class DisplayDeviceControl : Panel
    {
        public IDictionary<int, Device> deviceDictionary { get; set; }
        private PlaceHolder displayPlaceHolder;

        public DisplayDeviceControl(IDictionary<int, Device> deviceDictionary, PlaceHolder displayPlaceHolder)
        {
            this.deviceDictionary = deviceDictionary;
            this.displayPlaceHolder = displayPlaceHolder;
            Initializer();
        }

        public void Initializer()
        {
            Controls.Clear();
            if (deviceDictionary.Keys.Count != 0)
            {
                for (int i = 1; i <= deviceDictionary.Keys.Max(); i++)
                {
                    if (deviceDictionary.ContainsKey(i))
                    {
                        if (deviceDictionary[i] is UserDevice)
                        {
                            DisplayUserDevice DisplayUserDevice = new DisplayUserDevice(i, deviceDictionary, displayPlaceHolder); //this.DisplayDeviceControl);
                            DisplayUserDevice.Display();
                            DisplayUserDevice.CssClass = "device userDevice";
                            Controls.Add(DisplayUserDevice);
                        }

                        if (deviceDictionary[i] is TV)
                        {
                            DisplayTV DisplayTV = new DisplayTV(i, deviceDictionary, displayPlaceHolder);
                            DisplayTV.Display();
                            DisplayTV.CssClass = "device tv";
                            Controls.Add(DisplayTV);
                        }

                        if (deviceDictionary[i] is Heater)
                        {
                            DisplayHeater DisplayHeater = new DisplayHeater(i, deviceDictionary, displayPlaceHolder);
                            DisplayHeater.Display();
                            DisplayHeater.CssClass = "device heater";
                            Controls.Add(DisplayHeater);
                        }

                        if (deviceDictionary[i] is Conditioner)
                        {
                            DisplayConditioner DisplayConditioner = new DisplayConditioner(i, deviceDictionary, displayPlaceHolder);
                            DisplayConditioner.Display();
                            DisplayConditioner.CssClass = "device conditioner";
                            Controls.Add(DisplayConditioner);
                        }
                    }
                }
            }
        }
    }
}