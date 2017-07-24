using System;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MBoxMobile.CustomControls;
using MBoxMobile.Droid.CustomRenderers;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]
namespace MBoxMobile.Droid.CustomRenderers
{
    public class RoundedButtonRenderer : ButtonRenderer
    {
        private GradientDrawable _normal, _pressed;

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var button = (RoundedButton)e.NewElement;

                button.SizeChanged += (s, args) =>
                {
                    try
                    {
                        var radius = button.BorderRadius;
                        //var radius = (float)Math.Min(button.Width, button.Height);
                        //var radius = (float)Math.Min(button.Width, button.Height) / 2 * Resources.DisplayMetrics.Density; // tweak to make the Android renderer round corners properly for all screen densities

                        // Create a drawable for the button's normal state
                        _normal = new Android.Graphics.Drawables.GradientDrawable();

                        if (button.BackgroundColor.R == -1.0 && button.BackgroundColor.G == -1.0 && button.BackgroundColor.B == -1.0)
                            _normal.SetColor(Android.Graphics.Color.ParseColor("#ff2c2e2f"));
                        else
                            _normal.SetColor(button.BackgroundColor.ToAndroid());

                        _normal.SetCornerRadius(radius);
                        _normal.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());

                        // Create a drawable for the button's pressed state
                        _pressed = new Android.Graphics.Drawables.GradientDrawable();
                        var highlight = Context.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ColorActivatedHighlight }).GetColor(0, Android.Graphics.Color.Gray);
                        _pressed.SetColor(highlight);
                        _pressed.SetCornerRadius(radius);
                        _pressed.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());

                        // Add the drawables to a state list and assign the state list to the button
                        var sld = new StateListDrawable();
                        sld.AddState(new int[] { Android.Resource.Attribute.StatePressed }, _pressed);
                        sld.AddState(new int[] { }, _normal);
                        Control.Background = sld;
                    }
                    catch
                    { }
                };
            }
        }
    }
}