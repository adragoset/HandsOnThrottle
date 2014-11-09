using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace FrameWork
{
    public static class I2CBusFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="I2CBus" /> for the given socket.
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type I, and reserves the SDA and SCL pins.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="address">The address for the I2C device.</param>
        /// <param name="clockRateKhz">The clock rate, in kHz, used when communicating with the I2C device.</param>
        /// <param name="socket">The socket for this I2C device interface.</param>
        /// <param name="module">The module using this I2C interface, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="I2CBus" /> for the given socket.</returns>
        public static I2CBus Create(ushort address, int clockRateKhz)
        {
            Cpu.Pin nativeSclPin, nativeSdaPin;
            HardwareProvider.HwProvider.GetI2CPins(out nativeSclPin, out nativeSdaPin);

            return new NativeI2CBus(address, clockRateKhz);
        }
    }
}
