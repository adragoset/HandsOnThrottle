using System;
using Microsoft.SPOT;
using FrameWork;
using HardWare;
using Core.KeyPadState;

namespace Core.KeyPadElements
{
    public class StatefulButton : Button
    {
        private ButtonState StateList;

        private VirtualButton CurrentState;

        public uint LEDIndex { get; private set; }

        private RGBLedSeriesController LedController;

        public StatefulButton(int id, InterruptInput ioPin, RGBLedSeriesController controller, uint ledIndex, ButtonState stateList, string name)
            : base(id, ioPin, name)
        {
            LEDIndex = ledIndex;
            this.StateList = stateList;
            CurrentState = StateList.States[0];
            LedController = controller;
        }

        public void ResetColorState()
        {
            lock (button_lock)
            {
                this.CurrentState.CurrentColor = this.CurrentState.InitialColor;
            }
        }

        public Color GetCurrentColor()
        {
            lock (button_lock)
            {
                return this.CurrentState.CurrentColor;
            }
        }

        public void Home()
        {
            CurrentState = StateList.States[0];
            ColorCode color = null;
            lock (button_lock)
            {
                color = ColorCode.GetColorCode(CurrentState.InitialColor);
            }

            LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
        }

        public void IncrementState()
        {
            int index = -1;

            lock (button_lock)
            {

                for (var i = 0; i < this.StateList.States.Length; i++)
                {
                    if (this.StateList.States[i] == this.CurrentState)
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                {
                    throw new InvalidOperationException("Current state is not in list");
                }

                if (index + 1 < this.StateList.States.Length)
                {
                    this.CurrentState = this.StateList.States[index + 1];
                }
                else
                {
                    this.CurrentState = this.StateList.States[0];
                }

                ColorCode color = null;
                color = ColorCode.GetColorCode(CurrentState.CurrentColor);
                LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
            }
        }

        protected override void Button_Pressed(InterruptInput sender, bool value)
        {
            base.Button_Pressed(sender, value);
            if (this.WasPressed())
            {
                ColorCode color = null;
                lock (button_lock)
                {
                    TransitionColorState();
                    color = ColorCode.GetColorCode(CurrentState.CurrentColor);
                }

                LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
                On_Press(new ButtonPressedArgs());
            }
        }

        public override int ButtonId()
        {
            lock (button_lock)
            {
                return this.CurrentState.Id;
            }
        }

        private void TransitionColorState()
        {
            int index = -1;

            for (var i = 0; i < this.CurrentState.StateList.Length; i++)
            {
                if (this.CurrentState.StateList[i] == this.CurrentState.CurrentColor)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException("Current color is not in list");
            }

            if (index + 1 < this.CurrentState.StateList.Length)
            {
                this.CurrentState.CurrentColor = this.CurrentState.StateList[index + 1];
            }
            else
            {
                this.CurrentState.CurrentColor = this.CurrentState.InitialColor;
            }
        }

        public delegate void ButtonPressedDelegate(object sender, ButtonPressedArgs e);

        public event ButtonPressedDelegate ButtonPressed;

        public class ButtonPressedArgs : EventArgs
        {
        }

        private void On_Press(ButtonPressedArgs e){
            if (ButtonPressed != null)
            {
                ButtonPressed(this, e);
            }
        }
    }
}
