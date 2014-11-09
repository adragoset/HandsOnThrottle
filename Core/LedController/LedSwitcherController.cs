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

            lock (device_lock)
            {
                LedBank[led - 1].Write(true);
            }
        }

        public virtual void SetToOffState()
        {
            lock (device_lock)
            {
                for (var i = 0; i < LedBank.Length; i++)
                {
                    LedBank[i].Write(false);
                }
            }
        }

    }
}
