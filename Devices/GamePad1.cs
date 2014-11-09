using System;
using Microsoft.SPOT;
using Core;
using FrameWork;
using HardWare;

namespace Devices
{
    public class GamePad1 : UsbHidDevice
    {
        private ADS1115 adc;

        private MomentaryButton[] buttonInputs;

        private MomentaryButton hat_up;

        private MomentaryButton hat_down;

        private MomentaryButton hat_left;

        private MomentaryButton hat_right;

        public GamePad1(ADS1115 adc, InterruptInput[] buttonInputs)
        {
            if (buttonInputs.Length != 36)
            {
                throw new ArgumentException("You must provide 36 inputs for this device");
            }

            this.adc = adc;

            hat_up = new MomentaryButton(1, buttonInputs[0]);

            hat_down = new MomentaryButton(2, buttonInputs[1]);

            hat_left = new MomentaryButton(3, buttonInputs[2]);

            hat_right = new MomentaryButton(4, buttonInputs[3]);

            this.buttonInputs = new MomentaryButton[32];

            for (int index = 4; index < buttonInputs.Length; index++)
            {
                this.buttonInputs[index - 4] = new MomentaryButton(index - 3, buttonInputs[index]);
            }

        }

        public byte[] GetDeviceState()
        {
            var buttonReport = GetBytesFromButtons();
            var hatReport = GetBytesFromHat();
            var leftThrottleReport = GetBytesFromLeftThrottle();
            var rightThrottleReport = GetBytesFromRightThrottle();
            var xReport = GetBytesFromX();
            var yReport = GetBytesFromY();

            var result = new byte[14];

            result[0] = buttonReport[0];
            result[1] = buttonReport[1];
            result[2] = buttonReport[2];
            result[3] = buttonReport[3];
            result[4] = hatReport[0];
            result[5] = hatReport[1];
            result[6] = leftThrottleReport[0];
            result[7] = leftThrottleReport[1];
            result[8] = rightThrottleReport[0];
            result[9] = rightThrottleReport[1];
            result[10] = xReport[0];
            result[11] = xReport[1];
            result[12] = yReport[0];
            result[13] = yReport[1];

            return result;
        }

        private byte[] GetBytesFromButtons()
        {
            var result = new byte[] { 0, 0, 0, 0 };

            foreach (var button in buttonInputs)
            {
                if (button.WasPressed)
                {
                    var key = button.ButtonId;

                    if (key <= 8)
                    {
                        ByteHelper.FlipBitInByte(result[0], key);
                    }
                    else if (key <= 16)
                    {
                        ByteHelper.FlipBitInByte(result[1], key);
                    }
                    else if (key <= 24)
                    {
                        ByteHelper.FlipBitInByte(result[2], key);
                    }
                    else if (key <= 32)
                    {
                        ByteHelper.FlipBitInByte(result[3], key);
                    }
                    else
                    {
                        throw new ArgumentException("The key index is out of range");
                    }
                }
            }

            return result;
        }

        private byte[] GetBytesFromHat()
        {
            var result = new byte[] { 0, 0 };

            if (hat_up.WasPressed)
            {
                result[0] = ByteHelper.FlipBitInByte(result[0], hat_up.ButtonId);
            }

            if (hat_down.WasPressed)
            {
                result[0] = ByteHelper.FlipBitInByte(result[5], hat_down.ButtonId);
            }

            if (hat_left.WasPressed)
            {
                result[0] = ByteHelper.FlipBitInByte(result[5], hat_left.ButtonId);
            }

            if (hat_right.WasPressed)
            {
                result[0] = ByteHelper.FlipBitInByte(result[5], hat_right.ButtonId);
            }

            return result;
        }

        private byte[] GetBytesFromLeftThrottle()
        {
            short reading = 0;

            lock (adc)
            {
                reading = adc.ReadADC(ADS1115.Resolution.SPS475, ADS1115.Input.Input_1);
            }

            return ByteHelper.FromShort(reading);
        }

        private byte[] GetBytesFromRightThrottle()
        {
            short reading = 0;

            lock (adc)
            {
                reading = adc.ReadADC(ADS1115.Resolution.SPS475, ADS1115.Input.Input_2);
            }

            return ByteHelper.FromShort(reading);
        }

        private byte[] GetBytesFromX()
        {
            short reading = 0;

            lock (adc)
            {
                reading = adc.ReadADC(ADS1115.Resolution.SPS475, ADS1115.Input.Input_3);
            }

            return ByteHelper.FromShort(reading);
        }

        private byte[] GetBytesFromY()
        {
            short reading = 0;

            lock (adc)
            {
                reading = adc.ReadADC(ADS1115.Resolution.SPS475, ADS1115.Input.Input_4);
            }

            return ByteHelper.FromShort(reading);
        }

        public static byte[] GetHidReportDescriptorPayload()
        {
            return new byte[]
            {
                 0x05, 0x01,                    // USAGE_PAGE (Generic Desktop)
                 0x09, 0x05,                    // USAGE (Joystick)
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

                 0x75, 0x0c,                    //   REPORT_SIZE (12)
                 0x95, 0x01,                    //   REPORT_COUNT (1)
                 0x81, 0x03,                    //   INPUT (cnst,Var,Abs)

                 0x05, 0x01,                    //   USAGE_PAGE (Generic Desktop)
                 0x09, 0x30,                    //   USAGE(X)
                 0x09, 0x31,                    //   USAGE(Y)
                 0x09, 0x37,                    //   USAGE(Slider)
                 0x09, 0x36,                    //   USAGE(Dial)
                 0x16, 0x01, 0x80,              //   LOGICAL MINIMUM(-32767)
                 0x26, 0xff, 0x7f,              //   LOGICAL MAXIMUM(32767)
                 0x75, 0x10,                    //   REPORT_SIZE (16)
                 0x95, 0x04,                    //   REPORT_COUNT (4)
                 0x81, 0x02,                    //   INPUT (Data,Var,Abs)

                 0xc0,                          // END COLLECTION
                 0xc0                           // END COLLECTION
            };
        }
    }
}
