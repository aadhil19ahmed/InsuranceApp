using System;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Entry section view.
    /// </summary>
    public class EntrySectionView : BaseSectionView
    {
        public EntrySectionView()
        {
        }

        /// <summary>
        /// Creates the entry view.
        /// </summary>
		private void CreateEntryView()
		{
            var myEntry = new Entry
			{
				Text = description,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
			};
			var stackLayout = new StackLayout();
            var text = myEntry.Text;
			stackLayout.Children.Add(myEntry);
			Content = stackLayout;
		}
               
		public override void CreateView()
		{
			base.CreateView();
			this.CreateEntryView();
		}
    }
}
