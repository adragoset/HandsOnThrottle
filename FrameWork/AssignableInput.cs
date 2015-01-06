using System;
using Microsoft.SPOT;

namespace FrameWork
{
    public class AssignableInput
    {
        private bool assigned;

        private DigitalInput input;

        public AssignableInput(DigitalInput input)
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

        public DigitalInput GetInput()
        {
            this.Assign();

            return this.input;
        }
    }
}

