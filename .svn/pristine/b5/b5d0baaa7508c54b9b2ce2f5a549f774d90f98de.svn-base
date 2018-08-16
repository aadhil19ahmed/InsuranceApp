using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Carousel section.
    /// </summary>
    public class CarouselSection : BaseSection
    {
        public DotButtonsLayout dotLayout;
        private CarouselView carousel;
        private ContentView carouselContentView;
        ObservableCollection<BaseSectionView> viewsCollection = new ObservableCollection<BaseSectionView>();
        public override void CreateSection()
        {
            this.CreateCarouselSection();
        }

        public class values
        {
            public string value { get; set; }
        }
        /// <summary>
        /// Creates the carousel section.
        /// </summary>

        public void CreateCarouselSection()
        {
            int viewIndex = 0;

            TapGestureRecognizer imageTap = new TapGestureRecognizer();
            imageTap.Tapped += OnItemSelected;
            foreach (var item in this.Views)
            {
                this.Views[viewIndex].GestureRecognizers.Add(imageTap);
                viewsCollection.Add(item);
                viewIndex++;
            }
            viewIndex = 0;

            StackLayout body = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
            };

            carousel = new CarouselView()
            {
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            carouselContentView = new ContentView()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var sectionTitleLabel = new Label()
            {
                Text = this.title,
                Style = Application.Current.Resources["SectionTitleLabel"] as Style
            };

            DataTemplate template = new DataTemplate(() =>
            {
                return carouselContentView;
            });

            carousel.ItemTemplate = template;
            carousel.ItemsSource = viewsCollection;
            carousel.HeightRequest = 210;
            carousel.PositionSelected += pageChanged;
            carouselContentView.Content = viewsCollection[0];
            dotLayout = new DotButtonsLayout(viewsCollection.Count, Color.Black, 5);
            foreach (DotButton dot in dotLayout.dots)
                dot.Clicked += dotClicked;
            body.Children.Add(sectionTitleLabel);
            body.Children.Add(carousel);
            body.Children.Add(dotLayout);
            StackLayout stack = new StackLayout()
            {
                Children = { body },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            };

            //Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            Content = stack;
        }
        /// <summary>
        /// Ons the item selected.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnItemSelected(Object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Pages the changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void pageChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            var position = (int)(e.SelectedPosition);
            var currentView = viewsCollection[position];
            currentView.IsVisible = true;
            carouselContentView.Content = currentView;

            foreach (DotButton dotButton in dotLayout.dots)
            {
                if (dotButton.index == position)
                {
                    dotButton.Opacity = 1;
                }
                else
                {
                    dotButton.Opacity = 0.2;
                }
            }
        }

        public class DotButton : BoxView
        {
            public int index;
            public DotButtonsLayout layout;
            public event ClickHandler Clicked;
            public delegate void ClickHandler(DotButton sender);
            public DotButton()
            {
                var clickCheck = new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        if (Clicked != null)
                        {
                            Clicked(this);
                        }
                    })
                };
                GestureRecognizers.Add(clickCheck);
            }
        }

        public class DotButtonsLayout : StackLayout
        {
            public DotButton[] dots;
            public DotButtonsLayout(int dotCount, Color dotColor, int dotSize)
            {
                dots = new DotButton[dotCount];

                Orientation = StackOrientation.Horizontal;
                VerticalOptions = LayoutOptions.Center;
                HorizontalOptions = LayoutOptions.Center;
                Spacing = 10;

                for (int i = 0; i < dotCount; i++)
                {
                    dots[i] = new DotButton
                    {
                        HeightRequest = dotSize,
                        WidthRequest = dotSize,
                        BackgroundColor = dotColor,
                        Opacity = 0.2,
                        Margin = new Thickness(0, 0, 0, 8)
                    };
                    dots[i].index = i;
                    dots[i].layout = this;
                    Children.Add(dots[i]);
                }
                dots[0].Opacity = 1;
            }
        }
        /// <summary>
        /// Dots the clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        private void dotClicked(object sender)
        {
            var button = (DotButton)sender;
            int index = button.index;
            carousel.Position = index;
        }
    }
}