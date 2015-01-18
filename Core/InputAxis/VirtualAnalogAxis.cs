using System;
using Microsoft.SPOT;

namespace Core.InputAxis
{


    public class VirtualAnalogAxis : AnalogAxis
    {
        public bool IsReversed { get; private set; }

        public bool IsCenteredAxis { get; private set; }

        public bool MapToRange { get; private set; }

        public short MaxRangeValue { get; private set; }

        public short MinRangeValue { get; private set; }

        public double DeadZone { get; private set; }

        public int Precision { get; private set; }

        public short MaxValue { get; private set; }

        public short MinValue { get; private set; }

        public short CalculatedCenter
        {
            get
            {
                return (short)(MinValue + ((MaxValue - MinValue) / 2));
            }

        }

        public int AtRestCenter { get; private set; }

        public bool CalibratingMaxMin { get; set; }

        public bool CalibratingAtRestCenter { get; set; }

        public VirtualAnalogAxis(AnalogReader reader, Filter filter, bool isReversed, bool isCentering, bool mapToRange, double deadzone = .05, int precision = 16)
            : base(reader, filter)
        {
            this.IsReversed = isReversed;

            this.IsCenteredAxis = isCentering;

            this.MapToRange = mapToRange;

            this.DeadZone = deadzone;

            this.Precision = precision;

            this.MaxValue = 0;

            CalibratingMaxMin = false;

            if (Precision == 16)
            {
                int value = 32767;
                MaxRangeValue = (short)value;
                value = -32767;
                MinRangeValue = (short)value;
            }
        }

        public override short GetValue()
        {
            var value = base.GetValue();

            if (CalibratingMaxMin)
            {
                if (value > MaxValue)
                {
                    MaxValue = value;
                }

                if (value < MinValue || MinValue == 0)
                {
                    if (value > 6000)
                    {
                        MinValue = value;
                    }
                }
            }
            else
            {
                if (IsCenteredAxis)
                {
                    if (CalibratingAtRestCenter)
                    {
                        AtRestCenter = value;
                    }
                    else
                    {
                        if (value > MaxValue)
                        {
                            value = MaxValue;
                        }

                        if (value < MinValue)
                        {
                            value = MinValue;
                        }

                        value = CalculateCenteringValue(value);
                    }
                }

                if (MapToRange)
                {
                    if (value > MaxValue)
                    {
                        value = MaxValue;
                    }

                    if (value < MinValue)
                    {
                        value = MinValue;
                    }
                    value = MapValueToRange(value);
                }

                if (IsReversed)
                {
                    value = InvertAxis(value);
                }
            }

            return value;
        }

        private short InvertAxis(short value)
        {
            if (MapToRange)
            {
                value = (short)(value * -1);
            }
            else if (IsCenteredAxis)
            {
                if (value > CalculatedCenter)
                {
                    value = (short)(CalculatedCenter - (value - CalculatedCenter));
                }
                else
                {
                    value = (short)((CalculatedCenter - value) + CalculatedCenter);
                }
            }
            else
            {
                if (value > AtRestCenter)
                {
                    value = (short)(AtRestCenter - (value - AtRestCenter));
                }
                else
                {
                    value = (short)((AtRestCenter - value) + AtRestCenter);
                }
            }
            return value;
        }

        private short MapValueToRange(short value)
        {
            if (value < CalculatedCenter)
            {
                var percentage = CalculateRangePercentage(value, MinValue, CalculatedCenter);
                value = (short)(MinRangeValue * percentage);

            }
            else if (value > CalculatedCenter)
            {
                double percentage = 0;

                if (IsCenteredAxis)
                {
                    percentage = CalculateRangePercentage(value, CalculatedCenter, MaxValue);
                }
                else {
                    percentage = 1 - CalculateRangePercentage(value, CalculatedCenter, MaxValue);
                }

                value = (short)(MaxRangeValue * percentage);
            }
            else
            {
                value = 0;
            }
            return value;
        }

        private short CalculateCenteringValue(short value)
        {
            var deadzonevalue = (MaxValue-MinValue) * DeadZone;

            var maxDeadzoneValue = AtRestCenter + deadzonevalue;

            var minDeadzoneValue = AtRestCenter - deadzonevalue;

            if (maxDeadzoneValue > value && minDeadzoneValue < value)
            {
                value = CalculatedCenter;
            }
            else
            {
                double percentage = 0;
                if (value < minDeadzoneValue)
                {
                    percentage = CalculateRangePercentage(value, MinValue, (double)minDeadzoneValue);

                    value = (short)(CalculatedCenter - ((CalculatedCenter - MinValue) * percentage));
                }
                else
                {
                    percentage = CalculateRangePercentage(value, maxDeadzoneValue, MaxValue);

                    value = (short)(((MaxValue - CalculatedCenter) * percentage) + CalculatedCenter);
                }
            }
            return value;
        }

        private double CalculateRangePercentage(short value, double minValue, double maxValue)
        {
            if (minValue - maxValue != 0)
            {
                if (value <= maxValue && value >= minValue)
                {
                    return (double)(value - maxValue) / (double)(minValue - maxValue);
                }
                else if (value > maxValue)
                {
                    return 1;
                }
                else {
                    return 0;
                }
            }

            return 0;
        }

        public override void CalibrateAxisMinMax()
        {
            this.CalibratingMaxMin = true;
        }

        public override void FindCenter()
        {
            this.CalibratingMaxMin = false;

            this.CalibratingAtRestCenter = true;
        }

        public override void StopCalibration() {
            this.CalibratingMaxMin = false;

            this.CalibratingAtRestCenter = false;
        }
    }
}
