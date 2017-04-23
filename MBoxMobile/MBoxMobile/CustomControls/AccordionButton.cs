using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFShapeView;

namespace MBoxMobile.CustomControls
{
    public class AccordionButton2 : View
    {
        bool _isExpanded = false;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                if (value)
                    _imageSource = "arrow_up.png";
                else
                    _imageSource = "arrow_down.png";
            }
        }

        string _imageSource = "arrow_down.png";

        public ContentView AssosiatedContent { get; set; }

        public event EventHandler Clicked;
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public AccordionButton2(int width, int height, Color backgroundColor)
        {
            var box = new ShapeView
            {
                ShapeType = ShapeType.Box,
                HeightRequest = height,
                WidthRequest = width,
                Color = backgroundColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                CornerRadius = 5,
                BorderColor = backgroundColor,
                BorderWidth = 1f,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                        new Label
                        {
                            Text = this.Text,
                            TextColor = this.TextColor,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Start,
                            Style = (Style)Application.Current.Resources["LabelAccordionStyle"]
                        },
                        new Image
                        {
                            Aspect = Aspect.AspectFill,
                            Source = _imageSource,
                            WidthRequest = 0.6 * width,
                            HeightRequest = 0.6 * width
                        }
                    }
                }
            };
        }
    }
}
