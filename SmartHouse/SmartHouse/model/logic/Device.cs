using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace SmartHouse
{
    public abstract class Device
    {
        public bool Power { get; set; }
        public string Name { get; set; }

        public virtual void On()
        {
            Power = true;
        }
        public virtual void Off()
        {
            Power = false;
        }
    }
}