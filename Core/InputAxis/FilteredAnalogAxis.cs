using System;
using Microsoft.SPOT;

namespace Core.InputAxis
{
    public class VirtualAnalogAxis : AnalogAxis
    {
        public bool IsReversed { get; private set; }

        public bool IsCenteredAxis { get; private set; }

        public bool MapToRange { get; private set; }

        public double DeadZone { get; private set; }

        public int Range { get; private set; }

        public int MaxValue { get; private set; }

        public int MinValue { get; private set; }

        public int CalculatedCenter
        {
            get
            {
                return (MaxValue - MinValue) / 2;
            }

        }

        public VirtualAnalogAxis(AnalogReader reader, Filter filter, bool isReversed, bool isCentering, bool mapToRange, double deadzone = .05, int range = 32)
            : base(reader, filter)
        {
            this.IsReversed = isReversed;

            this.IsCenteredAxis = isCentering;

            this.MapToRange = mapToRange;

            this.DeadZone = deadzone;

            this.Range = range;

            this.MaxValue = 0;


        }

        public override short GetValue()
        {
            var value = base.GetValue();

            return value;
        }
    }
}
