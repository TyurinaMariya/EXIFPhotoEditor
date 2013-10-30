using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EXIFPhotoEditor
{
    internal class MainSettings
    {
        public string PathToLastImageDir
        {
            get
            {
                return ConfigurationManager.AppSettings["PathToLastImageDir"];
            }
            set
            {
                UpdateProperty("PathToLastImageDir", value);
            }
        }

        public string PathToLastGPXDir
        {
            get
            {
                return ConfigurationManager.AppSettings["PathToLastGPXDir"];
            }
            set
            {
                UpdateProperty("PathToLastGPXDir", value);
            }
        }

        public string LastDeltaTime
        {
            get
            {
                return ConfigurationManager.AppSettings["LastDeltaTime"];
            }
            set
            {
                UpdateProperty("LastDeltaTime", value);
            }
        }

        public string LastTimeZone
        {
            get
            {
                return ConfigurationManager.AppSettings["LastTimeZone"];
            }
            set
            {
                UpdateProperty("LastTimeZone", value);
            }
        }
        private void UpdateProperty(string name, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(name);
            config.AppSettings.Settings.Add(name, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
