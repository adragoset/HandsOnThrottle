using System;
using Microsoft.SPOT;
using FrameWork;
using Core;
using Core.KeyPadElements;
using Core.KeyPadState;
using System.Threading;
using Core.LedController;
using Microsoft.SPOT.Hardware;

namespace Devices
{
    public class KeyPad : InputArray
    {
        private static object State_Lock = new object();

        private int PhysicalButtonCount;
        //private int CommandButton;

        private RgbLedSeriesController LedController;

        private StatefulRGBKeypad KeyPadState;

        private bool CommandButtonPressed;

        public KeyPad(InterruptInput[] KeyPadIoInputs, OutputPort[] ledSwitchOutputs, PWM[] pwmOutputs, string config)
            : base(KeyPadIoInputs)
        {
            PhysicalButtonCount = KeyPadIoInputs.Length;

            KeyPadState = BuildKeyPad(config);

            KeyPadState.HomePage();

            LedController = new RgbLedSeriesController(ledSwitchOutputs, pwmOutputs);

            CommandButtonPressed = false;
        }

        private StatefulRGBKeypad BuildKeyPad(string config)
        {
            KeyPadStateData data = getData(config);
            StatefulRGBPage[] pages = getPages(data.PageStates);
            StatefulButton commandButton = new StatefulButton(1, Inputs[0], 1, data.CommandButton.StateList, data.CommandButton.InitialColor, data.CommandButton.CurrentColor);
            return new StatefulRGBKeypad(pages, commandButton);
        }

        private StatefulRGBPage[] getPages(PageState[] pageState)
        {
            var pages = new StatefulRGBPage[pageState.Length];
            var currentPin = 2;
            for (int i = 0; i < pageState.Length; i++)
            {
                StatefulButton[] Buttons = new StatefulButton[PhysicalButtonCount - 1];
                for (var b = 0; b < PhysicalButtonCount - 1; b++)
                {
                    if (currentPin <= Inputs.Length)
                    {
                        var page = pageState[i];
                        var buttonstate = page.ButtonStates[b];
                        Buttons[b] = new StatefulButton(buttonstate.Id, Inputs[currentPin - 1], currentPin, buttonstate.StateList, buttonstate.InitialColor, buttonstate.CurrentColor);
                        if (currentPin <= PhysicalButtonCount - 1)
                        {
                            currentPin++;
                        }
                        else
                        {
                            currentPin = 2;
                        }
                    }
                }

                pages[i] = new StatefulRGBPage(i + 1, Buttons);
            }

            return pages;
        }

        private KeyPadStateData getData(string config)
        {
            return KeyPadConfigurationParser.GetConfig(config);
        }

        public int[] ReadButtonPresses()
        {
            int[] buttonIds;

            if (KeyPadState.CommandButtonWasPressed())
            {
                buttonIds = new int[] { KeyPadState.CommandButton.ButtonId };
                if (!CommandButtonPressed)
                {
                    lock (KeyPadState)
                    {
                        KeyPadState.IncrementPage();
                    }
                    CommandButtonPressed = true;
                }
            }
            else
            {
                CommandButtonPressed = false;
                StatefulButton[] pressedButtons;

                lock (KeyPadState)
                {
                    pressedButtons = KeyPadState.PressedButtons();
                }

                buttonIds = new int[pressedButtons.Length];
                for (var i = 0; i < buttonIds.Length; i++)
                {
                    buttonIds[i] = pressedButtons[i].ButtonId;
                }
            }


            return buttonIds;
        }

        public void RunButtonLEDStates()
        {
            var commandButton = KeyPadState.CommandButton;
            LedController.SetLed(commandButton.LEDIndex, commandButton.GetCurrentColor());
            Thread.Sleep(5);
            StatefulButton[] buttons;

            lock (KeyPadState)
            {
                buttons = KeyPadState.StatefulRGBCurrentPage.Buttons;
            }

            foreach (var button in buttons)
            {
                LedController.SetLed(button.LEDIndex, button.GetCurrentColor());
                Thread.Sleep(5);
            }
        }
    }
}
