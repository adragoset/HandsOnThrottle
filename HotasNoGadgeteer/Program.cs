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
        private enum CalibrationSteps { CalibrateXY, CalibrateXYCenter, CalibrateThrottle, Finished }

        private BlackberryTrackBall TrackBall;

        private ExtendedTimer ThrottleFrameTimer;

        private ExtendedTimer TrackBallTimer;

        private ExtendedTimer CalibrationTimer;

        private HotasThrottle Device;

        private HubAP5 Hub;

        private ADS1115 ADC;

        private RGBLedSeriesController LedController;

        private AssignableInterrupt[] Interrupts;

        private KeyPad KeyPad;

        private GamePad1 GamePad1;

        private GamePad2 GamePad2;

        private bool Started;

        private bool Calibrating;

        private CalibrationSteps CalibrationStep;

        public int CenterCalibrationCount { get; set; }

        public static void Main()
        {
            Program p = new Program();
            p.Started = false;
            p.Calibrating = false;
            p.CalibrationStep = CalibrationSteps.CalibrateXY;
            p.InitializeHardWare();
            p.InitializeInterrupts();
            p.InitializeUsbConnection();
            p.InitializeDevices();
            p.CalibrateAxis();
            p.CenterCalibrationCount = 0;

            while (true)
            {
                p.Start();
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
            LedController = new RGBLedSeriesController();
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

            var Pwm1 = new PWM((Cpu.PWMChannel)8, 100, 1, false);

            var Pwm2 = new PWM(Cpu.PWMChannel.PWM_7, 100, 1, false);

            var Pwm3 = new PWM(Cpu.PWMChannel.PWM_6, 100, 1, false);



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

            KeyPad = new KeyPad(LedController, keyPadInputs, "");

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
                Interrupts[43].GetInput()
            };

            GamePad1 = new GamePad1(ADC, GamePad1Inputs);

            InterruptInput[] GamePad2Inputs = new InterruptInput[] { 

                Interrupts[44].GetInput(),
                Interrupts[45].GetInput(), 
                Interrupts[46].GetInput(), 
                Interrupts[47].GetInput(), 
            };

            GamePad2 = new GamePad2(KeyPad, GamePad2Inputs);
        }

        private void StartFrameTimers()
        {
            ThrottleFrameTimer = new ExtendedTimer(Report_Throttle_Frame, null, 0, 10);
            TrackBallTimer = new ExtendedTimer(Report_Mouse_Frame, null, 0, 5);
        }

        private void Report_Mouse_Frame(object state)
        {
            lock (Device)
            {
                Device.WriteTrackBallReport(TrackBall.GetDeviceState());
            }
        }

        private void Report_Throttle_Frame(object sender)
        {

            lock (Device)
            {
                Hub.ClearInterrupts();
                Device.WriteGamePad1Report(GamePad1.GetDeviceState());
                Device.WriteGamePad2Report(GamePad2.GetDeviceState());
            }
        }

        private void CalibrateAxis()
        {
            this.Calibrating = true;

            this.CalibrationTimer = new ExtendedTimer(Calibration_Callback, null, 0, 10);
        }

        private void Calibration_Callback(object state)
        {
            if (CenterCalibrationCount <= 50)
            {
                this.GamePad1.FindXYCenter();
                CenterCalibrationCount++;
                if (CenterCalibrationCount == 51)
                {
                    GamePad1.StopCalibration();
                }
            }
            else
            {

                if (GamePad1.ButtonsPressed())
                {
                    if (this.CalibrationStep == CalibrationSteps.CalibrateXY)
                    {
                        this.CalibrationStep = CalibrationSteps.CalibrateThrottle;
                    }
                    else if (this.CalibrationStep == CalibrationSteps.CalibrateThrottle)
                    {
                        this.CalibrationStep = CalibrationSteps.Finished;
                    }

                    GamePad1.StopCalibration();
                }

                if (this.CalibrationStep == CalibrationSteps.CalibrateXY)
                {
                    this.GamePad1.CalibrateXYAxisMinMax();
                }
                else if (this.CalibrationStep == CalibrationSteps.CalibrateThrottle)
                {
                    this.GamePad1.CalibrateThrottleAxisMaxMin();
                }
                else if (this.CalibrationStep == CalibrationSteps.Finished)
                {
                    this.Calibrating = false;
                    this.CalibrationTimer.Dispose();
                    this.CalibrationTimer = null;
                }
            }

            GamePad1.CycleAxis();          
        }

        private void Start()
        {
            if (!this.Started && !this.Calibrating)
            {
                this.Started = true;
                this.StartFrameTimers();
            }
        }
    }
}
