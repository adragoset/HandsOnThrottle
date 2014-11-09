using System;
using Microsoft.SPOT;
using FrameWork;

namespace Core
{
    public abstract class InputArray
    {

        public InterruptInput[] Inputs { get; private set; }

        public InputArray(InterruptInput[] inputs)
        {

            this.Inputs = inputs;
        }

    }
}
