using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Devices;
using FrameWork;
using Core;


namespace Devices
{
    public class BlackberryTrackBall : UsbHidDevice
    {

        private InterruptPort Pin_Up { get; set; }

        private InterruptPort Pin_Down { get; set; }

        private InterruptPort Pin_Left { get; set; }

        private InterruptPort Pin_Right { get; set; }

        private MomentaryButton Btn_Push { get; set; }

        private MomentaryButton Center_Push { get; set; }

        private MomentaryButton Up_Push { get; set; }

        private MomentaryButton Down_Push { get; set; }

        private RGBLedOutput Led { get; set; }

        private TrackBallState TrackState { get; set; }

        ExtendedTimer Tick { get; set; }

        public BlackberryTrackBall(InterruptPort[] trackBallInputs, InterruptInput[] buttonInputs, PWM[] ledOutput)
            : base()
        {

            if (trackBallInputs.Length != 4)
            {
                throw new ArgumentException("4 interrupts as trackball inputs are required.");
            }

            Pin_Up = trackBallInputs[0];
            Pin_Down = trackBallInputs[1];
            Pin_Left = trackBallInputs[2];
            Pin_Right = trackBallInputs[3];

            if (buttonInputs.Length != 3)
            {
                throw new ArgumentException("4 interrupts for buttons are required.");
            }

            Btn_Push = new MomentaryButton(1, buttonInputs[0]);
            Center_Push = new MomentaryButton(2, buttonInputs[1]);
            Up_Push = new MomentaryButton(3, buttonInputs[2]);
           

            if (ledOutput.Length != 3)
            {
                throw new ArgumentException("3 pwm outputs for led are required.");
            }

            Led = new RGBLedOutput(ledOutput[0], ledOutput[1], ledOutput[2], false);

            Led.SetColor(Devices.Color.Orange);
            TrackState = new TrackBallState();

            BindEventHandlers();
        }

        public byte[] GetDeviceState()
        {
            byte[] trackBallData = ConvertTrackBallAxisDataToBytes(TrackState.GetXYDelta());
            TrackState.Clear();

            byte buttonData = 0;

            if (Btn_Push.WasPressed)
            {
                buttonData = ByteHelper.FlipBitInByte(buttonData, 1);
            }

            if (Center_Push.WasPressed)
            {

                buttonData = ByteHelper.FlipBitInByte(buttonData, 2);
            }

            if (Up_Push.WasPressed)
            {
                buttonData = ByteHelper.FlipBitInByte(buttonData, 3);
            }

            return new byte[] { buttonData, trackBallData[0], trackBallData[1] };
        }

        private void BindEventHandlers()
        {
            Pin_Up.OnInterrupt += Pin_Changed_Up;
            Pin_Down.OnInterrupt += Pin_Changed_Down;
            Pin_Left.OnInterrupt += Pin_Changed_Left;
            Pin_Right.OnInterrupt += Pin_Changed_Right;
        }

        private void Pin_Changed_Up(uint data1, uint data2, DateTime time)
        {
            TrackState.Increment_Up();
        }

        private void Pin_Changed_Down(uint data1, uint data2, DateTime time)
        {
            TrackState.Increment_Down();
        }

        private void Pin_Changed_Left(uint data1, uint data2, DateTime time)
        {
            TrackState.Increment_Left();
        }

        private void Pin_Changed_Right(uint data1, uint data2, DateTime time)
        {
            TrackState.Increment_Right();
        }

        private static byte[] ConvertTrackBallAxisDataToBytes(DeltaCoord deltaCoord)
        {
            var XBytes = (byte)(deltaCoord.X);
            var YBytes = (byte)(deltaCoord.Y);

            return new byte[] { XBytes, YBytes };
        }

