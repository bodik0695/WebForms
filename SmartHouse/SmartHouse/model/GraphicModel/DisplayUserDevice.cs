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
    public class DisplayUserDevice : Panel
    {
        public int i;

        private PlaceHolder userDevicePlaceHolder;
        private PlaceHolder userDeviceErrPlaceHolder;
        private PlaceHolder displayPlaceHolder;

        private TextBox userDeviceTarget;
        private TextBox userDeviceBound;

        private Button userDeviceDelButton;
        private Button userDeviceOnOffButton;
        private Button userDeviceApplyButton;

        private IDictionary<int, Device> deviceDictionary;
        private List<TextBox> userDeviceBounds;

        public DisplayUserDevice(int i, IDictionary<int, Device> deviceDictionary, PlaceHolder displayPlaceHolder)
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
            userDevicePlaceHolder = new PlaceHolder();
            userDevicePlaceHolder.ID = "userDevicePlaceHolder" + i;
            userDeviceErrPlaceHolder = new PlaceHolder();
            userDeviceErrPlaceHolder.ID = "userDeviceErrPlaceHolder" + i;
            Controls.Add(userDevicePlaceHolder);

            if (deviceDictionary[i].Power)
            {
                tempPower = "включен";
            }
            else
            {
                tempPower = "выключен";
            }

            userDeviceDelButton = MyButton("");
            userDeviceDelButton.ID = "userDeviceDelButton" + i;
            userDeviceDelButton.CssClass = "delButton";
            userDeviceDelButton.Click += UserDeviceDelButton_Click;
            userDevicePlaceHolder.Controls.Add(userDeviceDelButton);
            userDevicePlaceHolder.Controls.Add(Span("Устройство: " + deviceDictionary[i].Name + "<br />"));
            userDevicePlaceHolder.Controls.Add(Span("Состояние: " + tempPower + "<br />"));

            UserDevice tempDevice = (UserDevice)deviceDictionary[i];
            userDeviceBounds = new List<TextBox>();
            int number = i + 10;
            foreach (var x in tempDevice.Sliders)
            {
                SliderExtender userDeviceSliderExtender = SliderExtender(x.Value.MinValue, x.Value.MaxValue);
                userDeviceTarget = TextBox();
                userDeviceBound = TextBox();
                userDeviceTarget.Text = x.Value.CurrentValue.ToString();
                userDeviceBound.Text = x.Value.CurrentValue.ToString();
                userDeviceBounds.Add(userDeviceBound);
                Table userDeviceTable = MyTable(x.Value.SliderName + ": ", userDeviceBound, userDeviceTarget);
                userDeviceTable.ID = "userDeviceTable" + number;
                userDeviceTarget.ID = "Target" + number;
                userDeviceBound.ID = "Bound" + number;
                userDeviceSliderExtender.TargetControlID = userDeviceTarget.ID;
                userDeviceSliderExtender.BoundControlID = userDeviceBound.ID;
                userDevicePlaceHolder.Controls.Add(userDeviceTable);
                userDevicePlaceHolder.Controls.Add(userDeviceSliderExtender);
                userDevicePlaceHolder.Controls.Add(Span("<br />"));
                number++;
            }
            if (tempDevice.Sliders.Count != 0)
            {
                userDeviceApplyButton = MyButton("Применить");
                userDeviceApplyButton.ID = "applyButton" + i;
                userDeviceApplyButton.Click += UserDeviceApplyButton_Click;
                userDeviceApplyButton.CssClass = "applyButton";
                userDevicePlaceHolder.Controls.Add(userDeviceApplyButton);
            }
            userDeviceOnOffButton = MyButton("Вкл/Выкл");
            userDeviceOnOffButton.ID = "userDeviceOnOffButton" + i;
            userDeviceOnOffButton.Click += UserDeviceOnOffButton_Click;
            userDeviceOnOffButton.CssClass = "onOffButton";
            userDevicePlaceHolder.Controls.Add(userDeviceOnOffButton);
            userDevicePlaceHolder.Controls.Add(Span("<br />"));
            userDevicePlaceHolder.Controls.Add(userDeviceErrPlaceHolder);
        }

        protected void UserDeviceApplyButton_Click(object sender, EventArgs e)
        {
            UserDevice tempDevice = (UserDevice)deviceDictionary[i];
            if (tempDevice.Power)
            {
                deviceDictionary.Remove(i);
                for (int i = 1; i <= userDeviceBounds.Count; i++)
                {
                    int value;
                    bool result = Int32.TryParse(userDeviceBounds[i - 1].Text, out value);
                    tempDevice.Sliders[i].CurrentValue = value;
                }
                deviceDictionary.Add(i, tempDevice);
                Page.Session["Devices"] = deviceDictionary;
                userDevicePlaceHolder.Controls.Clear();
                Display();
            }
            else
            {
                Display();
                userDeviceErrPlaceHolder.Controls.Add(Span("УСТРОЙСТВО НЕ ВКЛЮЧЕНО!"));
            }
        }

        protected void UserDeviceOnOffButton_Click(object sender, EventArgs e)
        {
            UserDevice tempDevice = (UserDevice)deviceDictionary[i];
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

        protected void UserDeviceDelButton_Click(object sender, EventArgs e)
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