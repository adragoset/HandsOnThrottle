using System;
using Microsoft.SPOT;

namespace Core.KeyPadState
{
    public class KeyPadStateData
    {

        public ButtonState[] ButtonStates { get; set; }

        public ButtonState CommandButton { get; set; }
    }
}
