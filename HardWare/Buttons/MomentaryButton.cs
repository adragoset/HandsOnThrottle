using System;
using Microsoft.SPOT.Hardware;
using FrameWork;


namespace Hardware
{
    public class MomentaryButton : Button
    {
        public MomentaryButton(int id, InterruptInput ioPin, string name)
            : base(id, ioPin, name)
        {
        }

        protected override void Button_Pressed(InterruptInput sender, bool value)
        {
            base.Button_Pressed(sender, value);
        }
    }
}