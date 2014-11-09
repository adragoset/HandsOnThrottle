using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace FrameWork
{
    /// <summary>
    /// Provides access to the <see cref="InterruptInput" /> interfaces on sockets.
    /// </summary>
    public static class InterruptInputFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="InterruptInput" /> for the given socket and pin number.
        /// </summary>
        /// <param name="socket">The socket for the interrupt input interface.</param>
        /// <param name="pin">The pin used by the interrupt input interface.</param>
        /// <param name="glitchFilterMode">
        ///  A value from the <see cref="GlitchFilterMode"/> enumeration that specifies 
        ///  whether to enable the glitch filter on this interrupt input interface.
        /// </param>
        /// <param name="resistorMode">
        ///  A value from the <see cref="ResistorMode"/> enumeration that establishes a default state for the interrupt input interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
        /// </param>
        /// <param name="interruptMode">
        ///  A value from the <see cref="InterruptMode"/> enumeration that establishes the requisite conditions 
        ///  for the interface port to generate an interrupt.
        /// </param>
        /// <param name="module">The module using this interrupt input interface, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="InterruptInput" /> for the given socket and pin number.</returns>
        public static InterruptInput Create(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, Port.ResistorMode resistorMode, InterruptMode interruptMode)
        {

            // native implementation is preferred to an indirected one
            if (socket.InterruptIndirector != null)
                return socket.InterruptIndirector(socket, pin, glitchFilterMode, resistorMode, interruptMode);
            else
                throw new ArgumentException("There is no interruptInputIndirector defined");

        }
    }
}
