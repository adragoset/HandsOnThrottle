using System;
using Microsoft.SPOT;

namespace FrameWork
{
    public class AssignableInterrupt
    {
        private bool assigned;

        private InterruptInput input;

        public AssignableInterrupt(InterruptInput input)
        {
            this.assigned = false;
            this.input = input;
        }

        private void Assign()
        {
            if (this.assigned)
            {
                throw new InvalidOperationException("This interrupt is already assigned.");
            }
            else
            {
                this.assigned = true;
            }
        }

        public void UnAssign(InterruptInput input)
        {
            if (this.input.Equals(input))
            {
                assigned = false;
            }
            else
            {
                throw new InvalidOperationException("You must provide this AssignableInterrupts interrupt input to un assign it.");
            }
        }

        public InterruptInput GetInput()
        {
            this.Assign();

            return this.input;
        }
    }
}
