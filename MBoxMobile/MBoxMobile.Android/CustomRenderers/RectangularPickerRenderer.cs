using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MBoxMobile.Droid.CustomRenderers;
using MBoxMobile.CustomControls;

[assembly: ExportRenderer(typeof(RectangularPicker), typeof(RectangularPickerRenderer))]
namespace MBoxMobile.Droid.CustomRenderers
{
    public class RectangularPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Rgb(48, 160, 255));
                Control.Gravity = Android.Views.GravityFlags.CenterHorizontal;
                Control.Typeface = Android.Graphics.Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, "OpenSans-Regular.ttf");
            }
        }
    }
}