using System;
using Microsoft.SPOT;
using System.Collections;
using Hardware;
using Core;

namespace Devices
{
    public class StatefulRGBKeypad : RGBKeypad
    {
        public StatefulRGBKeypad(StatefulButton[] buttons, StatefulButton commandButton)
            : base(buttons, commandButton)
        {

        }

        

        public void HomePage()
        {
            base.CommandButton.Home();

        }

        public override void SetPage(int pageNumber)
        {

            base.SetPage(pageNumber);

        }

        public int[] PressedButtons()
        {

            int[] allButtons = new int[8];
            if (CommandButton.WasPressed())
            {

                allButtons[0] = CommandButton.ButtonId();
            }
           

            for (int i = 0; i < Buttons.Length; i++)
            {
                if (Buttons[i].WasPressed())
                {
                    allButtons[i + 1] = Buttons[i].ButtonId();
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
