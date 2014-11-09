using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHI.Pins;

namespace G120
{

    public class G120II
    {
        public static Cpu.Pin[] Socket1 = new Cpu.Pin[] { 
            GHI.Pins.G120.P0_5,
            GHI.Pins.G120.P1_14,
            GHI.Pins.G120.P1_16,
            GHI.Pins.G120.P1_17,
            GHI.Pins.G120.P0_9,
            GHI.Pins.G120.P0_8,
            GHI.Pins.G120.P0_7,
        };

        public static Cpu.Pin[] I2cSocket = new Cpu.Pin[] { 
            GHI.Pins.G120.P0_25,
            Cpu.Pin.GPIO_NONE,
            Cpu.Pin.GPIO_NONE,
            GHI.Pins.G120.P1_0,
            Cpu.Pin.GPIO_NONE,
            GHI.Pins.G120.P0_27,
            GHI.Pins.G120.P0_28
        };

        public static Cpu.Pin[] TBSocket1 = new Cpu.Pin[] { 
            GHI.Pins.G120.P0_12,
            GHI.Pins.G120.P0_24,
            GHI.Pins.G120.P0_23,
            Cpu.Pin.GPIO_NONE,
            GHI.Pins.G120.P3_26,
            GHI.Pins.G120.P3_25,
            GHI.Pins.G120.P3_24     
        };

        public static Cpu.Pin[] TBSocket2 = new Cpu.Pin[] { 
            GHI.Pins.G120.P0_26,
            Cpu.Pin.GPIO_NONE,
            Cpu.Pin.GPIO_NONE,
            Cpu.Pin.GPIO_NONE,
            GHI.Pins.G120.P1_3,
            GHI.Pins.G120.P1_6,
            GHI.Pins.G120.P1_11,
        };

        public static Cpu.Pin[] MainBoardIoSocket1 = new Cpu.Pin[] { 
            GHI.Pins.G120.P2_12,
            GHI.Pins.G120.P2_6,
            GHI.Pins.G120.P2_7,
            GHI.Pins.G120.P2_8,
            GHI.Pins.G120.P2_9,
            GHI.Pins.G120.P2_3,
            GHI.Pins.G120.P2_5,
        };

        public static Cpu.Pin[] MainBoardIoSocket2 = new Cpu.Pin[] { 
            GHI.Pins.G120.P2_13,
            GHI.Pins.G120.P1_26,
            GHI.Pins.G120.P1_27,
            GHI.Pins.G120.P1_28,
            GHI.Pins.G120.P1_29,
            GHI.Pins.G120.P2_4,
            GHI.Pins.G120.P2_2,
        };



    }
}
