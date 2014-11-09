using System;
using Microsoft.SPOT;

namespace Core.KeyPadElements
{
    public abstract class RGBKeypad
    {
        public RGBPage[] PageStates { get; private set; }

        public StatefulButton CommandButton { get; private set; }

        public int Pages { get; private set; }

        public virtual RGBPage CurrentPage { get; private set; }

        public RGBKeypad(RGBPage[] pageStates, StatefulButton commandButton)
        {
            if (pageStates.Length < 1)
            {
                throw new ArgumentException("You need at least one page to define a key pad");
            }

            if (pageStates.Length > 1 && commandButton == null)
            {
                throw new ArgumentException("If you have more than one page a command button is required");
            }

            PageStates = pageStates;
            CommandButton = commandButton;
            Pages = PageStates.Length;
            CurrentPage = PageStates[0];
        }

        public virtual void Reset()
        {
            foreach (var p in PageStates)
            {
                p.ResetColorStates();
            }
        }

        public virtual void SetPage(int pageNumber)
        {
            foreach (var p in PageStates)
            {
                if (p.PageNumber == pageNumber)
                {
                    CurrentPage = p;
                }
            }
        }
    }
}
