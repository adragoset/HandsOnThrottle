using System;
using Microsoft.SPOT;

namespace Core.InputAxis
{
    public abstract class AnalogAxis : Axis
    {
        private AnalogReader Reader;

        private Filter Filter;

        public AnalogAxis(AnalogReader reader, Filter filter)
        {
            this.Reader = reader;
            this.Filter = filter;            
        }

        public virtual short GetValue()
        {
            return Filter.Filter(Reader.GetValue());
        }
    }
}
