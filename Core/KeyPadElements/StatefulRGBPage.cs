using System;
using Microsoft.SPOT;

namespace Core.KeyPadElements
{
    public class StatefulRGBPage : RGBPage
    {


        public StatefulRGBPage(int pageNumber, StatefulButton[] buttons)
            : base(pageNumber, buttons)
        {
        }
    }
}
