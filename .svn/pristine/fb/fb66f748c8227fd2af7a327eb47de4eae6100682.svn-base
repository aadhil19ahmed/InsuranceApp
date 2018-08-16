using System;
using Xamarin.Forms;
namespace ACIA.Views.UILayer
{ 
  /// <summary>
  /// Button section view.
  /// </summary>
    public class ButtonSectionView : BaseSectionView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DynamicUI.UILayer.ButtonSectionView"/> class.
        /// </summary>
        public string buttonName;
		public Button button;
		private Color buttonColor;

        public ButtonSectionView(string buttonname, Color buttonbgColor)
        {
            buttonName = buttonname;
			buttonColor = buttonbgColor;		
        }

		private void CreateButtonView()
		{
            var button = new Button
            { 
                Text = buttonName,
                TextColor = Color.White,
                BackgroundColor = buttonColor,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                				
			};
			var stackLayout = new StackLayout();
			stackLayout.Children.Add(button);
			Content = stackLayout;
		}

		public override void CreateView()
		{
			base.CreateView();
			this.CreateButtonView();
		}
    }
}
