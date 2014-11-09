using FrameWork;
using Microsoft.SPOT.Hardware;
using System;


namespace FrameWork
{


    public class Socket
    {
        public static readonly Cpu.Pin UnnumberedPin = (Cpu.Pin)int.MinValue;
        /// An enumeration of socket pins.
        /// </summary>
        public enum Pin
        {
            /// <summary>
            /// Not applicable
            /// </summary>
            None = 0,
            /// <summary>
            /// Socket pin 1
            /// </summary>
            One = 1,
            /// <summary>
            /// Socket pin 2
            /// </summary>
            Two = 2,
            /// <summary>
            /// Socket pin 3
            /// </summary>
            Three = 3,
            /// <summary>
            /// Socket pin 4
            /// </summary>
            Four = 4,
            /// <summary>
            /// Socket pin 5
            /// </summary>
            Five = 5,
            /// <summary>
            /// Socket pin 6
            /// </summary>
            Six = 6,
            /// <summary>
            /// Socket pin 7
            /// </summary>
            Seven = 7,
            /// <summary>
            /// Socket pin 8
            /// </summary>
            Eight = 8,
            /// <summary>
            /// Socket pin 9
            /// </summary>
            Nine = 9,
            /// <summary>
            /// Socket pin 10
            /// </summary>
            Ten = 10
        }

        /// <summary>
        /// The <see cref="SI.AnalogInput" /> provider for this socket.
        /// </summary>
        public AnalogInputIndirector AnalogInputIndirector;
        /// <summary>
        /// The <see cref="SI.AnalogOutput" /> provider for this socket.
        /// </summary>
        public AnalogOutputIndirector AnalogOutputIndirector;
        /// <summary>
        /// The <see cref="SI.DigitalInput" /> provider for this socket.
        /// </summary>
        public DigitalInputIndirector DigitalInputIndirector;
        /// <summary>
        /// The <see cref="SI.DigitalIO" /> provider for this socket.
        /// </summary>
        public DigitalOIndirector DigitalIOIndirector;
        /// <summary>
        /// The <see cref="SI.DigitalOutput" /> provider for this socket.
        /// </summary>
        public DigitalOutputIndirector DigitalOutputIndirector;
        /// <summary>
        /// The <see cref="SI.I2CBus" /> provider for this socket.
        /// </summary>
        public I2CBusIndirector I2CBusIndirector;
        /// <summary>
        /// The <see cref="SI.InterruptInput" /> provider for this socket.
        /// </summary>
        public InterruptInputIndirector InterruptIndirector;
        /// <summary>
        /// The <see cref="SI.PwmOutput" /> provider for this socket.
        /// </summary>
        public PwmOutputIndirector PwmOutputIndirector;

        public int SocketNumber { get; set; }

        public Cpu.Pin[] CpuPins { get; private set; }

        public Socket(int i)
        {
            this.SocketNumber = i;

            this.CpuPins = new Cpu.Pin[10];
        }

        public Cpu.AnalogChannel AnalogInput3 { get; set; }

        public Cpu.AnalogChannel AnalogInput4 { get; set; }

        public Cpu.AnalogChannel AnalogInput5 { get; set; }

        public Cpu.PWMChannel PWM9 { get; set; }

        public Cpu.PWMChannel PWM8 { get; set; }

        public Cpu.PWMChannel PWM7 { get; set; }
    }
}
