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

        public KeyPad(InterruptInput[] KeyPadIoInputs, OutputPort[] ledSwitchOutputs, PWM[] pwmOutputs, string config)
            : base(KeyPadIoInputs)
        {
            PhysicalButtonCount = KeyPadIoInputs.Length;

            KeyPadState = BuildKeyPad(config);

            KeyPadState.HomePage();

            LedController = new RgbLedSeriesController(ledSwitchOutputs, pwmOutputs);

           
        }

        private StatefulRGBKeypad BuildKeyPad(string config)
        {
            KeyPadStateData data = getData(config);
            StatefulButton[] buttons = getButtons(data.PageStates);
            StatefulButton commandButton = new StatefulButton(1, Inputs[0], 1, data.CommandButton.StateList, data.CommandButton.InitialColor, data.CommandButton.CurrentColor, "Command_B");
            return new StatefulRGBKeypad(buttons, commandButton);
        }

        private StatefulButton[] getButtons(PageState[] pageState)
        {
            var buttons = new StatefulButton[7];

            buttons[0] = new StatefulButton(2, Inputs[1], 2, new Color[] { Color.Blue, Color.Green }, Color.Blue, Color.Blue, "Keypad_B_2");
            buttons[1] = new StatefulButton(3, Inputs[2], 3, new Color[] { Color.Blue, Color.Green }, Color.Blue, Color.Blue, "Keypad_B_3");
            buttons[2] = new StatefulButton(4, Inputs[3], 4, new Color[] { Color.Blue, Color.Green }, Color.Blue, Color.Blue, "Keypad_B_4");
            buttons[3] = new StatefulButton(5, Inputs[4], 5, new Color[] { Color.Blue, Color.Green }, Color.Blue, Color.Blue, "Keypad_B_5");
            buttons[4] = new StatefulButton(6, Inputs[5], 6, new Color[] { Color.Blue, Color.Green }, Color.Blue, Color.Blue, "Keypad_B_6");
            buttons[5] = new StatefulButton(7, Inputs[6], 7, new Color[] { Color.Blue, Color.Green }, Color.Blue, Color.Blue, "Keypad_B_7");
            buttons[6] = new StatefulButton(8, Inputs[7], 8, new Color[] { Color.Blue, Color.Green }, Color.Blue, Color.Blue, "Keypad_B_8");
              

            return buttons;
        }

        private KeyPadStateData getData(string config)
        {
            return KeyPadConfigurationParser.GetConfig(config);
        }

        public int[] ReadButtonPresses()
        {
            int[] buttonIds;

            if (KeyPadState.CommandButton.WasPressed())
            {
                buttonIds = new int[] { KeyPadState.CommandButton.ButtonId };
            }
            else
            {
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
            var buttonColors = KeyPadState.KeypadColorState();       
            LedController.SetLed(1, buttonColors[0]);
            Thread.Sleep(2);
            LedController.SetLed(2, buttonColors[1]);
            Thread.Sleep(2);
            LedController.SetLed(3, buttonColors[2]);
            Thread.Sleep(2);
            LedController.SetLed(4, buttonColors[3]);
            Thread.Sleep(2);
            LedController.SetLed(5, buttonColors[4]);
            Thread.Sleep(2);
            LedController.SetLed(6, buttonColors[5]);
            Thread.Sleep(2);
            LedController.SetLed(7, buttonColors[6]);
            Thread.Sleep(2);
            LedController.SetLed(8, buttonColors[7]);
            Thread.Sleep(2);
            
        }
    }
}
