using System;
using Microsoft.SPOT;
using System.Collections;

namespace Core
{
    public class AveragingFilter : Filter
    {
        public int Count;

        private Queue readings; 

        public AveragingFilter(int count)
        {
            this.Count = count;
            this.readings = new Queue();
        }

        public short Filter(short input)
        {
            if (readings.Count >= Count)
            {
                readings.Dequeue();
            }

            readings.Enqueue(input);

            object[] prevReadins = new object[Count];
            readings.CopyTo(prevReadins, 0);

            int averagedValue = 0;

            foreach (var obj in prevReadins)
            {
                if (obj != null)
                {
                    averagedValue = averagedValue + (short)obj;
                }
            }

            return (short)(averagedValue / Count);
        }
    }
}
