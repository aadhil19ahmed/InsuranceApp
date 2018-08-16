using System;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Label section view.
    /// </summary>

	public class LabelSectionView : BaseSectionView
	{
		public Label label;
		private Color labelTextColor;
        private Color BgColor;
        private LineBreakMode lineBreakMode;
        private Model.View _qaView;
        public LabelSectionView(Model.View qaView, string labelText, Color textColor, Color bgColor, LineBreakMode WordWrap)
		{
            _qaView = qaView;
			description = labelText;
			labelTextColor = textColor;
            BgColor = bgColor;
            lineBreakMode = WordWrap;
		}

		private void CreateLabelView()
		{
			label = new Label
			{
				Text = description,
				TextColor = labelTextColor,
                BackgroundColor = BgColor,
                LineBreakMode = lineBreakMode,
                FontSize = 19,
                FontFamily = "HelveticaNeue-UltraLight"
			};
            //label.IsVisible = _qaView.default_visibility;
            var stackLayout = new StackLayout 
            { 
                Padding = 5
            };
			stackLayout.Children.Add(label);
			Content = stackLayout;
		}

		public override void CreateView()
		{
			base.CreateView();
			this.CreateLabelView();
		}
	}
}