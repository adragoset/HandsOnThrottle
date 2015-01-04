using System;
using Microsoft.SPOT;
using FrameWork;
using HardWare;

namespace Core.KeyPadElements
{
    public class StatefulButton : Button
    {
        public Color[] StateList { get; set; }

        public Color InitialColor { get; private set; }

        private Color CurrentColor { get; set; }

        public uint LEDIndex { get; private set; }

        private RGBLedSeriesController LedController;

        public StatefulButton(int id, InterruptInput ioPin, RGBLedSeriesController controller, uint ledIndex, Color[] stateList, Color initialColor, Color currentColor, string name)
            : base(id, ioPin, name)
        {
            LEDIndex = ledIndex;
            this.StateList = stateList;
            this.InitialColor = initialColor;
            this.CurrentColor = currentColor;
            LedController = controller;
        }

        public void ResetColorState()
        {
            lock (button_lock)
            {
                CurrentColor = InitialColor;
            }
        }

        public Color GetCurrentColor()
        {
            lock (button_lock)
            {
                return this.CurrentColor;
            }
        }

        protected override void Button_Pressed(InterruptInput sender, bool value)
        {
            base.Button_Pressed(sender, value);
            if (this.WasPressed()) {
                ColorCode color = null;
                lock (button_lock)
                {
                    TransitionState();
                    color = ColorCode.GetColorCode(this.CurrentColor);
                }

                LedController.UpdateLed(new Led(this.LEDIndex, color.R, color.G, color.B));
            }
        }

        private void TransitionState()
        {
            int index = -1;

            for (var i = 0; i < StateList.Length; i++)
            {
                if (StateList[i] == CurrentColor)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException("Current color is not in list");
            }

            if (index + 1 < StateList.Length)
            {
                CurrentColor = StateList[index + 1];
            }
            else
            {
                CurrentColor = InitialColor;
            }
        }
    }
}
