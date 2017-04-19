using MBoxMobile.Interfaces;
using MBoxMobile.iOS.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Display))]
namespace MBoxMobile.iOS.Helpers
{
    public class Display : IDisplay
    {
        public int Height
        {
            get { return (int)UIScreen.MainScreen.Bounds.Height; }
        }

        public int Width
        {
            get { return (int)UIScreen.MainScreen.Bounds.Width; }
        }

        public int HeightPx
        {
            get { return 0; }
        }

        public int WidthPx
        {
            get { return 0; }
        }

        public float Density
        {
            get { return 0; }
        }

        public double Xdpi
        {
            get { return 0; }
        }

        public double Ydpi
        {
            get { return 0; }
        }

        public Orientation Orientation
        {
            get { return (Orientation)UIDevice.CurrentDevice.Orientation; }
        }
    }
}