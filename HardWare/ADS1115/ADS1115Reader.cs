using System;
using Microsoft.SPOT;
using HardWare;
using Core;

namespace Hardware
{
    public class ADS1115Channel : AnalogReader
    {
        private ADS1115 ADC;

        private ADS1115.Input Input;

        private ADS1115.Gain Gain;

        private ADS1115.Resolution Resolution;

        public ADS1115Channel(ADS1115 adc, ADS1115.Input inputChannel, ADS1115.Gain gain, ADS1115.Resolution resolution)
        {
            this.ADC = adc;
            this.Input = inputChannel;
            this.Gain = gain;
            this.Resolution = resolution;                            
        }

        public short GetValue()
        {
            lock (ADC)
            {
                ADC.Configure(
                ADS1115.OperationalStatus.SingleConversion,
                ADS1115.Mode.ContinuousConversion,
                this.Input, this.Gain,
                this.Resolution,
                ADS1115.ComparatorMode.Traditional,
                ADS1115.ComparatorPolarity.ActiveHigh,
                ADS1115.ComparatorLatch.None,
                ADS1115.ComparatorQueue.Disable
                );

                return ADC.ReadADC(this.Resolution, this.Input);
            }
        }

        
    }
}
