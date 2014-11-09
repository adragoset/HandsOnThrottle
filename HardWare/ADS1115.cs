using System;
using Microsoft.SPOT;
using System.Threading;
using FrameWork;

namespace HardWare
{
    public class ADS1115
    {
        public static ushort I2C_ADDRESS = 0x49;
        private Input CurrentInput;
        private Gain Pga;
        private Resolution PollingResolution;
        private ComparatorMode CompMode;
        private ComparatorPolarity CompPol;
        private ComparatorLatch CompLatch;
        private ComparatorQueue CompQue;



        public enum Register
        {
            conversion = 0x00,
            Config = 0x01,
            Low_Thresh = 0x02,
            High_Thresh = 0x03
        }

        public enum OperationalStatus { NoEffext = 0x0, SingleConversion = 0x80 }


        public enum Mode { ContinuousConversion = 0x1, Sleep = 0x0 }


        public enum Input { Input_1 = 0x40, Input_2 = 0x50, Input_3 = 0x60, Input_4 = 0x70 }


        public enum Gain { v0 = 0x0, v1 = 0x2, v2 = 0x4, v3 = 0x6, v4 = 0x8, v5 = 0xA, v6 = 0xC, v7 = 0xE }


        public enum Resolution { SPS8 = 0x0, SPS16 = 0x20, SPS32 = 0x40, SPS64 = 0x60, SPS128 = 0x80, SPS250 = 0xA0, SPS475 = 0xC0, SPS860 = 0xE0 }


        public enum ComparatorMode { Traditional = 0x0, Window = 0x10 }


        public enum ComparatorPolarity { ActiveLow = 0x0, ActiveHigh = 0x8 }


        public enum ComparatorLatch { None = 0x0, Latching = 0x4 }


        public enum ComparatorQueue { Disable = 0x3, Assert1 = 0x0, Assert2 = 0x1, Assert4 = 0x2 }

        public I2CBus Bus { get; private set; }

        public OperationalStatus OpStatus { get; private set; }
        public Mode PollingMode { get; set; }

        public ADS1115()
        {
        }

        public ADS1115(ushort address)
            : base()
        {

            if (address == 0x4A || address == 0x49 || address == 0x48 || address == 0x96)
            {
                Bus = I2CBusFactory.Create(address, 400);
            }
            else
            {
                throw new ArgumentException("The provided address is out of range.");
            }

        }

        public void Configure(OperationalStatus os, Mode mode, Input input, Gain gain, Resolution resolution, ComparatorMode compMode, ComparatorPolarity compPol, ComparatorLatch compLat, ComparatorQueue compQue)
        {
            this.OpStatus = os;
            this.PollingMode = mode;
            this.CurrentInput = input;
            this.Pga = gain;
            this.PollingResolution = resolution;
            this.CompMode = compMode;
            this.CompPol = compPol;
            this.CompLatch = compLat;
            this.CompQue = compQue;

        }

        public byte[] BuildConfiguration()
        {
            byte byte1 = (byte)((byte)this.OpStatus + (byte)this.CurrentInput + (byte)this.Pga + (byte)this.PollingMode);
            byte byte2 = (byte)((byte)this.PollingResolution + (byte)this.CompMode + (byte)this.CompPol + (byte)this.CompLatch + (byte)this.CompQue);

            return new byte[] { byte1, byte2 };
        }

        /// <summary>
        /// Reads the specified ADC channel and returns value
        /// </summary>
        /// <param name="inputNumber"></param>
        /// <returns></returns>
        public Int16 ReadADC(Resolution resolution, Input input)
        {
            lock (Bus)
            {
                this.CurrentInput = input;

                this.PollingResolution = resolution;

                byte[] config = BuildConfiguration();

                // create write buffer (we need three bytes)
                byte[] registerADS1115 = new byte[3] { 1, config[0], config[1] };
                // Create write transaction

                // excecute ADS1115 setup

                Bus.Write(registerADS1115);

                // Wait for conversion
                Thread.Sleep(1);

                // Set to conversion register
                registerADS1115 = new byte[1] { 0 };

                // Execute set to conversion

                Bus.Write(registerADS1115);


                // create read buffer
                byte[] readADS1115 = new byte[2];
                // Read ADC values

                Bus.Read(registerADS1115);


                // return values
                return (Int16)(readADS1115[0] * 256 + readADS1115[1]);
            }
        }
    }
}
