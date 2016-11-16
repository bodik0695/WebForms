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
    public class DisplayConditioner : Panel
    {
        public int i;

        private PlaceHolder conditionerPlaceHolder;
        private PlaceHolder conditionerErrPlaceHolder;
        private PlaceHolder displayPlaceHolder;

        private TextBox conditionerTemperatureTarget;
        private TextBox conditionerTemperatureBound;

        private Button conditionerDelButton;
        private Button conditionerOnOffButton;
        private Button conditionerApplyButton;

        private IDictionary<int, Device> deviceDictionary;

        public DisplayConditioner(int i, IDictionary<int, Device> deviceDictionary, PlaceHolder displayPlaceHolder)
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
                deviceDictionary = (IDictionary<int, Device>)Page.Session["Devices"];
            }
            string tempPower;
            conditionerPlaceHolder = new PlaceHolder();
            conditionerPlaceHolder.ID = "conditionerPlaceHolder" + i;
            conditionerErrPlaceHolder = new PlaceHolder();
            conditionerErrPlaceHolder.ID = "conditionerErrPlaceHolder" + i;
            Controls.Add(conditionerPlaceHolder);

            if (deviceDictionary[i].Power)
            {
                tempPower = "включен";
            }
            else
            {
                tempPower = "выключен";
            }

            conditionerDelButton = MyButton("");
            conditionerDelButton.ID = "heaterDelButton" + i;
            conditionerDelButton.CssClass = "delButton";
            conditionerDelButton.Click += ConditionerDelButton_Click;
            conditionerPlaceHolder.Controls.Add(conditionerDelButton);

            conditionerPlaceHolder.Controls.Add(Span("Устройство: " + deviceDictionary[i].Name + "<br />"));
            conditionerPlaceHolder.Controls.Add(Span("Состояние: " + tempPower + "<br />"));

            Conditioner tempDevice = (Conditioner)deviceDictionary[i];

            SliderExtender temperatureSliderExtender = SliderExtender(tempDevice.Temperature.MinValue, tempDevice.Temperature.MaxValue);
            temperatureSliderExtender.ID = "temperatureSliderExtender" + i;
            conditionerTemperatureTarget = TextBox();
            conditionerTemperatureBound = TextBox();
            conditionerTemperatureTarget.Text = tempDevice.Temperature.CurrentValue.ToString();
            conditionerTemperatureBound.Text = tempDevice.Temperature.CurrentValue.ToString();
            Table temperatureTable = MyTable(tempDevice.Temperature.SliderName + ": ", conditionerTemperatureBound, conditionerTemperatureTarget);
            temperatureTable.ID = "temperatureTable" + i;
            conditionerTemperatureTarget.ID = "ConditionerTarget" + i;
            conditionerTemperatureBound.ID = "ConditionerBound" + i;
            temperatureSliderExtender.TargetControlID = conditionerTemperatureTarget.ID;
            temperatureSliderExtender.BoundControlID = conditionerTemperatureBound.ID;
            conditionerPlaceHolder.Controls.Add(temperatureTable);
            conditionerPlaceHolder.Controls.Add(temperatureSliderExtender);
            conditionerPlaceHolder.Controls.Add(Span("<br />"));
            conditionerApplyButton = MyButton("Применить");
            conditionerApplyButton.ID = "conditionerApplyButton" + i;
            conditionerApplyButton.Click += ConditionrApplyButton_Click;
            conditionerApplyButton.CssClass = "applyButton";
            conditionerPlaceHolder.Controls.Add(conditionerApplyButton);
            conditionerOnOffButton = MyButton("Вкл/Выкл");
            conditionerOnOffButton.ID = "conditionerOnOffButton" + i;
            conditionerOnOffButton.Click += ConditionerOnOffButton_Click;
            conditionerOnOffButton.CssClass = "onOffButton";
            conditionerPlaceHolder.Controls.Add(conditionerOnOffButton);
            conditionerPlaceHolder.Controls.Add(Span("<br />"));
            conditionerPlaceHolder.Controls.Add(conditionerErrPlaceHolder);
        }

        protected void ConditionrApplyButton_Click(object sender, EventArgs e)
        {
            Conditioner tempDevice = (Conditioner)deviceDictionary[i];
            if (tempDevice.Power)
            {
                deviceDictionary.Remove(i);
                int value;
                bool result = Int32.TryParse(conditionerTemperatureBound.Text, out value);
                tempDevice.Temperature.CurrentValue = value;
                deviceDictionary.Add(i, tempDevice);
                Page.Session["Devices"] = deviceDictionary;
                Display();
            }
            else
            {
                Display();
                conditionerErrPlaceHolder.Controls.Add(Span("УСТРОЙСТВО НЕ ВКЛЮЧЕНО!"));
            }
        }

        protected void ConditionerOnOffButton_Click(object sender, EventArgs e)
        {
            Conditioner tempDevice = (Conditioner)deviceDictionary[i];
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

        protected void ConditionerDelButton_Click(object sender, EventArgs e)
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