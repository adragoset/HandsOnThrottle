using System;
using Microsoft.SPOT;
using G120;
using Microsoft.SPOT.Hardware;
using GHI.Usb.Client;
using Devices;
using HardWare;
using FrameWork;
using System.Threading;

namespace HotasNoGadgeteer
{
    public class Program
    {
        private BlackberryTrackBall TrackBall;

        private ExtendedTimer ThrottleFrameTimer;

        private ExtendedTimer LedTimer;

        private HotasThrottle Device;

        private HubAP5 Hub;

        private ADS1115 ADC;

        private AssignableInterrupt[] Interrupts;

        private KeyPad KeyPad;

        private GamePad1 GamePad1;

        private GamePad2 GamePad2;

        public static void Main()
        {
            Program p = new Program();
            p.InitializeHardWare();
            p.InitializeInterrupts();
            p.InitializeUsbConnection();
            p.InitializeDevices();
            p.StartFrameTimers();

            while (true) {
                Thread.Sleep(1000);
            }
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
            InitializeKeyPad();
            InitializeGamePads();
        }

        private void InitializeTrackBall()
        {

            var pin_Up = new InterruptPort(G120II.TBSocket1[2], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);

            var pin_Down = new InterruptPort(G120II.TBSocket1[1], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);

            var pin_Left = new InterruptPort(G120II.TBSocket1[0], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);

            var pin_Right = new InterruptPort(G120II.TBSocket2[0], false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);

            var trackBallInputs = new InterruptPort[] { pin_Up, pin_Down, pin_Left, pin_Right };

            var btns = new InterruptInput[] { Interrupts[0].GetInput(), Interrupts[1].GetInput(), Interrupts[2].GetInput() };

            var Pwm1 = new PWM((Cpu.PWMChannel)8, 10, 50, PWM.ScaleFactor.Microseconds, false);

            var Pwm2 = new PWM(Cpu.PWMChannel.PWM_7, 10, 50, PWM.ScaleFactor.Microseconds, false);

            var Pwm3 = new PWM(Cpu.PWMChannel.PWM_6, 10, 50, PWM.ScaleFactor.Microseconds, false);

            var ledOutputs = new PWM[] { Pwm1, Pwm2, Pwm3 };

            TrackBall = new BlackberryTrackBall(trackBallInputs, btns, ledOutputs);
        }

        private void InitializeKeyPad()
        {
            InterruptInput[] keyPadInputs = new InterruptInput[] { 
                Interrupts[3].GetInput(), 
                Interrupts[4].GetInput(), 
                Interrupts[5].GetInput(), 
                Interrupts[6].GetInput(), 
                Interrupts[7].GetInput(), 
                Interrupts[8].GetInput(), 
                Interrupts[9].GetInput(), 
                Interrupts[10].GetInput() 
            };

            var led1 = new OutputPort(G120II.MainBoardIoSocket1[0], false);
            var led2 = new OutputPort(G120II.MainBoardIoSocket1[1], false);
            var led3 = new OutputPort(G120II.MainBoardIoSocket1[2], false);
            var led4 = new OutputPort(G120II.MainBoardIoSocket1[3], false);
            var led5 = new OutputPort(G120II.MainBoardIoSocket1[4], false);
            var led6 = new OutputPort(G120II.MainBoardIoSocket1[5], false);
            var led7 = new OutputPort(G120II.MainBoardIoSocket1[6], false);
            var led8 = new OutputPort(G120II.MainBoardIoSocket2[1], false);

            var outPutPorts = new OutputPort[] { led1, led2, led3, led4, led5, led6, led7, led8 };

            var Pwm1 = new PWM(Cpu.PWMChannel.PWM_3, 100, 0, PWM.ScaleFactor.Nanoseconds, false);
            Pwm1.Stop();

            var Pwm2 = new PWM(Cpu.PWMChannel.PWM_5, 100, 0, PWM.ScaleFactor.Nanoseconds, false);
            Pwm2.Stop();

            var Pwm3 = new PWM(Cpu.PWMChannel.PWM_1, 100, 0, PWM.ScaleFactor.Nanoseconds, false);
            Pwm3.Stop();
            PWM[] pwmOutputs = new PWM[] { Pwm1, Pwm2, Pwm3 };

            KeyPad = new KeyPad(keyPadInputs, outPutPorts, pwmOutputs, "");
        }

        private void InitializeGamePads()
        {
            InterruptInput[] GamePad1Inputs = new InterruptInput[] {
                Interrupts[11].GetInput(), 
                Interrupts[12].GetInput(), 
                Interrupts[13].GetInput(), 
                Interrupts[14].GetInput(),
                Interrupts[15].GetInput(), 
                Interrupts[16].GetInput(), 
                Interrupts[17].GetInput(), 
                Interrupts[18].GetInput(), 
                Interrupts[19].GetInput(),
                Interrupts[20].GetInput(), 
                Interrupts[21].GetInput(), 
                Interrupts[22].GetInput(), 
                Interrupts[23].GetInput(),
                Interrupts[24].GetInput(), 
                Interrupts[25].GetInput(), 
                Interrupts[26].GetInput(), 
                Interrupts[28].GetInput(),
                Interrupts[29].GetInput(), 
                Interrupts[30].GetInput(), 
                Interrupts[31].GetInput(), 
                Interrupts[32].GetInput(),
                Interrupts[33].GetInput(), 
                Interrupts[34].GetInput(), 
                Interrupts[35].GetInput(), 
                Interrupts[36].GetInput(),
                Interrupts[37].GetInput(), 
                Interrupts[38].GetInput(), 
                Interrupts[39].GetInput(), 
                Interrupts[40].GetInput(),
                Interrupts[41].GetInput(), 
                Interrupts[42].GetInput(), 
                Interrupts[43].GetInput(), 
                Interrupts[44].GetInput(),
                Interrupts[45].GetInput(), 
                Interrupts[46].GetInput(), 
                Interrupts[47].GetInput(), 
            };

            GamePad1 = new GamePad1(ADC, GamePad1Inputs);

            InterruptInput[] GamePad2Inputs = new InterruptInput[] { 

                Interrupts[48].GetInput(),
                Interrupts[49].GetInput(), 
                Interrupts[50].GetInput(), 
                Interrupts[51].GetInput()
            };

            GamePad2 = new GamePad2(KeyPad, GamePad2Inputs);
        }

        private void StartFrameTimers()
        {

            ThrottleFrameTimer = new ExtendedTimer(Report_Throttle_Frame, null, 0, 10);
            LedTimer = new ExtendedTimer(Cycle_Led, null, 0, 10);
        }


        private void Report_Throttle_Frame(object sender)
        {
            lock (Device)
            {
                Device.WriteTrackBallReport(TrackBall.GetDeviceState());
                Device.WriteGamePad1Report(GamePad1.GetDeviceState());
                Device.WriteGamePad2Report(GamePad2.GetDeviceState());
            }
        }

        private void Cycle_Led(object state)
        {
            KeyPad.RunButtonLEDStates();
        }

    }
}
