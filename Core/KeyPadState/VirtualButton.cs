using System;
using Microsoft.SPOT;

namespace Core.KeyPadState
{
    public class VirtualButton
    {
        public int Id { get; set; }

        public Color[] StateList { get; set; }

        public Color InitialColor { get; set; }

        public Color CurrentColor { get; set; }

        public void Reset()
        {
            CurrentColor = InitialColor;
        }
    }
}
