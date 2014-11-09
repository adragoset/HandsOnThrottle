using System;
using Microsoft.SPOT;

namespace Core.KeyPadElements
{
    public class StatefulRGBKeypad : RGBKeypad
    {
        public StatefulRGBPage StatefulRGBCurrentPage
        {
            get
            {
                return (StatefulRGBPage)base.CurrentPage;
            }
        }

        public StatefulRGBKeypad(StatefulRGBPage[] pages, StatefulButton commandButton)
            : base(pages, commandButton)
        {

        }

        public void IncrementPage()
        {
            if (StatefulRGBCurrentPage.PageNumber == Pages)
            {
                HomePage();
            }
            else
            {
                SetPage(StatefulRGBCurrentPage.PageNumber + 1);
            }
        }

        public void HomePage()
        {
            SetPage(1);
        }

        public override void SetPage(int pageNumber)
        {
            ((StatefulRGBPage)StatefulRGBCurrentPage).DeActivate();
            base.SetPage(pageNumber);
            ((StatefulRGBPage)StatefulRGBCurrentPage).Activate();
        }

        public bool CommandButtonWasPressed()
        {
            return CommandButton.WasPressed;
        }

        public StatefulButton[] PressedButtons()
        {
            var buttons = ((StatefulRGBPage)StatefulRGBCurrentPage).PressedButtons;
            var allButtons = new StatefulButton[buttons.Length];

            if (CommandButton.WasPressed)
            {
                allButtons = new StatefulButton[1];
                allButtons[buttons.Length] = CommandButton;
            }
            else
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].WasPressed)
                    {
                        allButtons[i] = buttons[i];
                    }
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
