using System;
using Microsoft.SPOT;
using FrameWork;

namespace HardWare
{
    public class RGBLedSeriesController
    {
        public static ushort I2C_ADDRESS = 0x09;

        public I2CBus Bus { get; private set; }

        public RGBLedSeriesController()
        {
            Bus = I2CBusFactory.Create(I2C_ADDRESS, 400);
        }

        public void UpdateLed(Led led) {
            lock (Bus)
            {
                Bus.Write(led.GetData());
            }            
        }
    }

    public class Led
    {
        public uint Red { get; private set; }

        public uint Green { get; private set; }

        public uint Blue { get; private set; }

        public uint Id { get; private set; }

        public Led(uint id, uint red, uint green, uint blue)
        {
            this.Id = id;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public byte[] GetData() {
            return new byte[4] { (byte)Id, (byte)Red, (byte)Green, (byte)Blue };
        }
    }
}
