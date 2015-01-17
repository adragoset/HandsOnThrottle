using System;
using Microsoft.SPOT;

namespace Core
{
    public interface AnalogReader
    {
       short GetValue();
    }
}
