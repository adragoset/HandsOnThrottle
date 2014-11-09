using System;
using Microsoft.SPOT;
using System.Collections;

namespace Core.KeyPadElements
{
    public abstract class RGBPage
    {
        public int PageNumber { get; set; }

        public StatefulButton[] Buttons { get; set; }

        public StatefulButton[] PressedButtons
        {
            get
            {
                var pressedButtons = new ArrayList();
                foreach (var button in Buttons)
                {
                    if (button.WasPressed)
                    {
                        pressedButtons.Add(Buttons);
                    }
                }

                StatefulButton[] result = new StatefulButton[pressedButtons.Count];
                for (var count = 0; count < pressedButtons.Count; count++)
                {
                    result[count] = (StatefulButton)pressedButtons[count];
                }
                return result;
            }
            private set { }
        }

        public bool Active { get; private set; }

        public RGBPage(int pageNumber, StatefulButton[] buttonStates)
        {
            PageNumber = pageNumber;
            Buttons = buttonStates;
        }

        public StatefulButton GetButton(int buttonNumber)
        {

            foreach (var button in Buttons)
            {
                if (button.ButtonId == buttonNumber)
                {
                    return button;
                }
            }

            return null;
        }

        public virtual void Activate()
        {
            if (!this.Active)
            {
                foreach (var button in Buttons)
                {
                    button.Activeate();
                }
                Active = true;
            }
        }

        public virtual void DeActivate()
        {
            if (this.Active)
            {
                foreach (var button in Buttons)
                {
                    button.DeActivate();
                }
                Active = false;
            }
        }

        public virtual void ResetColorStates()
        {
            foreach (var button in Buttons)
            {
                button.ResetColorState();
            }
        }
    }
}
