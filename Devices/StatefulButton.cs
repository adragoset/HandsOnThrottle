using System;
using Microsoft.SPOT;
using FrameWork;
using Core.KeyPadState;
using Core;
using Hardware;
using HardWare;

namespace Devices
{
    public class StatefulButton : Button
    {
        private ButtonState StateList;

        private VirtualButton CurrentState;

        public uint LEDIndex { get; private set; }

        private HardWare.RGBLedSeriesController LedController;

        public StatefulButton(int id, InterruptInput ioPin, HardWare.RGBLedSeriesController controller, uint ledIndex, ButtonState stateList, string name)
            : base(id, ioPin, name)
        {
            LEDIndex = ledIndex;
            this.StateList = stateList;
            CurrentState = StateList.GetState(0);
            LedController = controller;
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
            CurrentState = StateList.GetState(0);
            ColorCode color = null;
            lock (button_lock)
            {
                CurrentState = StateList.GetState(0);
                color = ColorCode.GetColorCode(CurrentState.CurrentColor);
            }
            LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
        }

        public void IncrementState()
        {
            int index = -1;
            ColorCode color = null;

            lock (button_lock)
            {
                index = this.StateList.GetIndex(this.CurrentState);

                if (index == -1)
                {
                    throw new InvalidOperationException("Current state is not in list");
                }

                if (index + 1 < this.StateList.Length)
                {
                    this.CurrentState = this.StateList.GetState(index + 1);
                }
                else
                {
                    this.CurrentState = this.StateList.GetState(0);
                }

                color = ColorCode.GetColorCode(CurrentState.CurrentColor);
            }

            LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
        }

        public void SetState(int pageNumber)
        {
            lock (button_lock)
            {
                if (pageNumber <= this.StateList.Length && pageNumber > 0)
                {
                    this.CurrentState = this.StateList.GetState(pageNumber - 1);
                }

                ColorCode color = null;
                color = ColorCode.GetColorCode(CurrentState.CurrentColor);
                LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
            }
        }

        public override int ButtonId()
        {
            lock (button_lock)
            {
                return this.CurrentState.Id;
            }
        }

        public void IncrementColorState()
        {
            ColorCode color = null;

            lock (button_lock)
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

                color = ColorCode.GetColorCode(CurrentState.CurrentColor);
            }

            LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
        }

        public void ResetColorState()
        {
            ColorCode color = null;

            lock (button_lock)
            {
                this.CurrentState.CurrentColor = this.CurrentState.InitialColor;

                color = ColorCode.GetColorCode(CurrentState.CurrentColor);

            }

            LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
        }

        public void SetColorState(int index)
        {
            ColorCode color = null;

            lock (button_lock)
            {
                if (index < 0 && index <= CurrentState.StateList.Length)
                {
                    this.CurrentState.CurrentColor = this.CurrentState.StateList[index];

                    color = ColorCode.GetColorCode(CurrentState.CurrentColor);

                }
            }

            if (color != null)
            {
                LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
            }
        }

        protected override void Button_Pressed(InterruptInput sender, bool value)
        {
            base.Button_Pressed(sender, value);
            if (this.WasPressed())
            {
                ColorCode color = null;
                IncrementColorState();
                On_Press(new ButtonPressedArgs());
            }
        }

        public delegate void ButtonPressedDelegate(object sender, ButtonPressedArgs e);

        public event ButtonPressedDelegate ButtonPressed;

        public class ButtonPressedArgs : EventArgs
        {
        }

        private void On_Press(ButtonPressedArgs e)
        {
            if (ButtonPressed != null)
            {
                ButtonPressed(this, e);
            }
        }
    }
}
