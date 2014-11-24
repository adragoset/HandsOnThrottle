using System;
using Microsoft.SPOT;
using FrameWork;

namespace Core.KeyPadElements
{
    public class StatefulButton : Button
    {
        public Color[] StateList { get; set; }

        public Color InitialColor { get; private set; }

        private Color CurrentColor { get; set; }

        public int LEDIndex { get; private set; }

        public StatefulButton(int id, InterruptInput ioPin, int ledIndex, Color[] stateList, Color initialColor, Color currentColor, string name)
            : base(id, ioPin, name)
        {
            LEDIndex = ledIndex;
            this.StateList = stateList;
            this.InitialColor = initialColor;
            this.CurrentColor = currentColor;
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
            lock (button_lock)
            {
                TransitionState();
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
