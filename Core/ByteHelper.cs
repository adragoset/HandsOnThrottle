using System;
using Microsoft.SPOT;

namespace Core
{
    public class ByteHelper
    {
        public static byte[] GetBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        public static byte FlipBitInByte(byte input, int index)
        {
            var position = index % 8;

            if (position == 0)
            {
                position = 7;
            }
            else
            {
                position = position - 1;
            }

            var temp = input;
            byte mask = (byte)(1 << position);
            temp ^= mask;
            return temp;
        }

        public static byte[] FromShort(short number)
        {
            var byte2 = (byte)(number >> 8);
            var byte1 = (byte)(number & 255);

            return new byte[] { byte1, byte2 };
        }

    }
}
