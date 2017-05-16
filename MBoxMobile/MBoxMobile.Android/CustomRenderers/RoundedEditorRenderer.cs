using Xamarin.Forms.Platform.Android;
using MBoxMobile.Droid.CustomRenderers;
using Xamarin.Forms;
using MBoxMobile.CustomControls;
using System.ComponentModel;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(RoundedEditor), typeof(RoundedEditorRenderer))]
namespace MBoxMobile.Droid.CustomRenderers
{
    public class RoundedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var editor = (RoundedEditor)e.NewElement;
                Control.Hint = editor.Placeholder;

                Control.SetBackgroundColor(global::Android.Graphics.Color.White);

                GradientDrawable normal = new GradientDrawable();
                normal.SetCornerRadius(10);
                normal.SetStroke(1, Android.Graphics.Color.ParseColor("#30a0ff"));

                var sld = new StateListDrawable();
                sld.AddState(new int[] { }, normal);
                Control.Background = sld;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == RoundedEditor.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as RoundedEditor;
                this.Control.Hint = element.Placeholder;
            }
        }
    }
}