using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Core;


namespace Hardware
{
    public class RGBLedOutput
    {

        private static object Lock = new object();

        private PWM Red;

        private PWM Blue;

        private PWM Green;

        private bool CommonAnode;

        public RGBLedOutput(PWM green, PWM red, PWM blue, bool commonAnode)
        {
            this.Red = red;
            this.Blue = blue;
            this.Green = green;
            this.CommonAnode = commonAnode;
        }

        public void SetColor(Color color)
        {
            lock (Lock)
            {
               

                ColorCode colorCode = ColorCode.GetColorCode(color);
                // Values are sent from 0 to 255, but we actually want 0 to 1.
                double uRed = (colorCode.R * .7) / 255.0;
                double uGreen = colorCode.G  / 255.0;
                double uBlue = (colorCode.B * .7) / 255.0;

                // Sets the values
                this.Red.DutyCycle = uRed;
                this.Green.DutyCycle = uGreen;
                this.Blue.DutyCycle = uBlue;

                
            }
        }


    }
}