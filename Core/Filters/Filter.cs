using System;
using Microsoft.SPOT;

namespace Core
{
    public interface Filter
    {
        short Filter(short input);
    }
}
