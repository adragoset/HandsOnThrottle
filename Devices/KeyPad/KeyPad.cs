using System;
using Microsoft.SPOT;
using FrameWork;
using Core;
using Core.KeyPadState;
using System.Threading;
using Microsoft.SPOT.Hardware;
using HardWare;

namespace Devices
{
    public class KeyPad : InputArray
    {
        private static object State_Lock = new object();

        private int PhysicalButtonCount;
        //private int CommandButton;

        private RGBLedSeriesController LedController;

        private StatefulRGBKeypad KeyPadState;

        public KeyPad(RGBLedSeriesController ledController, InterruptInput[] KeyPadIoInputs, string config)
            : base(KeyPadIoInputs)
        {
            LedController = ledController;

            PhysicalButtonCount = KeyPadIoInputs.Length;

            KeyPadState = BuildKeyPad(config);

            KeyPadState.HomePage();
        }

        private StatefulRGBKeypad BuildKeyPad(string config)
        {
            KeyPadStateData data = getData(config);
            StatefulButton[] buttons = getButtons(data.ButtonStates);
            StatefulButton commandButton = new StatefulButton(1, Inputs[0], LedController, 1, data.CommandButton, "Command_B");
            InitializeLedController(commandButton, buttons);
            return new StatefulRGBKeypad(buttons, commandButton);
        }

        private void InitializeLedController(StatefulButton commandButton, StatefulButton[] buttons)
        {
            var color = ColorCode.GetColorCode(commandButton.GetCurrentColor());

            LedController.UpdateLed(new Led(commandButton.LEDIndex, color.R, color.G, color.B));

            foreach (var button in buttons)
            {
                color = ColorCode.GetColorCode(button.GetCurrentColor());
                LedController.UpdateLed(new Led(button.LEDIndex, color.R, color.G, color.B));
            }


        }

        private StatefulButton[] getButtons(ButtonState[] buttonStates)
        {
            var buttons = new StatefulButton[7];

            buttons[0] = new StatefulButton(2, Inputs[1], LedController, 2, buttonStates[0], "Keypad_B_2");
            buttons[1] = new StatefulButton(3, Inputs[2], LedController, 3, buttonStates[1], "Keypad_B_3");
            buttons[2] = new StatefulButton(4, Inputs[3], LedController, 4, buttonStates[2], "Keypad_B_4");
            buttons[3] = new StatefulButton(5, Inputs[4], LedController, 5, buttonStates[3], "Keypad_B_5");
            buttons[4] = new StatefulButton(6, Inputs[5], LedController, 6, buttonStates[4], "Keypad_B_6");
            buttons[5] = new StatefulButton(7, Inputs[6], LedController, 7, buttonStates[5], "Keypad_B_7");
            buttons[6] = new StatefulButton(8, Inputs[7], LedController, 8, buttonStates[6], "Keypad_B_8");


            return buttons;
        }

        private KeyPadStateData getData(string config)
        {
            return KeyPadConfigurationParser.GetConfig(config);
        }

        public int[] ReadButtonPresses()
        {
                lock (KeyPadState)
                {
                    return KeyPadState.PressedButtons();
                }
        }

    }
}
