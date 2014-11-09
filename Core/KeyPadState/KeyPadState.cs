using System;
using Microsoft.SPOT;

namespace Core.KeyPadState
{
    public class KeyPadStateData
    {

        public PageState[] PageStates { get; set; }

        public ButtonState CommandButton { get; set; }
    }
}
