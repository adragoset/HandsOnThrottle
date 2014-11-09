using System;
using Microsoft.SPOT;
using G120;
using Microsoft.SPOT.Hardware;
using GHI.Usb.Client;
using Devices;
using HardWare;
using FrameWork;

namespace HotasNoGadgeteer
{
    public class Program
    {
        private BlackberryTrackBall TrackBall;

        private ExtendedTimer TrackBallFrameTimer;

        private HotasThrottle Device;

        private HubAP5 Hub;

        private ADS1115 ADC;

        private AssignableInterrupt[] Interrupts;

        public static void Main()
        {
            Program p = new Program();
            p.InitializeHardWare();
            p.InitializeInterrupts();
            p.InitializeUsbConnection();
            p.InitializeDevices();

            while (true) { }
        }

        private void InitializeHardWare()
        {
            Hub = new HubAP5(G120II.I2cSocket[0]);
            ADC = new ADS1115();
            ADC.Configure(
                ADS1115.OperationalStatus.SingleConversion,
                ADS1115.Mode.ContinuousConversion,
                ADS1115.Input.Input_1, ADS1115.Gain.v1,
                ADS1115.Resolution.SPS860,
                ADS1115.ComparatorMode.Traditional,
                ADS1115.ComparatorPolarity.ActiveHigh,
                ADS1115.ComparatorLatch.None,
                ADS1115.ComparatorQueue.Disable
                );
        }

        private void InitializeInterrupts()
        {
            Interrupts = new AssignableInterrupt[8 * 7];
            var count = 0;
            Socket[] HubAP5YSockets = new Socket[] { Hub.HubSocket1, Hub.HubSocket2, Hub.HubSocket3, Hub.HubSocket4, Hub.HubSocket5, Hub.HubSocket6, Hub.HubSocket7, Hub.HubSocket8 };
            foreach (var socket in HubAP5YSockets)
            {
                InterruptInput input;

                input = InterruptInputFactory.Create(socket, Socket.Pin.Three, GlitchFilterMode.On, Port.ResistorMode.PullUp, InterruptMode.RisingAndFallingEdge);
                Interrupts[count] = new AssignableInterrupt(input);
                count++;

                input = InterruptInputFactory.Create(socket, Socket.Pin.Four, GlitchFilterMode.On, Port.ResistorMode.PullUp, InterruptMode.RisingAndFallingEdge);
                Interrupts[count] = new AssignableInterrupt(input);
                count++;

                input = InterruptInputFactory.Create(socket, Socket.Pin.Five, GlitchFilterMode.On, Port.ResistorMode.PullUp, InterruptMode.RisingAndFallingEdge);
                Interrupts[count] = new AssignableInterrupt(input);
                count++;

                input = InterruptInputFactory.Create(socket, Socket.Pin.Six, GlitchFilterMode.On, Port.ResistorMode.PullUp, InterruptMode.RisingAndFallingEdge);
                Interrupts[count] = new AssignableInterrupt(input);
                count++;

                input = InterruptInputFactory.Create(socket, Socket.Pin.Seven, GlitchFilterMode.On, Port.ResistorMode.PullUp, InterruptMode.RisingAndFallingEdge);
                Interrupts[count] = new AssignableInterrupt(input);
                count++;

                input = InterruptInputFactory.Create(socket, Socket.Pin.Eight, GlitchFilterMode.On, Port.ResistorMode.PullUp, InterruptMode.RisingAndFallingEdge);
                Interrupts[count] = new AssignableInterrupt(input);
                count++;

                input = InterruptInputFactory.Create(socket, Socket.Pin.Nine, GlitchFilterMode.On, Port.ResistorMode.PullUp, InterruptMode.RisingAndFallingEdge);
                Interrupts[count] = new AssignableInterrupt(input);
                count++;
            }
        }

        private void InitializeUsbConnection()
        {

            try
            {
                Device = new HotasThrottle();
                Controller.ActiveDevice = Device;
            }
            catch (Exception e)
            {
                Debug.Print(
                    "Client stream could not be created due to exception " +
                    e.Message);

                return;
            }
        }

        public void InitializeDevices()
        {
            InitializeTrackBall();
            TrackBallFrameTimer = new ExtendedTimer(Report_TrackBall_Frame, null, 0, 5);
        }

        private void InitializeTrackBall()
        {

            var pin_Up = new InterruptPort(G120II.TBSocket1[2], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);

            var pin_Down = new InterruptPort(G120II.TBSocket1[1], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);

            var pin_Left = new InterruptPort(G120II.TBSocket1[0], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);

            var pin_Right = new InterruptPort(G120II.TBSocket2[0], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth); 

            var trackBallInputs = new InterruptPort[] { pin_Up, pin_Down, pin_Left, pin_Right };

            var btns = new InterruptInput[] { Interrupts[0].GetInput(), Interrupts[1].GetInput(), Interrupts[2].GetInput() };

            var value = btns[0].Read();
            var value1 = btns[1].Read();

            var Pwm1 = new PWM((Cpu.PWMChannel)8, 10, 50, PWM.ScaleFactor.Microseconds, false);

            var Pwm2 = new PWM(Cpu.PWMChannel.PWM_7, 10, 50, PWM.ScaleFactor.Microseconds, false);

            var Pwm3 = new PWM(Cpu.PWMChannel.PWM_6, 10, 50, PWM.ScaleFactor.Microseconds, false);

            var ledOutputs = new PWM[] { Pwm1, Pwm2, Pwm3 };

            TrackBall = new BlackberryTrackBall(trackBallInputs, btns, ledOutputs);
        }

        private void Report_TrackBall_Frame(object sender)
        {
            lock (Device)
            {
                Device.WriteTrackBallReport(TrackBall.GetDeviceState());
            }
        }

    }
}
