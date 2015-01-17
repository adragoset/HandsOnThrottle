using System;
using Microsoft.SPOT;

namespace Core.KeyPadState
{
    public class ButtonState
    {
        public VirtualButton[] States { get; set; }

        public int Length { get { return States.Length; } }

        public ButtonState()
        {
            States = new VirtualButton[1];
        }

        public ButtonState(VirtualButton[] states)
        {
            States = states;
        }

        public VirtualButton GetState(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentException("Invaled Index");
            }

            return States[index];
        }

        public int GetIndex(VirtualButton button)
        {
            int index = 0;
            foreach (var state in States)
            {
                if (button.Equals(state))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public void ResetStates()
        {
            foreach (var state in States)
            {
                if (state != null)
                {
                    state.Reset();
                }
            }
        }
    }
}
