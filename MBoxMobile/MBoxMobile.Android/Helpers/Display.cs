using Android.App;
using MBoxMobile.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(MBoxMobile.Droid.Helpers.Display))]
namespace MBoxMobile.Droid.Helpers
{
    public class Display : IDisplay
    {
        public int Height { get { return Application.Context.Resources.Configuration.ScreenHeightDp; } }

        public int Width { get { return Application.Context.Resources.Configuration.ScreenWidthDp; } }

        public int HeightPx { get { return Application.Context.Resources.DisplayMetrics.HeightPixels; } }

        public int WidthPx { get { return Application.Context.Resources.DisplayMetrics.WidthPixels; } }

        public float Density { get { return Application.Context.Resources.DisplayMetrics.Density; } }

        public double Xdpi { get { return Application.Context.Resources.DisplayMetrics.Xdpi; } }

        public double Ydpi { get { return Application.Context.Resources.DisplayMetrics.Ydpi; } }

        public MBoxMobile.Interfaces.Orientation Orientation { get { return (MBoxMobile.Interfaces.Orientation)(int)Application.Context.Resources.Configuration.Orientation; } }
    }
}