using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using SmartHouse.model.logic;

namespace SmartHouse.model.GraphicModel
{
    public class DisplayHeater : Panel
    {
        private int i;

        private PlaceHolder heaterPlaceHolder;
        private PlaceHolder heaterErrPlaceHolder;
        private PlaceHolder displayPlaceHolder;

        private TextBox heaterTemperatureTarget;
        private TextBox heaterTemperatureBound;

        private Button heaterDelButton;
        private Button heaterOnOffButton;
        private Button heaterApplyButton;
        private IDictionary<int, Device> deviceDictionary;

        public DisplayHeater(int i, IDictionary<int, Device> deviceDictionary, PlaceHolder displayPlaceHolder)
        {
            this.i = i;
            this.deviceDictionary = deviceDictionary;
            this.displayPlaceHolder = displayPlaceHolder;
        }

        public void Display()
        {
            Controls.Clear();
            if (Page != null)
            {
                deviceDictionary = (Dictionary<int, Device>)Page.Session["Devices"];
            }
            string tempPower;
            heaterPlaceHolder = new PlaceHolder();
            heaterPlaceHolder.ID = "heaterPlaceHolder" + i;
            heaterErrPlaceHolder = new PlaceHolder();
            heaterErrPlaceHolder.ID = "heaterErrPlaceHolder" + i;

            Controls.Add(heaterPlaceHolder);

            if (deviceDictionary[i].Power)
            {
                tempPower = "включен";
            }
            else
            {
                tempPower = "выключен";
            }

            heaterDelButton = MyButton("");
            heaterDelButton.ID = "heaterDelButton" + i;
            heaterDelButton.CssClass = "delButton";
            heaterDelButton.Click += HeaterDelButton_Click;
            heaterPlaceHolder.Controls.Add(heaterDelButton);

            heaterPlaceHolder.Controls.Add(Span("Устройство: " + deviceDictionary[i].Name + "<br />"));
            heaterPlaceHolder.Controls.Add(Span("Состояние: " + tempPower + "<br />"));

            Heater tempDevice = (Heater)deviceDictionary[i];

            SliderExtender temperatureSliderExtender = SliderExtender(tempDevice.Temperature.MinValue, tempDevice.Temperature.MaxValue);
            temperatureSliderExtender.ID = "temperatureSliderExtender" + i;
            heaterTemperatureTarget = TextBox();
            heaterTemperatureBound = TextBox();
            heaterTemperatureTarget.Text = tempDevice.Temperature.CurrentValue.ToString();
            heaterTemperatureBound.Text = tempDevice.Temperature.CurrentValue.ToString();
            Table temperatureTable = MyTable(tempDevice.Temperature.SliderName + ": ", heaterTemperatureBound, heaterTemperatureTarget);
            temperatureTable.ID = "temperatureTable" + i;
            heaterTemperatureTarget.ID = "HeaterTarget" + i;
            heaterTemperatureBound.ID = "HeaterBound" + i;
            temperatureSliderExtender.TargetControlID = heaterTemperatureTarget.ID;
            temperatureSliderExtender.BoundControlID = heaterTemperatureBound.ID;
            heaterPlaceHolder.Controls.Add(temperatureTable);
            heaterPlaceHolder.Controls.Add(temperatureSliderExtender);
            heaterPlaceHolder.Controls.Add(Span("<br />"));
            heaterApplyButton = MyButton("Применить");
            heaterApplyButton.ID = "heaterApplyButton" + i;
            heaterApplyButton.Click += HeaterApplyButton_Click;
            heaterApplyButton.CssClass = "applyButton";
            heaterPlaceHolder.Controls.Add(heaterApplyButton);
            heaterOnOffButton = MyButton("Вкл/Выкл");
            heaterOnOffButton.ID = "heaterOnOffButton" + i;
            heaterOnOffButton.Click += HeaterOnOffButton_Click;
            heaterOnOffButton.CssClass = "onOffButton";
            heaterPlaceHolder.Controls.Add(heaterOnOffButton);
            heaterPlaceHolder.Controls.Add(Span("<br />"));
            heaterPlaceHolder.Controls.Add(heaterErrPlaceHolder);
        }

        protected void HeaterApplyButton_Click(object sender, EventArgs e)
        {
            Heater tempDevice = (Heater)deviceDictionary[i];
            if (tempDevice.Power)
            {
                deviceDictionary.Remove(i);
                int value;
                bool result = Int32.TryParse(heaterTemperatureBound.Text, out value);
                tempDevice.Temperature.CurrentValue = value;
                deviceDictionary.Add(i, tempDevice);
                Page.Session["Devices"] = deviceDictionary;
                Display();
            }
            else
            {
                Display();
                heaterErrPlaceHolder.Controls.Add(Span("УСТРОЙСТВО НЕ ВКЛЮЧЕНО!"));
            }
        }

        protected void HeaterOnOffButton_Click(object sender, EventArgs e)
        {
            Heater tempDevice = (Heater)deviceDictionary[i];
            deviceDictionary.Remove(i);
            if (tempDevice.Power)
            {
                tempDevice.Power = false;
            }
            else
            {
                tempDevice.Power = true;
            }
            deviceDictionary.Add(i, tempDevice);
            Page.Session["Devices"] = deviceDictionary;
            Display();
        }

        protected void HeaterDelButton_Click(object sender, EventArgs e)
        {
            deviceDictionary.Remove(i);
            Page.Session["Devices"] = deviceDictionary;
            displayPlaceHolder.Controls.Clear();
            DisplayDeviceControl displayDeviceControl = new DisplayDeviceControl(deviceDictionary, displayPlaceHolder);
            displayPlaceHolder.Controls.Add(displayDeviceControl);
        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        protected SliderExtender SliderExtender(int minimum, int maximum)
        {
            SliderExtender sliderExtender = new SliderExtender();
            sliderExtender.Minimum = minimum;
            sliderExtender.Maximum = maximum;
            return sliderExtender;
        }

        protected TextBox TextBox()
        {
            TextBox textBox = new TextBox();
            return textBox;
        }
        protected Table MyTable(string firtCell, TextBox secondCell, TextBox thirdCell)
        {
            Table tempTable = new Table();
            TableRow tr1 = new TableRow();
            TableRow tr2 = new TableRow();
            TableRow tr3 = new TableRow();
            TableCell tc1 = new TableCell();
            TableCell tc2 = new TableCell();
            TableCell tc3 = new TableCell();
            tc1.Text = firtCell;
            tc2.Controls.Add(secondCell);
            tc3.Controls.Add(thirdCell);
            tr1.Cells.Add(tc1);
            tr2.Cells.Add(tc2);
            tr3.Cells.Add(tc3);
            tempTable.Rows.Add(tr1);
            tempTable.Rows.Add(tr2);
            tempTable.Rows.Add(tr3);
            return tempTable;
        }
        protected Button MyButton(string buttonName)
        {
            Button temp = new Button();
            temp.Text = buttonName;
            return temp;
        }

    }
}