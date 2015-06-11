using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Pic_a_Pix.CustomConfig
{
    class ColorConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("colors", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ColorConfigurationCollection), AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ColorConfigurationCollection Colors
        {
            get
            {
                return (ColorConfigurationCollection)base["colors"];
            }
        }

        [ConfigurationProperty("DefaultColor")]
        public ColorConfigurationElement DefaultColor
        {
            get { return (ColorConfigurationElement)this["DefaultColor"]; }
        }

        [ConfigurationProperty("Blank")]
        public ColorConfigurationElement Blank
        {
            get { return (ColorConfigurationElement)this["Blank"]; }
        }
    }
}
