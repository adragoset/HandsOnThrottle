using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;


namespace Core
{
    public class RGBLedOutput
    {

        private static object Lock = new object();

        private PWM Red;

        private PWM Blue;

        private PWM Green;

        private bool CommonAnode;

        public RGBLedOutput(PWM red, PWM blue, PWM green, bool commonAnode)
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
                this.Red.Stop();
                this.Blue.Stop();
                this.Green.Stop();

                ColorCode colorCode = ColorCode.GetColorCode(color);
                // Values are sent from 0 to 255, but we actually want 0 to 100.
                uint uRed = (uint)(colorCode.R * 100 / 255);
                uint uGreen = (uint)(colorCode.G * 100 / 255);
                uint uBlue = (uint)(colorCode.B * 100 / 255);

                if (this.CommonAnode)
                {
                    uRed = 100 - uRed;
                    uGreen = 100 - uGreen;
                    uBlue = 100 - uBlue;
                }

                // Sets the values
                this.Red.DutyCycle = 50;
                this.Green.DutyCycle = 50;
                this.Blue.DutyCycle = 50;

                this.Red.Start();
                this.Blue.Start();
                this.Green.Start();
            }
        }


    }
}