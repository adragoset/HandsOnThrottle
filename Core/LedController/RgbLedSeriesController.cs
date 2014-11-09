using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Core.LedController
{
    public class RgbLedSeriesController : LedSwitcherController
    {
        private RgbLedColorController ColorController;

        public RgbLedSeriesController(OutputPort[] ledSockets, PWM[] pwmBank)
            : base(ledSockets)
        {
            ColorController = new RgbLedColorController(pwmBank, true);
        }

        public void SetLed(int led, Color color)
        {
            this.SetToOffState();
            ColorController.SetColor(color);
            this.LightLed(led);
        }
    }
}
