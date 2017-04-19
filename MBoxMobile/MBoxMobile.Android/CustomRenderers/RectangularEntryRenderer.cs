using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MBoxMobile.CustomControls;
using MBoxMobile.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(RectangularEntry), typeof(RectangularEntryRenderer))]
namespace MBoxMobile.Droid.CustomRenderers
{
    public class RectangularEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Rgb(153,204,255));
            }
        }
    }
}