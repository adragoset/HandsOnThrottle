using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace FrameWork
{
    public static class DigitalInputFactory
    {
        public static DigitalInput Create(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, Port.ResistorMode resistorMode)
        {
            // native implementation is preferred to an indirected one
            if (socket.DigitalInputIndirector != null)
                return socket.DigitalInputIndirector(socket, pin, glitchFilterMode, resistorMode);
            else
                throw new ArgumentException("There is no DigitalInputIndirector defined");
        }
    }
}
