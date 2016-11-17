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
    public class DisplayTV : Panel
    {
        public int i;

        private PlaceHolder tvPlaceHolder;
        private PlaceHolder tVErrPlaceHolder;
        private PlaceHolder displayPlaceHolder;

        private TextBox channelTarget;
        private TextBox channelBound;

        private TextBox soundTarget;
        private TextBox soundBound;

        private Button tvDelButton;
        private Button tVOnOffButton;
        private Button tVApplyButton;

        private IDictionary<int, Device> deviceDictionary;

        public DisplayTV(int i, IDictionary<int, Device> deviceDictionary, PlaceHolder displayPlaceHolder)
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
            tvPlaceHolder = new PlaceHolder();
            tvPlaceHolder.ID = "tvPlaceHolder" + i;
            Controls.Add(tvPlaceHolder);
            tVErrPlaceHolder = new PlaceHolder();
            tVErrPlaceHolder.ID = "tVErrPlaceHolder" + i;
            
           
            if (deviceDictionary[i].Power)
            {
                tempPower = "включен";
            }
            else
            {
                tempPower = "выключен";
            }

            tvDelButton = MyButton("");
            tvDelButton.ID = "tVDelButton" + i;
            tvDelButton.CssClass = "delButton";
            tvDelButton.Click += TVDelButton_Click;
            tvPlaceHolder.Controls.Add(tvDelButton);
            tvPlaceHolder.Controls.Add(Span("Устройство: " + deviceDictionary[i].Name + "<br />"));
            tvPlaceHolder.Controls.Add(Span("Состояние: " + tempPower + "<br />"));

            TV tempDevice = (TV)deviceDictionary[i];

            SliderExtender soundSliderExtender = SliderExtender(tempDevice.Sound.MinValue, tempDevice.Sound.MaxValue);
            soundSliderExtender.ID = "soundSliderExtender" + i;
            soundTarget = TextBox();
            soundBound = TextBox();
            soundTarget.ID = "TvSoundTarget" + i;
            soundBound.ID = "TvSoundBound" + i;
            soundTarget.Text = tempDevice.Sound.CurrentValue.ToString();
            soundBound.Text = tempDevice.Sound.CurrentValue.ToString();
            
            Table soundTable = MyTable(tempDevice.Sound.SliderName + ": ", soundBound, soundTarget);
            soundTable.ID = "soundTable" + i;
            soundSliderExtender.TargetControlID = soundTarget.ID;
            soundSliderExtender.BoundControlID = soundBound.ID;
            tvPlaceHolder.Controls.Add(soundTable);
            tvPlaceHolder.Controls.Add(soundSliderExtender);
            tvPlaceHolder.Controls.Add(Span("<br />"));

            SliderExtender channelSliderExtender = SliderExtender(tempDevice.Channel.MinValue, tempDevice.Channel.MaxValue);
            channelSliderExtender.ID = "channelSliderExtender" + i;
            channelTarget = TextBox();
            channelBound = TextBox();
            channelTarget.Text = tempDevice.Channel.CurrentValue.ToString();
            channelBound.Text = tempDevice.Channel.CurrentValue.ToString();
            channelTarget.ID = "TvChannelTarget" + i;
            channelBound.ID = "TvChannelBound" + i;
            Table channelTable = MyTable(tempDevice.Channel.SliderName + ": ", channelBound, channelTarget);
            channelTable.ID = "channelTable" + i;
            channelSliderExtender.TargetControlID = channelTarget.ID;
            channelSliderExtender.BoundControlID = channelBound.ID;
            tvPlaceHolder.Controls.Add(channelTable);
            tvPlaceHolder.Controls.Add(channelSliderExtender);
            tvPlaceHolder.Controls.Add(Span("<br />"));

            tVApplyButton = MyButton("Применить");
            tVApplyButton.ID = "applyTVButton" + i;
            tVApplyButton.Click += TVApplyButton_Click;
            tVApplyButton.CssClass = "applyButton";
            tvPlaceHolder.Controls.Add(tVApplyButton);
            tVOnOffButton = MyButton("Вкл/Выкл");
            tVOnOffButton.ID = "tVOnOffButton" + i;
            tVOnOffButton.Click += TVOnOffButton_Click;
            tVOnOffButton.CssClass = "onOffButton";
            tvPlaceHolder.Controls.Add(tVOnOffButton);
            tvPlaceHolder.Controls.Add(Span("<br />"));
            tvPlaceHolder.Controls.Add(tVErrPlaceHolder);
        }

        protected void TVApplyButton_Click(object sender, EventArgs e)
        {
            TV tempDevice = (TV)deviceDictionary[i];
            if (tempDevice.Power)
            {
                int value1;
                bool result1 = Int32.TryParse(channelBound.Text, out value1);
                tempDevice.Channel.CurrentValue = value1;
                int value2;
                bool result2 = Int32.TryParse(soundBound.Text, out value2);
                tempDevice.Sound.CurrentValue = value2;
                deviceDictionary.Remove(i);
                deviceDictionary.Add(i, tempDevice);
                Page.Session["Devices"] = deviceDictionary;
                Display();
            }
            else
            {
                Display();
                tVErrPlaceHolder.Controls.Add(Span("УСТРОЙСТВО НЕ ВКЛЮЧЕНО!"));
            }
            
        }

        protected void TVOnOffButton_Click(object sender, EventArgs e)
        {
            TV tempDevice = (TV)deviceDictionary[i];
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

        protected void TVDelButton_Click(object sender, EventArgs e)
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