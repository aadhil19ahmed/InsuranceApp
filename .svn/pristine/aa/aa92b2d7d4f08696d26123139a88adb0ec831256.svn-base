using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ACIA.Helper.Prompt;
using ACIA.Model;
using ACIA.Services.Dialog;
using ACIA.Services.GetDynamicPage;
using ACIA.Services.GetZIPCodeMetadata;
using ACIA.Services.RequestProvider;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Cms grid.
    /// </summary>
    public class CmsGrid : BaseSection
    {
        Section _section;
        IRequestProvider requestProvider;
        IGetDynamicPageService dynamicPageService;
        IDialogService dialogService = new DialogService();
        BaseSectionView view;

        public CmsGrid(Section section)
        {
            requestProvider = new RequestProvider();
            dynamicPageService = new GetDynamicPageService(requestProvider);
            rowSize = section.options.GridSize.row;
            columnSize = section.options.GridSize.column;
            _section = section;
        }

        int rowSize, columnSize;
        public override void CreateSection()
        {
            var StackLayout = this.CreateGridSection();
            Content = StackLayout;
        }
        /// <summary>
        /// Creates the grid section.
        /// </summary>
        /// <returns>The grid section.</returns>
        public StackLayout CreateGridSection()
        {
            var layout = new StackLayout()
            {
                Spacing = 1,
                BackgroundColor = Color.White
            };

            var sectionTitleLabel = new Label()
            {
                Text = this.title,
                Style = Application.Current.Resources["SectionTitleLabel"] as Style
            };

            var grid = new Grid
            {
                //Margin = 10,
                BackgroundColor = Color.White,
                Padding = 10,

            };

            int gridCount = 0;
            for (int row = 0; row < this.rowSize; row++)
            {
                for (int col = 0; col < this.columnSize; col++)
                {
                    var stackLayout = new StackLayout();
                    stackLayout.HeightRequest = 110;
                    stackLayout.WidthRequest = 70;
                    var ViewTitle = new Label();
                    ViewTitle.Text = this.Views[gridCount].name;
                    ViewTitle.TextColor = Color.FromHex("#FB6C5E");
                    ViewTitle.HorizontalTextAlignment = TextAlignment.Center;
                    ViewTitle.VerticalTextAlignment = TextAlignment.Center;
                    TapGestureRecognizer imageTap = new TapGestureRecognizer();
                    imageTap.Tapped += OnItemSelectedAsync;
                    this.Views[gridCount].GestureRecognizers.Add(imageTap);
                    //this.Views[gridCount].ClassId = index.ToString();
                    stackLayout.Children.Add(this.Views[gridCount]);
                    stackLayout.Children.Add(ViewTitle);
                    grid.Children.Add(stackLayout, col, row);
                    gridCount++;
                }
            }
            gridCount = 0;
            this.rowSize = 0;
            //sectionTitleLabel.SetBinding(BackgroundColorProperty, "AppThemeColorValue");
            //sectionTitleLabel.BindingContext = App.appThemeViewModel;
            layout.Children.Add(sectionTitleLabel);
            layout.Children.Add(grid);
            return layout;
        }

        async void OnItemSelectedAsync(object sender, EventArgs e)
        {
            view = (BaseSectionView)sender;
            if (view.name == "New Quote")
            {
                await Navigation.PushAsync(new InsuranceListingView());
            }
        }
    }
}
