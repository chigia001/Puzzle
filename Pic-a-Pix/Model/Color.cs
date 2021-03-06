﻿using System.Collections.Generic;
using System.Configuration;
using Pic_a_Pix.CustomConfig;

namespace Pic_a_Pix.Model
{
    public class Color
    {
        public string ColorName { get; set; }
        public string HintCharacter { get; set; }
        public string ColorCode { get; set; }
        public Color(ColorConfigurationElement element)
        {
            ColorName = element.Color;
            ColorCode = element.ColorCode;
            HintCharacter = element.HintCharacter;
        }
    }

    public class ColorDictionary
    {
        public static ColorDictionary current = new ColorDictionary();

        public Dictionary<string, Color> Colors { private set; get; }

        public Color DefaultColor { private set; get; }

        public Color Blank { private set; get; }

        private ColorDictionary()
        {
            ColorConfigurationSection myCustomSection = (ColorConfigurationSection)ConfigurationManager.GetSection("ColorConfigurationSection");

            Colors = new Dictionary<string, Color>();

            foreach (ColorConfigurationElement element in myCustomSection.Colors)
            {
                if (!string.IsNullOrEmpty(element.HintCharacter))
                {
                    Colors.Add(element.HintCharacter, new Color(element));
                }
            }

            DefaultColor = new Color(myCustomSection.DefaultColor);

            Blank = new Color(myCustomSection.Blank);

        }

    }
}
