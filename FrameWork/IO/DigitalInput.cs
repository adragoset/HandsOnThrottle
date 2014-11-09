using System;
using Microsoft.SPOT;

namespace FrameWork
{
    public abstract class DigitalInput : System.IDisposable
    {
        /// <summary>
        /// Reads a Boolean value at the interface port input. 
        /// </summary>
        /// <returns>A Boolean value that represents the current value of the port, either 0 or 1.</returns>
        public abstract bool Read();

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}
