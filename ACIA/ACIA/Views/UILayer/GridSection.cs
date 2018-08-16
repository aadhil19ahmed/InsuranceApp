using System;
using System.Collections.Generic;
using ACIA.Services.GetDynamicPage;
using ACIA.Services.RequestProvider;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Grid section.
    /// </summary>
	public class GridSection : BaseSection
	{
        int rowSize, columnSize;
        IRequestProvider requestProvider;
        IGetDynamicPageService dynamicPageService;
        public GridSection(int row, int column)
        {
            requestProvider = new RequestProvider();
            dynamicPageService = new GetDynamicPageService(requestProvider);
            rowSize = row;
            columnSize = column;
        }

		public override void CreateSection()		
        {
			this.CreateGridSection();
		}

        /// <summary>
        /// Creates the grid section.
        /// </summary>
        /// 
		public void CreateGridSection()
		{
            var data = this.Views;
            var layout = new StackLayout();
			var grid = new Grid();

           var pname = new Label
			{
				Text = this.title,
				HorizontalTextAlignment = TextAlignment.Start,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.Black
			};

			int count = 0;

			for (int row = 0; row < this.rowSize; row++)
            {
                for (int col = 0; col < this.columnSize; col++)
                {
                    TapGestureRecognizer imageTap = new TapGestureRecognizer();
                    imageTap.Tapped += OnItemSelected;
                    this.Views[count].GestureRecognizers.Add(imageTap);
                    this.Views[count].ClassId = count.ToString();
                    var StackLayout = new StackLayout();
					StackLayout.Children.Add(data[count]);
                   
					count++;
					if (count == data.Count)
					{ break; }
		}
	}

		layout.Children.Add(pname);
		layout.Children.Add(grid);

		Content = layout;

	}
		void OnItemSelected(Object sender, EventArgs e)
		{
            var view = (BaseSectionView)sender;
            DynamicUIPage homePage = dynamicPageService.GetDynamicPage(view.url);
            Navigation.PushAsync(homePage);
		}
	}
}
