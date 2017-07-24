using Foundation;
using MBoxMobile.Interfaces;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(Version))]
namespace MBoxMobile.iOS.Helpers
{
    public class IosVersion : IVersion
    {
        public string Version
        {
            get
            {
                NSObject ver = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"];
                return ver.ToString();
            }
        }
    }
}
