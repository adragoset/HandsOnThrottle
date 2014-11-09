using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using FrameWork;

namespace Core
{
    public abstract class Button
    {
        public int ButtonId { get; private set; }

        protected object button_lock;

        protected bool active;

        public bool Active
        {
            get
            {
                lock (button_lock)
                {
                    return active;
                }
            }
            private set
            {
                active = value;
            }
        }

        protected bool wasPressed;

        public bool WasPressed
        {
            get
            {
                lock (button_lock)
                {
                    return wasPressed;
                }
            }
            private set
            {
                wasPressed = value;
            }
        }

        private InterruptInput input;

        public Button(int id, InterruptInput ioPin, bool isActive)
        {
            this.button_lock = new object();
            ButtonId = id;

            input = ioPin;

            this.wasPressed = false;

            if (isActive)
            {
                this.active = true;
                input.Interrupt += Button_Pressed;
            }
            else
            {
                this.active = false;
            }
        }

        public void Activeate()
        {
            lock (button_lock)
            {
                if (!active)
                {
                    input.Interrupt += Button_Pressed;
                    this.active = true;
                }
            }
        }

        public void DeActivate()
        {
            lock (button_lock)
            {
                if (active)
                {
                    input.Interrupt -= Button_Pressed;
                    this.active = false;
                    this.wasPressed = false;
                }
            }
        }

        protected virtual void Button_Pressed(InterruptInput sender, bool value)
        {


            if (active)
            {
                lock (button_lock)
                {
                    if (wasPressed)
                    {
                        wasPressed = false;
                    }
                    else
                    {
                        wasPressed = true;
                    }
                }
            }
        }
    }
}
