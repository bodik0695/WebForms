using SmartHouse.model.CutomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SmartHouse.model.GraphicModel
{
    public class GeneralControl : Panel
    {
        private GraphicFactory graphicFactory;
        private DropDownList dropDownList;

        private Button selectButton;
        private Button addElementButton;
        private Button removeElementButton;
        private Button addDeviceButton;
        private Button addTvButton;
        private Button addHeaterButton;
        private Button addConditionerButton;

        private PlaceHolder firstPlaceHolder;
        private PlaceHolder secondPlaceHolder;
        private PlaceHolder thirdPlaceHolder;
        private PlaceHolder displayPlaceHolder;

        private TextBox nameTextBox;
        private TextBox sliderNameTextBox;
        private TextBox minValueTextBox;
        private TextBox maxValueTextBox;
        private TextBox minVolumeValueTextBox;
        private TextBox maxVolumeValueTextBox;
        private TextBox minChannelValueTextBox;
        private TextBox maxChannelValueTextBox;
        private TextBox minTemperatureValueTextBox;
        private TextBox maxTemperatureValueTextBox;

        private UserDevice userDevice;
        private TV tV;
        private Heater heater;
        private Conditioner conditioner;

        public DisplayDeviceControl DisplayDeviceControl { set; get; }

        public IList<TextBox> SlidersNamesTextBoxes { set; get; }
        public IList<TextBox> MinValuesTextBoxes { set; get; }
        public IList<TextBox> MaxValuesTextBoxes { set; get; }

        public GeneralControl(DisplayDeviceControl displayDeviceControl, PlaceHolder displayPlaceHolder)
        {
            DisplayDeviceControl = displayDeviceControl;
            this.displayPlaceHolder = displayPlaceHolder;
        }

        public void Initializer()
        {
            Controls.Clear();
            firstPlaceHolder = new PlaceHolder();
            firstPlaceHolder.ID = "FirstPlaceHolder";
            Controls.Add(firstPlaceHolder);
            graphicFactory = new GraphicFactory();
            dropDownList = new DropDownList();
            dropDownList.ID = "dropDownList";
            ListItem li0 = new ListItem();
            ListItem li1 = new ListItem();
            ListItem li2 = new ListItem();
            ListItem li3 = new ListItem();
            ListItem li4 = new ListItem();
            li0.Text = "";
            li1.Text = "Свое Устройство";
            li2.Text = "Телевизор";
            li3.Text = "Обогреватель";
            li4.Text = "Кондиционер";
            dropDownList.Items.Add(li0);
            dropDownList.Items.Add(li1);
            dropDownList.Items.Add(li2);
            dropDownList.Items.Add(li3);
            dropDownList.Items.Add(li4);
            dropDownList.CssClass = "dropDownList";
            firstPlaceHolder.Controls.Add(dropDownList);
            selectButton = MyButton("Выбрать");
            selectButton.ID = "selectButton" + 1;
            selectButton.Click += SelectButton_Click;
            selectButton.CssClass = "templateSelectButton";
            secondPlaceHolder = new PlaceHolder();
            secondPlaceHolder.ID = "SecondPlaceHolder";
            thirdPlaceHolder = new PlaceHolder();
            thirdPlaceHolder.ID = "ThirdPlaceHolder";
            secondPlaceHolder.Controls.Add(thirdPlaceHolder);
            firstPlaceHolder.Controls.Add(selectButton);
            firstPlaceHolder.Controls.Add(Span("<br />"));
            firstPlaceHolder.Controls.Add(secondPlaceHolder);
            firstPlaceHolder.Controls.Add(Span("<br />"));
            SlidersNamesTextBoxes = new List<TextBox>();
            MinValuesTextBoxes = new List<TextBox>();
            MaxValuesTextBoxes = new List<TextBox>();
            DrawDevice();
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            Page.Session["EmptyUserDevice"] = null;
            Page.Session["EmptyTV"] = null;
            Page.Session["EmptyHeater"] = null;
            Page.Session["EmptyConditioner"] = null;
            secondPlaceHolder.Controls.Clear();
            if (dropDownList.SelectedIndex == 0)
            {

            }

            if (dropDownList.SelectedIndex == 1)
            {
                userDevice = graphicFactory.CreateEmptyUserDevice();
                userDevice.Sliders = new Dictionary<int, Slider>();
                Page.Session["EmptyUserDevice"] = userDevice;
            }

            if (dropDownList.SelectedIndex == 2)
            {
                tV = graphicFactory.CreateEmptyTV();
                Page.Session["EmptyTV"] = tV;
            }

            if (dropDownList.SelectedIndex == 3)
            {
                heater = graphicFactory.CreateEmptyHeater();
                Page.Session["EmptyHeater"] = heater;
            }

            if (dropDownList.SelectedIndex == 4)
            {
                conditioner = graphicFactory.CreateEmptyConditioner();
                Page.Session["EmptyConditioner"] = conditioner;
            }
            DrawDevice();
        }

        protected void DrawDevice()
        {
            if (Page != null)
            {
                secondPlaceHolder.Controls.Clear();
                thirdPlaceHolder.Controls.Clear();
                SlidersNamesTextBoxes.Clear();
                MinValuesTextBoxes.Clear();
                MaxValuesTextBoxes.Clear();
                if ((UserDevice)Page.Session["EmptyUserDevice"] != null)
                {
                    secondPlaceHolder.Controls.Add(Span("Название устройства:"));
                    nameTextBox = TextBox();
                    nameTextBox.ID = "UserDeviceNameTextBox";
                    secondPlaceHolder.Controls.Add(nameTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    addElementButton = MyButton("Добавить элемент управления");
                    addElementButton.ID = "addElementButton";
                    addElementButton.Click += AddElementButton_Click;
                    addElementButton.CssClass = "addElementButton";
                    secondPlaceHolder.Controls.Add(addElementButton);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    removeElementButton = MyButton("Удалить элемент управления");
                    removeElementButton.ID = "RemoveElementButton";
                    removeElementButton.Click += RemoveElementButton_Click;
                    removeElementButton.CssClass = "removeElementButton";
                    secondPlaceHolder.Controls.Add(removeElementButton);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    thirdPlaceHolder.ID = "FormControlPlaceHolder" + 1;
                    userDevice = (UserDevice)Page.Session["EmptyUserDevice"];
                    if (userDevice.Sliders != null)
                    {
                        for (int i = 0; i < userDevice.Sliders.Count; i++)
                        {
                            thirdPlaceHolder.Controls.Add(Span("Название элемента: "));
                            sliderNameTextBox = TextBox();
                            SlidersNamesTextBoxes.Add(sliderNameTextBox);
                            sliderNameTextBox.ID = "sliderNameTextBox" + i;
                            thirdPlaceHolder.Controls.Add(sliderNameTextBox);
                            thirdPlaceHolder.Controls.Add(Span("<br />"));
                            thirdPlaceHolder.Controls.Add(Span("Минимальное значение: "));
                            minValueTextBox = TextBox();
                            MinValuesTextBoxes.Add(minValueTextBox);
                            minValueTextBox.ID = "minValueTextBox" + i;
                            thirdPlaceHolder.Controls.Add(minValueTextBox);
                            thirdPlaceHolder.Controls.Add(Span("<br />"));
                            thirdPlaceHolder.Controls.Add(Span("Максимальное значение: "));
                            maxValueTextBox = TextBox();
                            MaxValuesTextBoxes.Add(maxValueTextBox);
                            maxValueTextBox.ID = "maxValueTextBox" + i;
                            thirdPlaceHolder.Controls.Add(maxValueTextBox);
                            thirdPlaceHolder.Controls.Add(Span("<br />"));
                        }
                    }

                    secondPlaceHolder.Controls.Add(thirdPlaceHolder);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    addDeviceButton = MyButton("Добавить");
                    addDeviceButton.ID = "AddDeviceButton";
                    addDeviceButton.Click += AddDeviceButton_Click;
                    addDeviceButton.CssClass = "templateAddDeviceButton";
                    secondPlaceHolder.Controls.Add(addDeviceButton);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                }
                if ((TV)Page.Session["EmptyTV"] != null)
                {
                    secondPlaceHolder.Controls.Add(Span("Название устройства:"));
                    nameTextBox = TextBox();
                    nameTextBox.ID = "TvNameTextBox";
                    secondPlaceHolder.Controls.Add(nameTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Громкость: "));
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Минимальное значение громкости: "));
                    minVolumeValueTextBox = TextBox();
                    minVolumeValueTextBox.ID = "TvMinVolumeValueTextBox";
                    secondPlaceHolder.Controls.Add(minVolumeValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Максимальное значение громкости: "));
                    maxVolumeValueTextBox = TextBox();
                    maxVolumeValueTextBox.ID = "TvMaxVolumeValueTextBox";
                    secondPlaceHolder.Controls.Add(maxVolumeValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Каналы: "));
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Минимально возможный канал: "));
                    minChannelValueTextBox = TextBox();
                    minChannelValueTextBox.ID = "TvMinChannelValueTextBox";
                    secondPlaceHolder.Controls.Add(minChannelValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Максимально возможный канал: "));
                    maxChannelValueTextBox = TextBox();
                    maxChannelValueTextBox.ID = "TvMaxChannelValueTextBox";
                    secondPlaceHolder.Controls.Add(maxChannelValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    addTvButton = MyButton("Добавить");
                    addTvButton.ID = "AddTvButton";
                    addTvButton.Click += AddTvButton_Click;
                    addTvButton.CssClass = "templateAddDeviceButton";
                    secondPlaceHolder.Controls.Add(addTvButton);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                }
                if ((Heater)Page.Session["EmptyHeater"] != null)
                {
                    secondPlaceHolder.Controls.Add(Span("Название устройства:"));
                    nameTextBox = TextBox();
                    nameTextBox.ID = "HeaterNameTextBox";
                    secondPlaceHolder.Controls.Add(nameTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Температура: "));
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Минимальное значение температуры: "));
                    minTemperatureValueTextBox = TextBox();
                    minTemperatureValueTextBox.ID = "HeaterMinTemperatureValueTextBox";
                    secondPlaceHolder.Controls.Add(minTemperatureValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Максимальное значение температуры: "));
                    maxTemperatureValueTextBox = TextBox();
                    maxTemperatureValueTextBox.ID = "HeaterMaxTemperatureValueTextBox";
                    secondPlaceHolder.Controls.Add(maxTemperatureValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    addHeaterButton = MyButton("Добавить");
                    addHeaterButton.ID = "AddHeaterButton";
                    addHeaterButton.Click += AddHeaterButton_Click;
                    addHeaterButton.CssClass = "templateAddDeviceButton";
                    secondPlaceHolder.Controls.Add(addHeaterButton);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                }
                if ((Conditioner)Page.Session["EmptyConditioner"] != null)
                {
                    secondPlaceHolder.Controls.Add(Span("Название устройства:"));
                    nameTextBox = TextBox();
                    nameTextBox.ID = "ConditionerNameTextBox";
                    secondPlaceHolder.Controls.Add(nameTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Температура: "));
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Минимальное значение температуры(не менее 17С): "));
                    minTemperatureValueTextBox = TextBox();
                    minTemperatureValueTextBox.ID = "ConditionerMinTemperatureValueTextBox";
                    secondPlaceHolder.Controls.Add(minTemperatureValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    secondPlaceHolder.Controls.Add(Span("Максимальное значение температуры(не более 30С): "));
                    maxTemperatureValueTextBox = TextBox();
                    maxTemperatureValueTextBox.ID = "ConditionerMaxTemperatureValueTextBox";
                    secondPlaceHolder.Controls.Add(maxTemperatureValueTextBox);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                    addConditionerButton = MyButton("Добавить");
                    addConditionerButton.ID = "AddConditionerButton";
                    addConditionerButton.Click += AddConditionerButton_Click;
                    addConditionerButton.CssClass = "templateAddDeviceButton";
                    secondPlaceHolder.Controls.Add(addConditionerButton);
                    secondPlaceHolder.Controls.Add(Span("<br />"));
                }
            }
        }

        protected void AddElementButton_Click(object sender, EventArgs e)
        {
            if (Page != null)
            {
                if ((UserDevice)Page.Session["EmptyUserDevice"] != null)
                {
                    userDevice = (UserDevice)Page.Session["EmptyUserDevice"];
                    userDevice.Sliders.Add((int)Page.Session["NextSliderId"], graphicFactory.CreateEmptySlider());
                    Page.Session["NextSliderId"] = (int)Page.Session["NextSliderId"] + 1;
                    Page.Session["EmptyUserDevice"] = userDevice;
                }
                DrawDevice();
            }
        }

        protected void RemoveElementButton_Click(object sender, EventArgs e)
        {
            if (Page != null)
            {
                if ((UserDevice)Page.Session["EmptyUserDevice"] != null)
                {
                    userDevice = (UserDevice)Page.Session["EmptyUserDevice"];
                    userDevice.Sliders.Remove((int)Page.Session["NextSliderId"] - 1);
                    if ((int)Page.Session["NextSliderId"] > 0)
                    {
                        Page.Session["NextSliderId"] = (int)Page.Session["NextSliderId"] - 1;
                    }
                    Page.Session["EmptyUserDevice"] = userDevice;
                }
                DrawDevice();
            }
        }

        protected void AddDeviceButton_Click(object sender, EventArgs e)
        {
            if (Page != null)
            {
                if ((UserDevice)Page.Session["EmptyUserDevice"] != null)
                {
                    userDevice = (UserDevice)Page.Session["EmptyUserDevice"];
                    userDevice.Name = nameTextBox.Text.ToString();
                    userDevice.Sliders.Clear();
                    bool correct = true;
                    for (int j = 1; j <= SlidersNamesTextBoxes.Count; j++)
                    {
                        userDevice.Sliders.Add(j, graphicFactory.CreateEmptySlider());
                        int minUDValue;
                        bool result1 = Int32.TryParse(MinValuesTextBoxes[j - 1].Text, out minUDValue);
                        int maxUDValue;
                        bool result2 = Int32.TryParse(MaxValuesTextBoxes[j - 1].Text, out maxUDValue);
                        userDevice.Sliders[j].SliderName = SlidersNamesTextBoxes[j - 1].Text;
                        userDevice.Sliders[j].MinValue = minUDValue;
                        userDevice.Sliders[j].CurrentValue = minUDValue;
                        userDevice.Sliders[j].MaxValue = maxUDValue;
                        if (result1 || result2 == false)
                        {
                            correct = false;
                        }
                    }
                    if (correct)
                    {
                        Dictionary<int, Device> devicesDictionary = (Dictionary<int, Device>)Page.Session["Devices"];
                        devicesDictionary.Add((int)Page.Session["NextDeviceId"], userDevice);
                        Page.Session["Devices"] = devicesDictionary;
                        Page.Session["NextDeviceId"] = (int)Page.Session["NextDeviceId"] + 1;
                        DisplayDeviceControl.Initializer();
                        displayPlaceHolder.Controls.Add(DisplayDeviceControl);
                        Page.Session["EmptyUserDevice"] = null;
                        Initializer();
                    }
                    else
                    {
                        Page.Session["EmptyUserDevice"] = null;
                        Initializer();
                        Controls.Add(Span("НЕКОРРЕКТНЫЙ ВВОД ДАННЫХ"));
                    }
                }
            }
        }

        protected void AddTvButton_Click(object sender, EventArgs e)
        {
            if (Page != null)
            {
                if ((TV)Page.Session["EmptyTV"] != null)
                {
                    tV = (TV)Page.Session["EmptyTV"];
                    tV.Name = nameTextBox.Text.ToString();
                    tV.Channel = graphicFactory.CreateEmptySlider();
                    tV.Sound = graphicFactory.CreateEmptySlider();
                    tV.Sound.SliderName = "Громкость";
                    int minSoundValue;
                    bool result3 = Int32.TryParse(minVolumeValueTextBox.Text, out minSoundValue);
                    int maxSoundValue;
                    bool result4 = Int32.TryParse(maxVolumeValueTextBox.Text, out maxSoundValue);
                    tV.Sound.MinValue = minSoundValue;
                    tV.Sound.CurrentValue = minSoundValue;
                    tV.Sound.MaxValue = maxSoundValue;
                    tV.Channel.SliderName = "Каналы";
                    int minChannelValue;
                    bool result1 = Int32.TryParse(minChannelValueTextBox.Text, out minChannelValue);
                    int maxChannelValue;
                    bool result2 = Int32.TryParse(maxChannelValueTextBox.Text, out maxChannelValue);
                    tV.Channel.MinValue = minChannelValue;
                    tV.Channel.CurrentValue = minChannelValue;
                    tV.Channel.MaxValue = maxChannelValue;
                    
                    if (result1 && result2 && result3 && result4)
                    {
                        Dictionary<int, Device> devicesDictionary = (Dictionary<int, Device>)Page.Session["Devices"];
                        devicesDictionary.Add((int)Page.Session["NextDeviceId"], tV);
                        Page.Session["Devices"] = devicesDictionary;
                        Page.Session["NextDeviceId"] = (int)Page.Session["NextDeviceId"] + 1;
                        DisplayDeviceControl.Initializer();
                        displayPlaceHolder.Controls.Add(DisplayDeviceControl);
                        Page.Session["EmptyTV"] = null;
                        Initializer();
                    }
                    else
                    {
                        Page.Session["EmptyTV"] = null;
                        Initializer();
                        Controls.Add(Span("НЕКОРРЕКТНЫЙ ВВОД ДАННЫХ"));
                    }
                }
            }
        }

        protected void AddHeaterButton_Click(object sender, EventArgs e)
        {
            if (Page != null)
            {
                if ((Heater)Page.Session["EmptyHeater"] != null)
                {
                    heater = (Heater)Page.Session["EmptyHeater"];
                    heater.Name = nameTextBox.Text.ToString();
                    heater.Temperature = graphicFactory.CreateEmptySlider();
                    heater.Temperature.SliderName = "Температура";
                    int minTempValue;
                    bool result1 = Int32.TryParse(minTemperatureValueTextBox.Text, out minTempValue);
                    int maxTempValue;
                    bool result2 = Int32.TryParse(maxTemperatureValueTextBox.Text, out maxTempValue);
                    heater.Temperature.MinValue = minTempValue;
                    heater.Temperature.CurrentValue = minTempValue;
                    heater.Temperature.MaxValue = maxTempValue;
                    if (result1 && result2 )
                    {
                        Dictionary<int, Device> devicesDictionary = (Dictionary<int, Device>)Page.Session["Devices"];
                        devicesDictionary.Add((int)Page.Session["NextDeviceId"], heater);
                        Page.Session["Devices"] = devicesDictionary;
                        Page.Session["NextDeviceId"] = (int)Page.Session["NextDeviceId"] + 1;
                        DisplayDeviceControl.Initializer();
                        displayPlaceHolder.Controls.Add(DisplayDeviceControl);
                        Page.Session["EmptyHeater"] = null;
                        Initializer();
                    }
                    else
                    {
                        Page.Session["EmptyHeater"] = null;
                        Initializer();
                        Controls.Add(Span("НЕКОРРЕКТНЫЙ ВВОД ДАННЫХ"));
                    }
                }
            }
        }

        protected void AddConditionerButton_Click(object sender, EventArgs e)
        {
            if (Page != null)
            {
                if ((Conditioner)Page.Session["EmptyConditioner"] != null)
                {
                    conditioner = (Conditioner)Page.Session["EmptyConditioner"];
                    conditioner.Name = nameTextBox.Text.ToString();
                    conditioner.Temperature = graphicFactory.CreateEmptySlider();
                    conditioner.Temperature.SliderName = "Температура";
                    int minTempValue;
                    bool result1 = Int32.TryParse(minTemperatureValueTextBox.Text, out minTempValue);
                    int maxTempValue;
                    bool result2 = Int32.TryParse(maxTemperatureValueTextBox.Text, out maxTempValue);
                    conditioner.Temperature.MinValue = minTempValue;
                    conditioner.Temperature.CurrentValue = minTempValue;
                    conditioner.Temperature.MaxValue = maxTempValue;
                    if (result1 && result2)
                    {
                        Dictionary<int, Device> devicesDictionary = (Dictionary<int, Device>)Page.Session["Devices"];
                        devicesDictionary.Add((int)Page.Session["NextDeviceId"], conditioner);
                        Page.Session["Devices"] = devicesDictionary;
                        Page.Session["NextDeviceId"] = (int)Page.Session["NextDeviceId"] + 1;
                        DisplayDeviceControl.Initializer();
                        displayPlaceHolder.Controls.Add(DisplayDeviceControl);
                        Page.Session["EmptyConitioner"] = null;
                        Initializer();
                    }
                    else
                    {
                        Page.Session["EmptyConditioner"] = null;
                        Initializer();
                        Controls.Add(Span("НЕКОРРЕКТНЫЙ ВВОД ДАННЫХ"));
                    }
                }
            }
        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        protected TextBox TextBox()
        {
            TextBox textBox = new TextBox();
            return textBox;
        }
        protected Button MyButton(string buttonName)
        {
            Button temp = new Button();
            temp.Text = buttonName;
            return temp;
        }
    }
}