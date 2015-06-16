using System.Configuration;

namespace Pic_a_Pix.CustomConfig
{
    public class ColorConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("color", IsRequired = true)]
        public string Color
        {
            get { return (string)this["color"]; }
            set { this["color"] = value; }
        }

        [ConfigurationProperty("hintCharacter", IsRequired = true)]
        public string HintCharacter
        {
            get { return (string)this["hintCharacter"]; }
            set { this["hintCharacter"] = value; }
        }

        [ConfigurationProperty("colorCode", IsRequired = true)]
        public string ColorCode
        {
            get { return (string)this["colorCode"]; }
            set { this["colorCode"] = value; }
        }

    }
}
