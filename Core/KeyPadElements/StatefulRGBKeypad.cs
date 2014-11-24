using System;
using Microsoft.SPOT;
using System.Collections;

namespace Core.KeyPadElements
{
    public class StatefulRGBKeypad : RGBKeypad
    {
      

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

        public StatefulButton[] PressedButtons()
        {

            StatefulButton[] allButtons = null;

            if (CommandButton.WasPressed())
            {
                allButtons = new StatefulButton[1];
                allButtons[0] = CommandButton;
            }
            else
            {
                ArrayList buttons = new ArrayList();

                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (Buttons[i].WasPressed())
                    {
                        buttons.Add(Buttons[i]);
                    }
                }

                allButtons = new StatefulButton[buttons.Count];

                int index = 0;
                foreach (var button in buttons) {
                    allButtons[index] = (StatefulButton)button;
                    index++;
                }
            }

            return allButtons;
        }

        public void Clear()
        {
            CommandButton.ResetColorState();
            base.Reset();
        }


    }
}
