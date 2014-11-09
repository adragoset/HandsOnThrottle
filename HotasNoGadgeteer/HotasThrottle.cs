using System;
using GHI.Usb.Client;
using Microsoft.SPOT.Hardware.UsbClient;
using Devices;



namespace HotasNoGadgeteer
{
    public class HotasThrottle : RawDevice
    {
        static ushort myVID = 0x0335;
        static ushort myPID = 0x0014;
        static ushort myDeviceVersion = 0x100;
        static ushort myDeviceMaxPower = 500; // in milli amps
        static string companyName = "Your Mom";
        static string productName = "HOTAS Throttle";
        static string myDeviceSerialNumber = "0";

        private int TrackBallWriteEndpoint { get; set; }
        private int GamePad1WriteEndpoint { get; set; }
        private int GamePad2WriteEndpoint { get; set; }

        private RawStream TrackBallStream { get; set; }
        private RawStream GamePad1Stream { get; set; }
        private RawStream GamePad2Stream { get; set; }


        public HotasThrottle()
            : this(myVID, myPID, myDeviceVersion, myDeviceMaxPower, companyName, productName, myDeviceSerialNumber)
        {
        }
        public HotasThrottle(ushort myVID, ushort myPID, ushort myDeviceVersion, ushort myDeviceMaxPower, string companyName, string productName, string myDeviceSerialNumber) :
            base(myVID, myPID, myDeviceVersion, myDeviceMaxPower, companyName, productName, myDeviceSerialNumber)
        {

            TrackBallWriteEndpoint = this.ReserveNewEndpoint();
            GamePad1WriteEndpoint = this.ReserveNewEndpoint();
            GamePad2WriteEndpoint = this.ReserveNewEndpoint();

            InitializeTrackBall();
            InitializeGamePad1();
            InitializeGamePad2();
        }

        private void InitializeTrackBall()
        {
            //Configure TrackBall Endpoint
            Configuration.Endpoint[] trackBallEndPoints = new Configuration.Endpoint[1];
            Configuration.Endpoint trackBallEndpoint1 = new Configuration.Endpoint((byte)TrackBallWriteEndpoint, (byte)131);
            trackBallEndpoint1.wMaxPacketSize = (ushort)8;
            trackBallEndpoint1.bInterval = (byte)5;
            trackBallEndPoints[0] = trackBallEndpoint1;

            //Configure TrackBall Interface
            var TrackBallInterface = new Configuration.UsbInterface((byte)0, trackBallEndPoints);
            TrackBallInterface.bInterfaceClass = (byte)3;
            TrackBallInterface.bInterfaceSubClass = (byte)0;
            TrackBallInterface.bInterfaceProtocol = (byte)0;

            TrackBallInterface.classDescriptors = new Configuration.ClassDescriptor[1] { 
                new Configuration.ClassDescriptor((byte)33, GetHidClassDescriptorPayload((byte)BlackberryTrackBall.GetHidReportDescriptorPayload().Length)) 
            };

            var genericTrackBallDescriptor = new Configuration.GenericDescriptor((byte)129, (ushort)8704, BlackberryTrackBall.GetHidReportDescriptorPayload());
            //Add members
            byte Mnum = this.AddInterface(TrackBallInterface, "HOTATrackball");
            this.AddDescriptor((Configuration.Descriptor)genericTrackBallDescriptor);
            genericTrackBallDescriptor.wIndex = (ushort)Mnum;

            //create the TrackBall stream;
            TrackBallStream = this.CreateStream(TrackBallWriteEndpoint, RawDevice.RawStream.NullEndpoint);
        }

