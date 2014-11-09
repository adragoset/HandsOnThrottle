using System;
using Microsoft.SPOT;

namespace Core
{
    public enum Color { Off = 1, White = 2, Red = 3, Green = 4, Blue = 5, Yellow = 6, Orange = 7, Purple = 8, Pink = 9, Magenta = 10, LightBlue = 11, SeaFoam = 12, SeaFoamGreen = 13, YellowGreen = 14 }

    public class ColorCode
    {

        public static ColorCode[] ColorCodes = { 
                                                   new ColorCode { Name = "Off", R = 0, G = 0, B = 0 },
                                                   new ColorCode { Name = "White", R = 255, G = 255, B = 255 }, 
                                                   new ColorCode { Name = "Red" , R = 255, G = 0, B = 0 },
                                                   new ColorCode { Name = "Green" , R= 0, G = 255, B = 0 },
                                                   new ColorCode { Name = "Blue", R = 0, G = 0, B = 255 },
                                                   new ColorCode { Name = "Yellow", R = 255, G = 255, B = 0 },
                                                   new ColorCode { Name = "Orange", R = 255, G = 128, B = 0 },
                                                   new ColorCode { Name = "Purple", R = 128, G = 0, B = 255 },
                                                   new ColorCode { Name = "Pink", R = 255, G = 0, B = 128 },
                                                   new ColorCode { Name = "Magenta", R = 255, G = 0, B = 255 },
                                                   new ColorCode { Name = "Light Blue", R = 0, G = 128, B = 255 },
                                                   new ColorCode { Name = "Sea Foam", R = 0, G = 255, B = 255 },
                                                   new ColorCode { Name = "Sea Foam Green", R = 0, G = 255, B = 128 },
                                                   new ColorCode { Name = "Yellow Green", R = 128, G = 255, B = 0 }
                                               };
        public string Name { get; set; }
        public uint R { get; set; }
        public uint G { get; set; }
        public uint B { get; set; }

        public static ColorCode GetColorCode(Color color)
        {
            return ColorCodes[((int)color) - 1];
        }
    }
}
