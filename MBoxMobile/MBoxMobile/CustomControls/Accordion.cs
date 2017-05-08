using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MBoxMobile.CustomControls
{
    public class Accordion : ContentView
    {
        #region Private Variables
        List<AccordionSource> mDataSource;
        bool mFirstExpanded = false;
        StackLayout mMainLayout;
        #endregion

        public Accordion()
        {
            var mMainLayout = new StackLayout();
            Content = mMainLayout;
        }

        public Accordion(List<AccordionSource> aSource)
        {
            mDataSource = aSource;
            DataBind();
        }

        #region Properties
        public List<AccordionSource> DataSource
        {
            get { return mDataSource; }
            set { mDataSource = value; }
        }

        public double AccordionWidth { get; set; }
        public double AccordionHeight { get; set; }

        public bool FirstExpanded
        {
            get { return mFirstExpanded; }
            set { mFirstExpanded = value; }
        }
        #endregion

        public void DataBind()
        {
            var vMainLayout = new StackLayout();
            var vFirst = true;
            if (mDataSource != null)
            {
                foreach (var vSingleItem in mDataSource)
                {
                    var vHeaderLabel = new Label()
                    {
                        Text = vSingleItem.HeaderText,
                        BackgroundColor = (Color)Application.Current.Resources["BlueMedium"], // vSingleItem.HeaderBackGroundColor,
                        VerticalTextAlignment = TextAlignment.Center,
                        Style = (Style)Application.Current.Resources["LabelMediumStyle"]
                    };
                    
                    var vHeaderButton = new AccordionButton()
                    {
                        Text = vSingleItem.HeaderText,
                        TextColor = vSingleItem.HeaderBackGroundColor,  //intentionally
                        BackgroundColor = (Color)Application.Current.Resources["BlueMedium"], // vSingleItem.HeaderBackGroundColor,
                        WidthRequest = this.AccordionWidth,
                        HeightRequest = this.AccordionHeight,
                        Margin = new Thickness(0, 0, 0, -7)
                    };

                    var vAccordionContent = new ContentView()
                    {
                        Content = vSingleItem.ContentItems,
                        IsVisible = false,
                        HeightRequest = vSingleItem.ContentHeight
                    };

                    if (vFirst)
                    {
                        vHeaderButton.IsExpanded = mFirstExpanded;
                        vAccordionContent.IsVisible = mFirstExpanded;
                        vFirst = false;
                    }

                    vHeaderButton.AssosiatedContent = vAccordionContent;
                    vHeaderButton.Clicked += OnAccordionButtonClicked;

                    RelativeLayout relativeLayout = new RelativeLayout();
                    relativeLayout.Children.Add(vHeaderButton, Constraint.RelativeToParent((parent) => { return 0; }));
                    relativeLayout.Children.Add(vHeaderLabel,
                        Constraint.RelativeToView(vHeaderButton, (parent, sibling) => { return sibling.X + 20; }),
                        Constraint.RelativeToView(vHeaderButton, (parent, sibling) => { return sibling.Y + 10; }),
                        Constraint.RelativeToView(vHeaderButton, (parent, sibling) => { return sibling.Width * .7; }),
                        Constraint.RelativeToView(vHeaderButton, (parent, sibling) => { return sibling.Height - 20; }));

                    vMainLayout.Children.Add(relativeLayout);
                    vMainLayout.Children.Add(vAccordionContent);
                }
            }
            mMainLayout = vMainLayout;
            Content = mMainLayout;
        }

        public void DataUpdate()
        {

        }

        void OnAccordionButtonClicked(object sender, EventArgs args)
        {
            var vSenderButton = (AccordionButton)sender;
            bool isCurrentButtonExpanded = vSenderButton.IsExpanded;

            foreach (var vChildItem in mMainLayout.Children)
            {
                if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
                if (vChildItem.GetType() == typeof(RelativeLayout))
                {
                    var relative = (RelativeLayout)vChildItem;
                    var vButton = (AccordionButton)relative.Children[0];
                    vButton.IsExpanded = false;
                    vButton.Image = "arrow_down.png";
                }
            }

            if (isCurrentButtonExpanded)
            {
                vSenderButton.IsExpanded = false;
                vSenderButton.Image = "arrow_down.png";
            }
            else
            {
                vSenderButton.IsExpanded = true;
                vSenderButton.Image = "arrow_up.png";
                vSenderButton.AssosiatedContent.HeightRequest = DataSource.Find(x => x.HeaderText == vSenderButton.Text).ContentHeight;
            }
            vSenderButton.AssosiatedContent.IsVisible = vSenderButton.IsExpanded;
        }

    }

    public class AccordionButton : Button
    {
        #region Private Variables
        bool mExpand = false;
        #endregion
        public AccordionButton()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            BorderColor = Color.Black;
            BorderRadius = 5;
            BorderWidth = 1;
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Right, 10);
            Image = "arrow_down.png";
        }
        #region Properties
        public bool IsExpanded
        {
            get { return mExpand; }
            set { mExpand = value; }
        }
        public ContentView AssosiatedContent { get; set; }
        #endregion
    }

    public class AccordionSource
    {
        public string HeaderText { get; set; }
        public Color HeaderTextColor { get; set; }
        public Color HeaderBackGroundColor { get; set; }
        public View ContentItems { get; set; }
        public double ContentHeight { get; set; }
    }

    public class SimpleObject
    {
        public string TextValue { get; set; }
        public string DataValue { get; set; }
    }
}
