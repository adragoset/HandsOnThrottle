using System;
using Microsoft.SPOT;
using FrameWork;
using Microsoft.SPOT.Hardware;

namespace Core.LedController
{
    public abstract class LedSwitcherController
    {
        private static object device_lock = new object();
        private OutputPort[] LedBank;
        private int litLedIndex = 0;


        public LedSwitcherController(OutputPort[] outputs)
        {
            LedBank = outputs;
        }

        public void LightLed(int led)
        {
            if (led > LedBank.Length)
            {
                throw new ArgumentException("LED number must be <= than the total number of LEDs");
            }

            litLedIndex = led - 1;
            LedBank[led - 1].Write(true);

        }

        public virtual void SetToOffState()
        {
            LedBank[litLedIndex].Write(false);
        }

    }
}
