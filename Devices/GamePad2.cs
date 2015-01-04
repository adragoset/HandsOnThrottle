using System;
using Microsoft.SPOT;
using FrameWork;
using HardWare;
using Core;

namespace Devices
{
    public class GamePad2 : UsbHidDevice
    {
        private KeyPad keyPad;

        private MomentaryButton hat_up;

        private MomentaryButton hat_down;

        private MomentaryButton hat_left;

        private MomentaryButton hat_right;

        public GamePad2(KeyPad keypad, InterruptInput[] hatSwitchInputs)
        {
            this.keyPad = keypad;

            if (hatSwitchInputs.Length != 4)
            {
                throw new ArgumentException("You must provide 4 inputs for this device");
            }

            hat_up = new MomentaryButton(1, hatSwitchInputs[0], "left_throttle_hat_up");

            hat_down = new MomentaryButton(2, hatSwitchInputs[1], "left_throttle_hat_down");

            hat_left = new MomentaryButton(3, hatSwitchInputs[2], "left_throttle_hat_left");

            hat_right = new MomentaryButton(4, hatSwitchInputs[3], "left_throttle_hat_right");
        }

        public byte[] GetDeviceState()
        {
            byte[] result = GetBytesForKeyPad(keyPad.ReadButtonPresses());

            if (hat_up.WasPressed())
            {
                result[5] = ByteHelper.FlipBitInByte(result[5], hat_up.ButtonId + 3);
            }

            if (hat_down.WasPressed())
            {
                result[5] = ByteHelper.FlipBitInByte(result[5], hat_down.ButtonId + 3);
            }

            if (hat_left.WasPressed())
            {
                result[5] = ByteHelper.FlipBitInByte(result[5], hat_left.ButtonId + 3);
            }

            if (hat_right.WasPressed())
            {
                result[5] = ByteHelper.FlipBitInByte(result[5], hat_right.ButtonId + 3);
            }

            return result;
        }

        private static byte[] GetBytesForKeyPad(int[] buttonIds)
        {
            var result = new byte[6] { 0, 0, 0, 0, 0, 0 };

            foreach (var key in buttonIds)
            {
                if (key <= 8)
                {
                    result[0] = ByteHelper.FlipBitInByte(result[0], key);
                }
                else if (key <= 16)
                {
                    result[1] = ByteHelper.FlipBitInByte(result[1], key);
                }
                else if (key <= 24)
                {
                    result[2] = ByteHelper.FlipBitInByte(result[2], key);
                }
                else if (key <= 32)
                {
                   result[3] = ByteHelper.FlipBitInByte(result[3], key);
                }
                else if (key <= 35)
                {
                    result[4] = ByteHelper.FlipBitInByte(result[4], key);
                }
                else
                {
                    throw new ArgumentException("The key index is out of range");
                }
            }

            return result;
        }

        public static byte[] GetHidReportDescriptorPayload()
        {
            return new byte[]
            {
                 0x05, 0x01,                    // USAGE_PAGE (Generic Desktop)
                 0x09, 0x05,                    // USAGE (GamePad)
                 0xa1, 0x01,                    // COLLECTION (Application)
                 0xa1, 0x00,                    // COLLECTION (Physical)

                 0x05, 0x09,                    //   USAGE_PAGE (Button)
                 0x19, 0x01,                    //   USAGE_MINIMUM (Button 1)
                 0x29, 0x10,                    //   USAGE_MAXIMUM (Button 16)
                 0x15, 0x00,                    //   LOGICAL_MINIMUM (0)
                 0x25, 0x01,                    //   LOGICAL_MAXIMUM (1)
                 0x95, 0x10,                    //   REPORT_COUNT (16)
                 0x75, 0x01,                    //   REPORT_SIZE (1)
                 0x81, 0x02,                    //   INPUT (Data,Var,Abs)

                 0x05, 0x09,                    //   USAGE_PAGE (Button)
                 0x19, 0x11,                    //   USAGE_MINIMUM (Button 17)
                 0x29, 0x20,                    //   USAGE_MAXIMUM (Button 32)
                 0x15, 0x00,                    //   LOGICAL_MINIMUM (0)
                 0x25, 0x01,                    //   LOGICAL_MAXIMUM (1)
                 0x95, 0x10,                    //   REPORT_COUNT (16)
                 0x75, 0x01,                    //   REPORT_SIZE (1)
                 0x81, 0x02,                    //   INPUT (Data,Var,Abs)

                 0x05, 0x09,                    //   USAGE_PAGE (Button)
                 0x19, 0x21,                    //   USAGE_MINIMUM (Button 33)
                 0x29, 0x23,                    //   USAGE_MAXIMUM (Button 35)
                 0x15, 0x00,                    //   LOGICAL_MINIMUM (0)
                 0x25, 0x01,                    //   LOGICAL_MAXIMUM (1)
                 0x95, 0x03,                    //   REPORT_COUNT (3)
                 0x75, 0x01,                    //   REPORT_SIZE (1)
                 0x81, 0x02,                    //   INPUT (Data,Var,Abs)

                 0x05, 0x01,                    //   USAGE_PAGE (Generic Desktop)
                 0x09, 0x39,                    //   USAGE (Hat switch)
                 0x15, 0x01,                    //   LOGICAL_MINIMUM (1)
                 0x25, 0x04,                    //   LOGICAL_MAXIMUM (4)
                 0x35, 0x00,                    //   PHYSICAL_MINIMUM (0)
                 0x46, 0x0e, 0x01,              //   PHYSICAL_MAXIMUM (270)
                 0x65, 0x14,                    //   UNIT (Eng Rot:Angular Pos)
                 0x75, 0x04,                    //   REPORT_SIZE (4)
                 0x95, 0x01,                    //   REPORT_COUNT (1)
                 0x81, 0x02,                    //   INPUT (Data,Var,Abs)

                 0x75, 0x09,                    //   REPORT_SIZE (9)
                 0x95, 0x01,                    //   REPORT_COUNT (1)
                 0x81, 0x03,                    //   INPUT (cnst,Var,Abs)

                 0xc0,                          // END COLLECTION
                 0xc0                           // END COLLECTION
            };
        }
    }
}