        private void InitializeGamePad1()
        {
            //Configure GamePad1 Endpoint
            Configuration.Endpoint[] gamePad1EndPoints = new Configuration.Endpoint[1];
            Configuration.Endpoint gamePad1Endpoint1 = new Configuration.Endpoint((byte)GamePad1WriteEndpoint, (byte)131);
            gamePad1Endpoint1.wMaxPacketSize = (ushort)16;
            gamePad1Endpoint1.bInterval = (byte)10;
            gamePad1EndPoints[0] = gamePad1Endpoint1;

            //Configure GamePad1 Interface
            Configuration.UsbInterface gamePad1Interface = new Configuration.UsbInterface((byte)1, gamePad1EndPoints);
            gamePad1Interface.bInterfaceClass = (byte)3;
            gamePad1Interface.bInterfaceSubClass = (byte)0;
            gamePad1Interface.bInterfaceProtocol = (byte)0;

            gamePad1Interface.classDescriptors = new Configuration.ClassDescriptor[1] { 
                new Configuration.ClassDescriptor((byte)33, GetHidClassDescriptorPayload((byte)GamePad1.GetHidReportDescriptorPayload().Length)) 
            };

            Configuration.GenericDescriptor genericGamePad1Descriptor = new Configuration.GenericDescriptor((byte)129, (ushort)8704, GamePad1.GetHidReportDescriptorPayload());
            //Add Members
            byte Jnum = this.AddInterface(gamePad1Interface, "Throttle");
            this.AddDescriptor((Configuration.Descriptor)genericGamePad1Descriptor);
            genericGamePad1Descriptor.wIndex = (ushort)Jnum;

            GamePad1Stream = this.CreateStream(GamePad1WriteEndpoint, RawDevice.RawStream.NullEndpoint);
        }

        private void InitializeGamePad2()
        {
            //Configure GamePad1 Endpoint
            Configuration.Endpoint[] gamePad2EndPoints = new Configuration.Endpoint[1];
            Configuration.Endpoint gamePad2Endpoint1 = new Configuration.Endpoint((byte)GamePad2WriteEndpoint, (byte)131);
            gamePad2Endpoint1.wMaxPacketSize = (ushort)16;
            gamePad2Endpoint1.bInterval = (byte)10;
            gamePad2EndPoints[0] = gamePad2Endpoint1;

            //Configure GamePad1 Interface
            Configuration.UsbInterface gamePad2Interface = new Configuration.UsbInterface((byte)2, gamePad2EndPoints);
            gamePad2Interface.bInterfaceClass = (byte)3;
            gamePad2Interface.bInterfaceSubClass = (byte)0;
            gamePad2Interface.bInterfaceProtocol = (byte)0;

            gamePad2Interface.classDescriptors = new Configuration.ClassDescriptor[1] { 
                new Configuration.ClassDescriptor((byte)33, GetHidClassDescriptorPayload((byte)GamePad2.GetHidReportDescriptorPayload().Length)) 
            };

            Configuration.GenericDescriptor genericGamePad2Descriptor = new Configuration.GenericDescriptor((byte)129, (ushort)8704, GamePad2.GetHidReportDescriptorPayload());
            //Add Members
            byte Jnum = this.AddInterface(gamePad2Interface, "ThrottleKeyPad");
            this.AddDescriptor((Configuration.Descriptor)genericGamePad2Descriptor);
            genericGamePad2Descriptor.wIndex = (ushort)Jnum;

            GamePad2Stream = this.CreateStream(GamePad2WriteEndpoint, RawDevice.RawStream.NullEndpoint);
        }

        public static byte[] GetHidClassDescriptorPayload(byte ReportPayLoadLength)
        {
            return new byte[] 
            {
                0x01, 0x01,     // bcdHID (v1.01)
                0x00,           // bCountryCode
                0x01,           // bNumDescriptors
                0x22,           // bDescriptorType (report)
                ReportPayLoadLength, 0x00      // wDescriptorLength (report descriptor size in bytes)
            };
        }

        public void WriteGamePad1Report(byte[] report)
        {
            if (GamePad1Stream.CanWrite)
            {
                GamePad1Stream.Write(report);
            }
        }

        public void WriteGamePad2Report(byte[] report)
        {
            if (GamePad2Stream.CanWrite)
            {
                GamePad2Stream.Write(report);
            }
        }

        public void WriteTrackBallReport(byte[] report)
        {
            if (TrackBallStream.CanWrite)
            {
                TrackBallStream.Write(report);
            }
        }
    }

}

