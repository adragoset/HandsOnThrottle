using System;
using Microsoft.SPOT;
using System.Collections;

namespace Core.KeyPadElements
{
    public class StatefulRGBKeypad : RGBKeypad
    {

        private bool CommandStateObserved = false; 
        public StatefulRGBKeypad(StatefulButton[] buttons, StatefulButton commandButton)
            : base(buttons, commandButton)
        {

        }

        public void IncrementPage()
        {

        }

        public void HomePage()
        {

        }

        public override void SetPage(int pageNumber)
        {

            base.SetPage(pageNumber);

        }

        public int[] PressedButtons()
        {

            int[] allButtons = new int[8];
            bool commandWasPressed = false;
            if (CommandButton.WasPressed())
            {
                if (!CommandStateObserved)
                {
                    commandWasPressed = true;
                    CommandStateObserved = true;
                }

                allButtons[0] = CommandButton.ButtonId();
            }
            else {
                CommandStateObserved = false;
            }

            for (int i = 0; i < Buttons.Length; i++)
            {
                if (Buttons[i].WasPressed())
                {
                    allButtons[i + 1] = Buttons[i].ButtonId();
                }

                if (commandWasPressed)
                {
                    Buttons[i].IncrementState();
                }
            }

            return allButtons;
        }

        public Color[] KeypadColorState()
        {
            Color[] results = new Color[8];
            int index = 1;
            results[0] = CommandButton.GetCurrentColor();

            foreach (var button in Buttons)
            {
                results[index] = button.GetCurrentColor();
                index++;
            }

            return results;
        }

        public void Clear()
        {
            CommandButton.ResetColorState();
            base.Reset();
        }


    }
}
