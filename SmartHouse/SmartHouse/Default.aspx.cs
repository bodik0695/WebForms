using SmartHouse.model.GraphicModel;
using SmartHouse.model.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartHouse
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private IDictionary<int, Device> devicesDictionary;
        private GeneralControl GeneralControl;
        private DisplayDeviceControl displayDeviceControl;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                devicesDictionary = (Dictionary<int, Device>)Session["Devices"];
            }
            else
            {
                devicesDictionary = new Dictionary<int, Device>();
                Session["Devices"] = devicesDictionary;
                Page.Session["NextDeviceId"] = 1;
                Page.Session["NextSliderId"] = 1;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
            else
            {
                
            }
            InitilizeCreateDevicePanel();
        }

        protected void InitilizeCreateDevicePanel()
        {
            displayDeviceControl = new DisplayDeviceControl(devicesDictionary, DisplayPlaceHolder);
            GeneralControl = new GeneralControl(displayDeviceControl, DisplayPlaceHolder);
            TemplatesPlaceHolder.Controls.Add(GeneralControl);
            GeneralControl.Initializer();
            DisplayPlaceHolder.Controls.Add(displayDeviceControl);
        }
    }
}