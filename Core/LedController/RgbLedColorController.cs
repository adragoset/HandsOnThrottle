using System;
using Microsoft.SPOT;
using FrameWork;
using Microsoft.SPOT.Hardware;

namespace Core.LedController
{
    public class RgbLedColorController
    {

        private object device_lock = new object();

        private PWM RedChannel;

        private PWM BlueChannel;

        private PWM GreenChannel;

        private bool CommonAnode = false;

        public RgbLedColorController(PWM[] outputs, bool commonAnode)
        {

            if (outputs.Length != 3)
            {
                throw new ArgumentException("You must provide 3 pwm outputs for a RGB Led Color Controller");
            }

            CommonAnode = commonAnode;

            RedChannel = outputs[0];

            GreenChannel = outputs[1];

            BlueChannel = outputs[2];
        }

        private void Write(uint red, uint green, uint blue)
        {
            lock (device_lock)
            {
                // Values are sent from 0 to 255, but we actually want 0 to 100.
                uint uRed = (red * 100 / 255);
                uint uGreen = (green * 100 / 255);
                uint uBlue = (blue * 100 / 255);

                if (this.CommonAnode)
                {
                    uRed = 100 - uRed;
                    uGreen = 100 - uGreen;
                    uBlue = 100 - uBlue;
                }

                // Sets the values
                this.RedChannel.DutyCycle = 50;
                this.GreenChannel.DutyCycle = 50;
                this.BlueChannel.DutyCycle = 50;

                this.RedChannel.Start();
                this.GreenChannel.Start();
                this.BlueChannel.Start();
            }
        }

        public void SetColor(Color color)
        {

            ColorCode colorCode = ColorCode.GetColorCode(color);

            Write(colorCode.R, colorCode.G, colorCode.B);
        }

        public void TurnOff()
        {
            lock (device_lock)
            {
                this.RedChannel.Stop();
                this.GreenChannel.Stop();
                this.BlueChannel.Stop();
            }
        }
    }
}
