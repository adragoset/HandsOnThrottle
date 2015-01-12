using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using FrameWork;

namespace Core
{
    public abstract class Button
    {
        private int buttonId { get; set; }

        public string ButtonName { get; private set; }

        protected object button_lock;

        protected bool wasPressed;

        private InterruptInput input;


        public Button(int id, InterruptInput ioPin, string name)
        {
            button_lock = new object();
            buttonId = id;

            input = ioPin;

            this.wasPressed = false;
            this.ButtonName = name;

            input.Interrupt += Button_Pressed;

        }

        private void Test_Button(object state)
        {
            throw new NotImplementedException();
        }

        public virtual int ButtonId(){
            lock(button_lock){
                return this.buttonId;
            }
        }

        public virtual bool WasPressed()
        {
            lock (button_lock)
            {
                return this.wasPressed;
            }
        }

        protected virtual void Button_Pressed(InterruptInput sender, bool value)
        {
            lock (button_lock)
            {
                var state = !value;
                if (state != wasPressed)
                {
                    wasPressed = state;
                }
            }
        }

    }
}
