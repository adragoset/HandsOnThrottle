using System;
using Microsoft.SPOT;

namespace Core.KeyPadState
{
    public class PageState
    {
        public VirtualButton[] ButtonStates;

        public int PageNumber { get; set; }
    }
}
