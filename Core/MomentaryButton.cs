using System;
using Microsoft.SPOT.Hardware;
using FrameWork;


namespace Core
{
    public class MomentaryButton : Button
    {
        public MomentaryButton(int id, InterruptInput ioPin)
            : base(id, ioPin, true)
        {
        }

        protected override void Button_Pressed(InterruptInput sender, bool value)
        {
            base.Button_Pressed(sender, value);
        }
    }
}