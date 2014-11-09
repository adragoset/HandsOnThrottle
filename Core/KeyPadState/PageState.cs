using System;
using Microsoft.SPOT;

namespace Core.KeyPadState
{
    public class PageState
    {
        public ButtonState[] ButtonStates;

        public int PageNumber { get; set; }
    }
}