        public static byte[] GetHidReportDescriptorPayload()
        {
            return new byte[]
            {
                    0x05, 0x01,        // USAGE_PAGE (Generic Desktop)
                    0x09, 0x02,        // USAGE (Mouse)
                    0xa1, 0x01,        // COLLECTION (Application)
                    0x09, 0x02,        //   USAGE (Mouse)
                    0xa1, 0x02,        //   COLLECTION (Logical)
                    0x09, 0x01,        //     USAGE (Pointer)
                    0xa1, 0x00,        //     COLLECTION (Physical)
                                       // ------------------------------  Buttons
                    0x05, 0x09,        //       USAGE_PAGE (Button)      
                    0x19, 0x01,        //       USAGE_MINIMUM (Button 1)
                    0x29, 0x04,        //       USAGE_MAXIMUM (Button 4)
                    0x15, 0x00,        //       LOGICAL_MINIMUM (0)
                    0x25, 0x01,        //       LOGICAL_MAXIMUM (1)
                    0x75, 0x01,        //       REPORT_SIZE (1)
                    0x95, 0x04,        //       REPORT_COUNT (4)
                    0x81, 0x02,        //       INPUT (Data,Var,Abs)

                    
                                        // ------------------------------  Padding
                    0x75, 0x04,        //       REPORT_SIZE (4)
                    0x95, 0x01,        //       REPORT_COUNT (1)
                    0x81, 0x03,        //       INPUT (Cnst,Var,Abs)
    

                                       // ------------------------------  X position
                    0x05, 0x01,        //       USAGE_PAGE (Generic Desktop)
                    0x09, 0x30,        //       USAGE (X)
                    0x35, 0x81,        //       PHYSICAL_MINIMUM (-127)
                    0x45, 0x7f,        //       PHYSICAL_MAXIMUM (127)
                    0x15, 0xf0,        //       LOGICAL_MINIMUM (-16)
                    0x25, 0x10,        //       LOGICAL_MAXIMUM (16)
                    0x75, 0x04,        //       REPORT_SIZE (4)
                    0x95, 0x01,        //       REPORT_COUNT (1)
                    0x81, 0x06,        //       INPUT (Data,Var,Rel)

                    
                                        // ------------------------------  Padding
                    0x75, 0x04,        //       REPORT_SIZE (4)
                    0x95, 0x01,        //       REPORT_COUNT (1)
                    0x81, 0x03,        //       INPUT (Cnst,Var,Abs)

                                       // ------------------------------  Y position
                    0x05, 0x01,        //       USAGE_PAGE (Generic Desktop)
                    0x09, 0x31,        //       USAGE (Y)
                    0x35, 0x81,        //       PHYSICAL_MINIMUM (-127)
                    0x45, 0x7f,        //       PHYSICAL_MAXIMUM (127)
                    0x15, 0xf0,        //       LOGICAL_MINIMUM (-16)
                    0x25, 0x10,        //       LOGICAL_MAXIMUM (16)
                    0x75, 0x04,        //       REPORT_SIZE (4)
                    0x95, 0x01,        //       REPORT_COUNT (1)
                    0x81, 0x06,        //       INPUT (Data,Var,Rel)

                    
                                        // ------------------------------  Padding
                    0x75, 0x04,        //       REPORT_SIZE (4)
                    0x95, 0x01,        //       REPORT_COUNT (1)
                    0x81, 0x03,        //       INPUT (Cnst,Var,Abs)
 
                    0xc0,              //     END_COLLECTION
                    0xc0,              //   END_COLLECTION
                    0xc0     
            };
        }
    }

    public class DeltaCoord
    {
        public short X { get; set; }

        public short Y { get; set; }
    }

    public class TrackBallState
    {
        private static object State_Lock = new object();

        private int Up_Count { get; set; }



        private int Down_Count { get; set; }



        private int Left_Count { get; set; }



        private int Right_Count { get; set; }



        public TrackBallState()
        {

            Up_Count = 0;

            Down_Count = 0;

            Left_Count = 0;

            Right_Count = 0;

        }

        public void Increment_Up()
        {
            lock (State_Lock)
            {
                Up_Count++;
            }
        }

        public void Increment_Down()
        {
            lock (State_Lock)
            {
                Down_Count++;
            }
        }

        public void Increment_Left()
        {
            lock (State_Lock)
            {
                Left_Count++;
            }
        }

        public void Increment_Right()
        {
            lock (State_Lock)
            {
                Right_Count++;
            }
        }

        public DeltaCoord GetXYDelta()
        {
            lock (State_Lock)
            {
                var resultX = Right_Count - Left_Count;
                var resultY = Up_Count - Down_Count;



                var result = new DeltaCoord() { X = (short)(resultX), Y = (short)(resultY) };

                if (result.X > 16)
                {
                    result.X = 16;
                }
                else if (result.X < -16)
                {
                    result.X = -16;
                }

                if (result.Y > 16)
                {
                    result.Y = 16;
                }
                else if (result.Y < -16)
                {
                    result.Y = -16;
                }

                return result;
            }
        }


        public void Clear()
        {
            lock (State_Lock)
            {
                Up_Count = 0;
                Down_Count = 0;
                Left_Count = 0;
                Right_Count = 0;
            }
        }

    }
}