using System;
using Microsoft.SPOT;

namespace Core.KeyPadElements
{
    public abstract class RGBKeypad
    {
        public StatefulButton[] Buttons { get; private set; }

        public StatefulButton CommandButton { get; private set; }

        public int Pages { get; private set; }

        public virtual int CurrentPage { get; private set; }

        public RGBKeypad(StatefulButton[] buttons, StatefulButton commandButton)
        {

            Buttons = buttons;
            CommandButton = commandButton;
            CommandButton.ButtonPressed += CommandButton_ButtonPressed;
            CurrentPage = 1;

        }

        private void CommandButton_ButtonPressed(object sender, StatefulButton.ButtonPressedArgs e)
        {
            foreach (var button in Buttons)
            {
                button.IncrementState();
            }
        }

        public virtual void Reset()
        {

        }

        public virtual void SetPage(int pageNumber)
        {

        }
    }
}
